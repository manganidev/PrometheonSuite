using PrometheonSuite.Identity.Entities.ApplicazioneAggregate;

namespace PrometheonSuite.Identity.UseCases.Applicazioni.Update;

public record UpdateApplicazioneCommand(ApplicazioneId ApplicazioneId, ApplicazioneName Name, ApplicazioneCode Code) : ICommand<Result<ApplicazioneDto>>;
