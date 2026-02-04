using PrometheonSuite.Identity.Entities.UserTenantAggregate;
using PrometheonSuite.Identity.Entities.UserTenantAggregate.Specifications;
using PrometheonSuite.Identity.Core.Interfaces;
namespace  PrometheonSuite.Identity.UseCases.UserTenants.RemoveFigure;

public class RemoveFigureFromUserTenantHandler(ICoreRepository<UserTenant> repository)
  : ICommandHandler<RemoveFigureFromUserTenantCommand, Result<UserTenantDto>>
{
  private readonly ICoreRepository<UserTenant> _repository = repository;

  public async ValueTask<Result<UserTenantDto>> Handle(RemoveFigureFromUserTenantCommand request, CancellationToken cancellationToken)
  {
    var userTenant = await _repository.FirstOrDefaultAsync(
      new UserTenantByIdSpec(request.UserTenantId), 
      cancellationToken);

    if (userTenant == null)
    {
      return Result<UserTenantDto>.NotFound();
    }

    userTenant.RimuoviFigura(request.FigureId);
    await _repository.SaveChangesAsync(cancellationToken);

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
