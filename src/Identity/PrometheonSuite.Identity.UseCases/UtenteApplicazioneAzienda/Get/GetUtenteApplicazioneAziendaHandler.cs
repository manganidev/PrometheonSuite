using PrometheonSuite.Identity.Entities.UtenteApplicazioneAziendaAggregate;
using PrometheonSuite.Identity.Entities.UtenteApplicazioneAziendaAggregate.Specifications;
using PrometheonSuite.Identity.Core.Interfaces;
namespace  PrometheonSuite.Identity.UseCases.UtenteApplicazioneAziendas.Get;

public class GetUtenteApplicazioneAziendaHandler(ICoreRepository<UtenteApplicazioneAzienda> repository)
  : IQueryHandler<GetUtenteApplicazioneAziendaQuery, Result<UtenteApplicazioneAziendaDto>>
{
  private readonly ICoreRepository<UtenteApplicazioneAzienda> _repository = repository;

  public async ValueTask<Result<UtenteApplicazioneAziendaDto>> Handle(GetUtenteApplicazioneAziendaQuery request, CancellationToken cancellationToken)
  {
    var utenteApplicazioneAzienda = await _repository.FirstOrDefaultAsync(
      new UtenteApplicazioneAziendaByIdSpec(request.UtenteApplicazioneAziendaId), 
      cancellationToken);

    if (utenteApplicazioneAzienda == null)
    {
      return Result<UtenteApplicazioneAziendaDto>.NotFound();
    }

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
