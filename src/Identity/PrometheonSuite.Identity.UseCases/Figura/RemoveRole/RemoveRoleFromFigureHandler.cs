using PrometheonSuite.Identity.Entities.FiguraAggregate;
using PrometheonSuite.Identity.Entities.FiguraAggregate.Specifications;
using PrometheonSuite.Identity.Core.Interfaces;
namespace  PrometheonSuite.Identity.UseCases.Figuras.RemoveRuolo;

public class RemoveRuoloFromFiguraHandler(ICoreRepository<Figura> repository)
  : ICommandHandler<RemoveRuoloFromFiguraCommand, Result<FiguraDto>>
{
  private readonly ICoreRepository<Figura> _repository = repository;

  public async ValueTask<Result<FiguraDto>> Handle(RemoveRuoloFromFiguraCommand request, CancellationToken cancellationToken)
  {
    var figure = await _repository.FirstOrDefaultAsync(
      new FiguraByIdSpec(request.FiguraId), 
      cancellationToken);

    if (figure == null)
    {
      return Result<FiguraDto>.NotFound();
    }

    figure.RimuoviRuolo(request.RuoloId);
    await _repository.SaveChangesAsync(cancellationToken);

    var roleIds = figure.FiguraRuolos.Select(fr => fr.RuoloId.Value).ToList();
    var dto = new FiguraDto(figure.Id, figure.Code, figure.Name,figure.ApplicazioneId, figure.Description, roleIds);
    
    return Result<FiguraDto>.Success(dto);
  }
}
