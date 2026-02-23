using PrometheonSuite.Identity.Entities.ApplicazioneAggregate;
using PrometheonSuite.Identity.Entities.AziendaAggregate;
using PrometheonSuite.Identity.Entities.AziendaApplicazioneAggregate;

namespace PrometheonSuite.Identity.UseCases.AziendaApplicazioni;

public record AziendaApplicazioneDto(AziendaApplicazioneId Id, AziendaId AziendaId, ApplicazioneId ApplicazioneId, bool IsActive);
