using PrometheonSuite.Identity.Entities.ApplicazioneAggregate;

namespace PrometheonSuite.Identity.UseCases.Applicazioni.Get;

public record GetApplicazioneQuery(ApplicazioneId ApplicazioneId) : IQuery<Result<ApplicazioneDto>>;
