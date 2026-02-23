namespace PrometheonSuite.Identity.Web.Endpoints.AziendaApplicazioni;

public sealed record AziendaApplicazioneResponse(Guid Id, Guid AziendaId, Guid ApplicazioneId, bool IsActive);

public sealed class AziendaApplicazioneIdRequest
{
  public Guid AziendaApplicazioneId { get; init; }
}
