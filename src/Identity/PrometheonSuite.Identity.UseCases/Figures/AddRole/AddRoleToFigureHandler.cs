using PrometheonSuite.Identity.Core.Interfaces;
using PrometheonSuite.Identity.Entities.FigureAggregate;
using PrometheonSuite.Identity.Entities.FigureAggregate.Specifications;
using PrometheonSuite.Identity.Entities.RoleAggregate;
using PrometheonSuite.Identity.Entities.RoleAggregate.Specifications;
using PrometheonSuite.Identity.UseCases.Figures;

namespace  PrometheonSuite.Identity.UseCases.Figures.AddRole;

public class AddRoleToFigureHandler(ICoreRepository<Figure> figureRepository, ICoreRepository<Role> roleRepository)
  : ICommandHandler<AddRoleToFigureCommand, Result<FigureDto>>
{
  private readonly ICoreRepository<Figure> _figureRepository = figureRepository;
  private readonly ICoreRepository<Role> _roleRepository = roleRepository;

  public async ValueTask<Result<FigureDto>> Handle(AddRoleToFigureCommand request, CancellationToken cancellationToken)
  {
    var figure = await _figureRepository.FirstOrDefaultAsync(
      new FigureByIdSpec(request.FigureId), 
      cancellationToken);

    if (figure == null)
    {
      return Result<FigureDto>.NotFound("Figure not found");
    }

    // Verify role exists
    var role = await _roleRepository.FirstOrDefaultAsync(
      new RoleByIdSpec(request.RoleId), 
      cancellationToken);

    if (role == null)
    {
      return Result<FigureDto>.NotFound("Role not found");
    }

    figure.AggiungiRuolo(request.RoleId);
    await _figureRepository.SaveChangesAsync(cancellationToken);

    var roleIds = figure.FigureRoles.Select(fr => fr.RoleId.Value).ToList();
    var dto = new FigureDto(figure.Id, figure.Code, figure.Name, figure.Description, roleIds);
    
    return Result<FigureDto>.Success(dto);
  }
}
