namespace PrometheonSuite.Identity.Web.Endpoints.Ruoli;

public sealed record RuoloResponse(Guid Id, string Code, string Name, Guid ApplicazioneId, string? Description);

public sealed class RuoloIdRequest
{
  public Guid RuoloId { get; init; }
}
