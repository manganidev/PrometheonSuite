using PrometheonSuite.Identity.Entities.UserTenantAggregate;
using PrometheonSuite.Identity.Entities.UserTenantAggregate.Specifications;
using PrometheonSuite.Identity.Entities.FigureAggregate;
using PrometheonSuite.Identity.Entities.FigureAggregate.Specifications;
using PrometheonSuite.Identity.Entities.RoleAggregate;
using PrometheonSuite.Identity.Entities.RoleAggregate.Specifications;
using  PrometheonSuite.Identity.UseCases.Roles;

namespace  PrometheonSuite.Identity.UseCases.UserTenants.GetEffectiveRoles;

public class GetEffectiveRolesHandler(
  IRepository<UserTenant> userTenantRepository,
  IRepository<Figure> figureRepository,
  IRepository<Role> roleRepository)
  : IQueryHandler<GetEffectiveRolesQuery, Result<EffectiveRolesDto>>
{
  private readonly IRepository<UserTenant> _userTenantRepository = userTenantRepository;
  private readonly IRepository<Figure> _figureRepository = figureRepository;
  private readonly IRepository<Role> _roleRepository = roleRepository;

  public async ValueTask<Result<EffectiveRolesDto>> Handle(GetEffectiveRolesQuery request, CancellationToken cancellationToken)
  {
    var userTenant = await _userTenantRepository.FirstOrDefaultAsync(
      new UserTenantByIdSpec(request.UserTenantId), 
      cancellationToken);

    if (userTenant == null)
    {
      return Result<EffectiveRolesDto>.NotFound();
    }

    // Get roles from figures
    var figureRoleIds = new HashSet<Guid>();
    foreach (var userTenantFigure in userTenant.UserTenantFigures)
    {
      var figure = await _figureRepository.FirstOrDefaultAsync(
        new FigureByIdSpec(userTenantFigure.FigureId), 
        cancellationToken);

      if (figure != null)
      {
        foreach (var figureRole in figure.FigureRoles)
        {
          figureRoleIds.Add(figureRole.RoleId.Value);
        }
      }
    }

    // Get direct roles
    var directRoleIds = userTenant.UserTenantRoles.Select(utr => utr.RoleId.Value).ToHashSet();

    // Combine all role IDs
    var allRoleIds = figureRoleIds.Union(directRoleIds).ToList();

    // Fetch role details
    var allRoles = await _roleRepository.ListAsync(cancellationToken);
    var rolesDict = allRoles.ToDictionary(r => r.Id.Value, r => new RoleDto(r.Id, r.Code, r.Name, r.Description));

    var allRoleDtos = allRoleIds
      .Where(id => rolesDict.ContainsKey(id))
      .Select(id => rolesDict[id])
      .ToList();

    var rolesFromFiguresDtos = figureRoleIds
      .Where(id => rolesDict.ContainsKey(id))
      .Select(id => rolesDict[id])
      .ToList();

    var directRoleDtos = directRoleIds
      .Where(id => rolesDict.ContainsKey(id))
      .Select(id => rolesDict[id])
      .ToList();

    var dto = new EffectiveRolesDto(allRoleDtos, rolesFromFiguresDtos, directRoleDtos);
    return Result<EffectiveRolesDto>.Success(dto);
  }
}
