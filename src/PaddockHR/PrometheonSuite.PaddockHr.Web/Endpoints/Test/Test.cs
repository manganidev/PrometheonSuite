using Microsoft.AspNetCore.Http.HttpResults;
using FastEndpoints;


namespace PrometheonSuite.PaddockHR.Web.Endpoints.Utenti;

public class Test : EndpointWithoutRequest<Ok>
{
  public override void Configure()
  {
    Get("/test");
    Summary(s =>
    {
      s.Summary = "Simple test endpoint";
      s.Description = "Returns HTTP 200 OK without a response body.";
      s.Responses[200] = "OK";
    });
    Tags("Test");
  }

  public override Task<Ok> ExecuteAsync(CancellationToken ct)
    => Task.FromResult(TypedResults.Ok());
}
