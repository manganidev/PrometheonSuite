namespace PrometheonSuite.Identity.Web.Endpoints.Applicazioni;

public sealed record ApplicazioneResponse(Guid Id, string Name, string Code, bool IsActive);

public sealed class ApplicazioneIdRequest
{
  public Guid ApplicazioneId { get; init; }
}
