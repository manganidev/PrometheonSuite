using PrometheonSuite.Identity.Entities.ApplicazioneAggregate;

namespace PrometheonSuite.Identity.UseCases.Applicazioni.Create;

public record CreateApplicazioneCommand(ApplicazioneName Name, ApplicazioneCode Code) : ICommand<Result<ApplicazioneId>>;
