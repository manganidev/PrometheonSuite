using PrometheonSuite.Identity.Entities.UtenteAggregate;
using PrometheonSuite.Identity.Entities.AziendaAggregate;
using PrometheonSuite.Identity.Entities.ApplicazioneAggregate;

namespace  PrometheonSuite.Identity.UseCases.UtenteApplicazioneAziendas.Create;

public record CreateUtenteApplicazioneAziendaCommand(
  UtenteId UserId,
  AziendaId AziendaId,
    ApplicazioneId ApplicazioneId,
  string? DefaultLocale = null
) : ICommand<Result<UtenteApplicazioneAziendaDto>>;
