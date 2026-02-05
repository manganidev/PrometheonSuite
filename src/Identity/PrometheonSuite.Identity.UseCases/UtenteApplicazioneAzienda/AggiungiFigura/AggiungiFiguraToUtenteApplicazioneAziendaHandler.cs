using PrometheonSuite.Identity.Entities.UtenteApplicazioneAziendaAggregate;
using PrometheonSuite.Identity.Entities.UtenteApplicazioneAziendaAggregate.Specifications;
using PrometheonSuite.Identity.Entities.FiguraAggregate;
using PrometheonSuite.Identity.Entities.FiguraAggregate.Specifications;
using PrometheonSuite.Identity.Core.Interfaces;
namespace  PrometheonSuite.Identity.UseCases.UtenteApplicazioneAziendas.AggiungiFigura;

public class AddFiguraToUtenteApplicazioneAziendaHandler(
  ICoreRepository<UtenteApplicazioneAzienda> utenteApplicazioneAziendaRepository,
  ICoreRepository<Figura> figureRepository)
  : ICommandHandler<AddFiguraToUtenteApplicazioneAziendaCommand, Result<UtenteApplicazioneAziendaDto>>
{
  private readonly ICoreRepository<UtenteApplicazioneAzienda> _utenteApplicazioneAziendaRepository = utenteApplicazioneAziendaRepository;
  private readonly ICoreRepository<Figura> _figureRepository = figureRepository;

  public async ValueTask<Result<UtenteApplicazioneAziendaDto>> Handle(AddFiguraToUtenteApplicazioneAziendaCommand request, CancellationToken cancellationToken)
  {
    var utenteApplicazioneAzienda = await _utenteApplicazioneAziendaRepository.FirstOrDefaultAsync(
      new UtenteApplicazioneAziendaByIdSpec(request.UtenteApplicazioneAziendaId), 
      cancellationToken);

    if (utenteApplicazioneAzienda == null)
    {
      return Result<UtenteApplicazioneAziendaDto>.NotFound("UtenteApplicazioneAzienda not found");
    }

    // Verify figure exists
    var figure = await _figureRepository.FirstOrDefaultAsync(
      new FiguraByIdSpec(request.FiguraId), 
      cancellationToken);

    if (figure == null)
    {
      return Result<UtenteApplicazioneAziendaDto>.NotFound("Figura not found");
    }

    utenteApplicazioneAzienda.AggiungiFigura(request.FiguraId);
    await _utenteApplicazioneAziendaRepository.SaveChangesAsync(cancellationToken);

    var figureIds = utenteApplicazioneAzienda.UtenteApplicazioneAziendaFiguras.Select(utf => utf.FiguraId.Value).ToList();

    var dto = new UtenteApplicazioneAziendaDto(
      utenteApplicazioneAzienda.Id,
      utenteApplicazioneAzienda.UserId,
      utenteApplicazioneAzienda.AziendaId,
      utenteApplicazioneAzienda.ApplicazioneId,
      utenteApplicazioneAzienda.IsActive,
      utenteApplicazioneAzienda.DefaultLocale,
      figureIds);

    return Result<UtenteApplicazioneAziendaDto>.Success(dto);
  }
}
