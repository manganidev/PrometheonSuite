using PrometheonSuite.Identity.Entities.ApplicazioneAggregate;
using PrometheonSuite.Identity.Entities.AziendaAggregate;

namespace PrometheonSuite.Identity.UseCases.AziendaApplicazioni.Create;

public record CreateAziendaApplicazioneCommand(AziendaId AziendaId, ApplicazioneId ApplicazioneId) : ICommand<Result<AziendaApplicazioneDto>>;
