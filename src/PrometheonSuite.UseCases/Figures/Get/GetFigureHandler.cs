using PrometheonSuite.Identity.Entities.FigureAggregate;
using PrometheonSuite.Identity.Entities.FigureAggregate.Specifications;
using PrometheonSuite.Identity.Core.Interfaces;
namespace  PrometheonSuite.Identity.UseCases.Figures.Get;

public class GetFigureHandler(ICoreRepository<Figure> repository)
  : IQueryHandler<GetFigureQuery, Result<FigureDto>>
{
  private readonly ICoreRepository<Figure> _repository = repository;

  public async ValueTask<Result<FigureDto>> Handle(GetFigureQuery request, CancellationToken cancellationToken)
  {
    var figure = await _repository.FirstOrDefaultAsync(
      new FigureByIdSpec(request.FigureId), 
      cancellationToken);

    if (figure == null)
    {
      return Result<FigureDto>.NotFound();
    }

    var roleIds = figure.FigureRoles.Select(fr => fr.RoleId.Value).ToList();
    var dto = new FigureDto(figure.Id, figure.Code, figure.Name, figure.Description, roleIds);
    
    return Result<FigureDto>.Success(dto);
  }
}
