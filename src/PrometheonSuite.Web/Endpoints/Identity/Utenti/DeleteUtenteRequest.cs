namespace PrometheonSuite.Web.Endpoints.Identity.Utenti;


public record DeleteUtenteRequest
{
  public const string Route = "/Utenti/{UtenteId:guid}";

  public static string BuildRoute(Guid utenteId) =>
    Route.Replace("{UtenteId:guid}", utenteId.ToString());

  public Guid UtenteId { get; set; }
}
