using PrometheonSuite.Identity.Core.Interfaces;
using PrometheonSuite.Identity.Entities.FiguraAggregate;
using PrometheonSuite.Identity.Entities.FiguraAggregate.Specifications;
using PrometheonSuite.Identity.Entities.RuoloAggregate;
using PrometheonSuite.Identity.Entities.RuoloAggregate.Specifications;
using PrometheonSuite.Identity.UseCases.Figuras;

namespace  PrometheonSuite.Identity.UseCases.Figuras.AddRuolo;

public class AddRuoloToFiguraHandler(ICoreRepository<Figura> figureRepository, ICoreRepository<Ruolo> roleRepository)
  : ICommandHandler<AddRuoloToFiguraCommand, Result<FiguraDto>>
{
  private readonly ICoreRepository<Figura> _figureRepository = figureRepository;
  private readonly ICoreRepository<Ruolo> _roleRepository = roleRepository;

  public async ValueTask<Result<FiguraDto>> Handle(AddRuoloToFiguraCommand request, CancellationToken cancellationToken)
  {
    var figure = await _figureRepository.FirstOrDefaultAsync(
      new FiguraByIdSpec(request.FiguraId), 
      cancellationToken);

    if (figure == null)
    {
      return Result<FiguraDto>.NotFound("Figura not found");
    }

    // Verify role exists
    var role = await _roleRepository.FirstOrDefaultAsync(
      new RuoloByIdSpec(request.RuoloId), 
      cancellationToken);

    if (role == null)
    {
      return Result<FiguraDto>.NotFound("Ruolo not found");
    }

    figure.AggiungiRuolo(request.RuoloId);
    await _figureRepository.SaveChangesAsync(cancellationToken);

    var roleIds = figure.FiguraRuolos.Select(fr => fr.RuoloId.Value).ToList();
    var dto = new FiguraDto(figure.Id, figure.Code, figure.Name,figure.ApplicazioneId, figure.Description, roleIds);
    
    return Result<FiguraDto>.Success(dto);
  }
}
