using PrometheonSuite.Identity.Entities.ApplicazioneAggregate;

namespace PrometheonSuite.Identity.UseCases.Applicazioni.Delete;

public record DeleteApplicazioneCommand(ApplicazioneId ApplicazioneId) : ICommand<Result>;
