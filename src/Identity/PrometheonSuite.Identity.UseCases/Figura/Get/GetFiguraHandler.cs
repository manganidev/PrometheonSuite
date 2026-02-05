using PrometheonSuite.Identity.Entities.FiguraAggregate;
using PrometheonSuite.Identity.Entities.FiguraAggregate.Specifications;
using PrometheonSuite.Identity.Core.Interfaces;
namespace  PrometheonSuite.Identity.UseCases.Figuras.Get;

public class GetFiguraHandler(ICoreRepository<Figura> repository)
  : IQueryHandler<GetFiguraQuery, Result<FiguraDto>>
{
  private readonly ICoreRepository<Figura> _repository = repository;

  public async ValueTask<Result<FiguraDto>> Handle(GetFiguraQuery request, CancellationToken cancellationToken)
  {
    var figure = await _repository.FirstOrDefaultAsync(
      new FiguraByIdSpec(request.FiguraId), 
      cancellationToken);

    if (figure == null)
    {
      return Result<FiguraDto>.NotFound();
    }

    var roleIds = figure.FiguraRuolos.Select(fr => fr.RuoloId.Value).ToList();
    var dto = new FiguraDto(figure.Id, figure.Code, figure.Name,figure.ApplicazioneId, figure.Description, roleIds);
    
    return Result<FiguraDto>.Success(dto);
  }
}
