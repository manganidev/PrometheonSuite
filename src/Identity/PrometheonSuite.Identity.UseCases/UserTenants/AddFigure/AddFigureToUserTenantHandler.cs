using PrometheonSuite.Identity.Entities.UserTenantAggregate;
using PrometheonSuite.Identity.Entities.UserTenantAggregate.Specifications;
using PrometheonSuite.Identity.Entities.FigureAggregate;
using PrometheonSuite.Identity.Entities.FigureAggregate.Specifications;
using PrometheonSuite.Identity.Core.Interfaces;
namespace  PrometheonSuite.Identity.UseCases.UserTenants.AddFigure;

public class AddFigureToUserTenantHandler(
  ICoreRepository<UserTenant> userTenantRepository,
  ICoreRepository<Figure> figureRepository)
  : ICommandHandler<AddFigureToUserTenantCommand, Result<UserTenantDto>>
{
  private readonly ICoreRepository<UserTenant> _userTenantRepository = userTenantRepository;
  private readonly ICoreRepository<Figure> _figureRepository = figureRepository;

  public async ValueTask<Result<UserTenantDto>> Handle(AddFigureToUserTenantCommand request, CancellationToken cancellationToken)
  {
    var userTenant = await _userTenantRepository.FirstOrDefaultAsync(
      new UserTenantByIdSpec(request.UserTenantId), 
      cancellationToken);

    if (userTenant == null)
    {
      return Result<UserTenantDto>.NotFound("UserTenant not found");
    }

    // Verify figure exists
    var figure = await _figureRepository.FirstOrDefaultAsync(
      new FigureByIdSpec(request.FigureId), 
      cancellationToken);

    if (figure == null)
    {
      return Result<UserTenantDto>.NotFound("Figure not found");
    }

    userTenant.AggiungiFigura(request.FigureId);
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
