using PrometheonSuite.Identity.Entities.FigureAggregate;
using PrometheonSuite.Identity.Entities.FigureAggregate.Specifications;

namespace  PrometheonSuite.Identity.UseCases.Figures.RemoveRole;

public class RemoveRoleFromFigureHandler(IRepository<Figure> repository)
  : ICommandHandler<RemoveRoleFromFigureCommand, Result<FigureDto>>
{
  private readonly IRepository<Figure> _repository = repository;

  public async ValueTask<Result<FigureDto>> Handle(RemoveRoleFromFigureCommand request, CancellationToken cancellationToken)
  {
    var figure = await _repository.FirstOrDefaultAsync(
      new FigureByIdSpec(request.FigureId), 
      cancellationToken);

    if (figure == null)
    {
      return Result<FigureDto>.NotFound();
    }

    figure.RimuoviRuolo(request.RoleId);
    await _repository.SaveChangesAsync(cancellationToken);

    var roleIds = figure.FigureRoles.Select(fr => fr.RoleId.Value).ToList();
    var dto = new FigureDto(figure.Id, figure.Code, figure.Name, figure.Description, roleIds);
    
    return Result<FigureDto>.Success(dto);
  }
}
