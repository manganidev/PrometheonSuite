namespace PrometheonSuite.Identity.Web.Endpoints.Aziende;

public sealed record AziendaResponse(Guid Id, string Name, string Code, bool IsActive);

public sealed class AziendaIdRequest
{
  public Guid AziendaId { get; init; }
}
