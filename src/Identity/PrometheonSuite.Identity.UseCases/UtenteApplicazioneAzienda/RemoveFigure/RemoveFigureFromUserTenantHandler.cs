using PrometheonSuite.Identity.Entities.UtenteApplicazioneAziendaAggregate;
using PrometheonSuite.Identity.Entities.UtenteApplicazioneAziendaAggregate.Specifications;
using PrometheonSuite.Identity.Core.Interfaces;
namespace  PrometheonSuite.Identity.UseCases.UtenteApplicazioneAziendas.RemoveFigure;

public class RemoveFigureFromUtenteApplicazioneAziendaHandler(ICoreRepository<UtenteApplicazioneAzienda> repository)
  : ICommandHandler<RemoveFigureFromUtenteApplicazioneAziendaCommand, Result<UtenteApplicazioneAziendaDto>>
{
  private readonly ICoreRepository<UtenteApplicazioneAzienda> _repository = repository;

  public async ValueTask<Result<UtenteApplicazioneAziendaDto>> Handle(RemoveFigureFromUtenteApplicazioneAziendaCommand request, CancellationToken cancellationToken)
  {
    var utenteApplicazioneAzienda = await _repository.FirstOrDefaultAsync(
      new UtenteApplicazioneAziendaByIdSpec(request.UtenteApplicazioneAziendaId), 
      cancellationToken);

    if (utenteApplicazioneAzienda == null)
    {
      return Result<UtenteApplicazioneAziendaDto>.NotFound();
    }

    utenteApplicazioneAzienda.RimuoviFigura(request.FigureId);
    await _repository.SaveChangesAsync(cancellationToken);

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
