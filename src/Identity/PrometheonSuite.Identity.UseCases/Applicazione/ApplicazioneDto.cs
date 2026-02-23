using PrometheonSuite.Identity.Entities.ApplicazioneAggregate;

namespace PrometheonSuite.Identity.UseCases.Applicazioni;

public record ApplicazioneDto(ApplicazioneId Id, ApplicazioneName Name, ApplicazioneCode Code, bool IsActive);
