using PrometheonSuite.Identity.Entities.UserTenantAggregate;
using PrometheonSuite.Identity.Entities.UserTenantAggregate.Specifications;
using PrometheonSuite.Identity.Entities.RoleAggregate;
using PrometheonSuite.Identity.Entities.RoleAggregate.Specifications;

namespace  PrometheonSuite.Identity.UseCases.UserTenants.AddRole;

public class AddRoleToUserTenantHandler(
  IRepository<UserTenant> userTenantRepository,
  IRepository<Role> roleRepository)
  : ICommandHandler<AddRoleToUserTenantCommand, Result<UserTenantDto>>
{
  private readonly IRepository<UserTenant> _userTenantRepository = userTenantRepository;
  private readonly IRepository<Role> _roleRepository = roleRepository;

  public async ValueTask<Result<UserTenantDto>> Handle(AddRoleToUserTenantCommand request, CancellationToken cancellationToken)
  {
    var userTenant = await _userTenantRepository.FirstOrDefaultAsync(
      new UserTenantByIdSpec(request.UserTenantId), 
      cancellationToken);

    if (userTenant == null)
    {
      return Result<UserTenantDto>.NotFound("UserTenant not found");
    }

    // Verify role exists
    var role = await _roleRepository.FirstOrDefaultAsync(
      new RoleByIdSpec(request.RoleId), 
      cancellationToken);

    if (role == null)
    {
      return Result<UserTenantDto>.NotFound("Role not found");
    }

    userTenant.AggiungiRuoloDiretto(request.RoleId);
    await _userTenantRepository.SaveChangesAsync(cancellationToken);

    var figureIds = userTenant.UserTenantFigures.Select(utf => utf.FigureId.Value).ToList();
    var roleIds = userTenant.UserTenantRoles.Select(utr => utr.RoleId.Value).ToList();

    var dto = new UserTenantDto(
      userTenant.Id,
      userTenant.UserId,
      userTenant.TenantId,
      userTenant.IsActive,
      userTenant.DefaultLocale,
      figureIds,
      roleIds);

    return Result<UserTenantDto>.Success(dto);
  }
}
