using PrometheonSuite.Identity.Entities.UserTenantAggregate;
using PrometheonSuite.Identity.Entities.UserTenantAggregate.Specifications;
using PrometheonSuite.Identity.Core.Interfaces;
namespace  PrometheonSuite.Identity.UseCases.UserTenants.Get;

public class GetUserTenantHandler(ICoreRepository<UserTenant> repository)
  : IQueryHandler<GetUserTenantQuery, Result<UserTenantDto>>
{
  private readonly ICoreRepository<UserTenant> _repository = repository;

  public async ValueTask<Result<UserTenantDto>> Handle(GetUserTenantQuery request, CancellationToken cancellationToken)
  {
    var userTenant = await _repository.FirstOrDefaultAsync(
      new UserTenantByIdSpec(request.UserTenantId), 
      cancellationToken);

    if (userTenant == null)
    {
      return Result<UserTenantDto>.NotFound();
    }

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
