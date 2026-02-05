using PrometheonSuite.Identity.Entities.UtenteApplicazioneAziendaAggregate;
using PrometheonSuite.Identity.Entities.UtenteAggregate;
using PrometheonSuite.Identity.Entities.AziendaAggregate;
using PrometheonSuite.Identity.Entities.ApplicazioneAggregate;

namespace  PrometheonSuite.Identity.UseCases.UtenteApplicazioneAziendas;

public record UtenteApplicazioneAziendaDto(
  UtenteApplicazioneAziendaId Id,
  UtenteId UserId,
  AziendaId TenantId,
  ApplicazioneId ApplicazioneId,
  bool IsActive,
  string? DefaultLocale,
  IReadOnlyList<Guid> FigureIds
);
