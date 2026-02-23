using PrometheonSuite.Identity.Entities.AziendaApplicazioneAggregate;

namespace PrometheonSuite.Identity.UseCases.AziendaApplicazioni.Delete;

public record DeleteAziendaApplicazioneCommand(AziendaApplicazioneId AziendaApplicazioneId) : ICommand<Result>;
