using PrometheonSuite.Identity.Entities.FiguraAggregate;
using PrometheonSuite.Identity.Entities.FiguraAggregate.Specifications;
using PrometheonSuite.Identity.Core.Interfaces;
namespace  PrometheonSuite.Identity.UseCases.Figuras.Update;

public class UpdateFiguraHandler(ICoreRepository<Figura> repository)
  : ICommandHandler<UpdateFiguraCommand, Result<FiguraDto>>
{
  private readonly ICoreRepository<Figura> _repository = repository;

  public async ValueTask<Result<FiguraDto>> Handle(UpdateFiguraCommand request, CancellationToken cancellationToken)
  {
    var figure = await _repository.FirstOrDefaultAsync(
      new FiguraByIdSpec(request.FiguraId), 
      cancellationToken);

    if (figure == null)
    {
      return Result<FiguraDto>.NotFound();
    }

    // Check if new code already exists on another figure
    if (figure.Code != request.Code)
    {
      var existingFigura = await _repository.FirstOrDefaultAsync(
        new FiguraByCodeSpec(request.Code), 
        cancellationToken);

      if (existingFigura != null && existingFigura.Id != figure.Id)
      {
        return Result<FiguraDto>.Error($"A figure with code '{request.Code.Value}' already exists.");
      }
    }

    figure.AggiornaInfo(request.Code, request.Name, request.Description);
    await _repository.SaveChangesAsync(cancellationToken);

    var roleIds = figure.FiguraRuolos.Select(fr => fr.RuoloId.Value).ToList();
    var dto = new FiguraDto(figure.Id, figure.Code, figure.Name,figure.ApplicazioneId, figure.Description, roleIds);
    
    return Result<FiguraDto>.Success(dto);
  }
}
