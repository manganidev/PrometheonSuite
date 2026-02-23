using PrometheonSuite.Identity.Entities.AziendaApplicazioneAggregate;

namespace PrometheonSuite.Identity.UseCases.AziendaApplicazioni.Get;

public record GetAziendaApplicazioneQuery(AziendaApplicazioneId AziendaApplicazioneId) : IQuery<Result<AziendaApplicazioneDto>>;
