
using Microsoft.AspNetCore.Http.HttpResults;
using PrometheonSuite.Identity.Entities.UtenteAggregate;
using PrometheonSuite.Identity.UseCases.Utenti;
using PrometheonSuite.Identity.UseCases.Utenti.Get;
using PrometheonSuite.Web.Extensions;

namespace PrometheonSuite.Web.Endpoints.Identity.Utenti;

public class GetById(IMediator mediator)
  : Endpoint<GetUtenteByIdRequest,
             Results<Ok<UtenteRecord>,
                     NotFound,
                     ProblemHttpResult>,
             GetUtenteByIdMapper>
{
  public override void Configure()
  {
    Get(GetUtenteByIdRequest.Route);
    AllowAnonymous();

    Summary(s =>
    {
      s.Summary = "Get a user by ID";
      s.Description = "Retrieves a specific user by their unique identifier. Returns detailed user information including ID, username, email, and active status.";
      s.ExampleRequest = new GetUtenteByIdRequest { UtenteId = Guid.NewGuid() };
      s.ResponseExamples[200] = new UtenteRecord(Guid.NewGuid(), "johndoe", "john.doe@example.com", true);

      s.Responses[200] = "User found and returned successfully";
      s.Responses[404] = "User with specified ID not found";
    });

    Tags("Utenti");

    Description(builder => builder
      .Accepts<GetUtenteByIdRequest>()
      .Produces<UtenteRecord>(200, "application/json")
      .ProducesProblem(404));
  }

  public override async Task<Results<Ok<UtenteRecord>, NotFound, ProblemHttpResult>>
    ExecuteAsync(GetUtenteByIdRequest request, CancellationToken ct)
  {
    var result = await mediator.Send(new GetUtenteQuery(UtenteId.From(request.UtenteId)), ct);

    return result.ToGetByIdResult(Map.FromEntity);
  }
}

public class GetUtenteByIdRequest
{
  public const string Route = "/Utenti/{UtenteId:guid}";

  public static string BuildRoute(Guid utenteId) =>
    Route.Replace("{UtenteId:guid}", utenteId.ToString());

  public Guid UtenteId { get; set; }
}

public sealed class GetUtenteByIdMapper
  : Mapper<GetUtenteByIdRequest, UtenteRecord, UtenteDto>
{
  public override UtenteRecord FromEntity(UtenteDto e)
    => new(e.Id.Value, e.Username.Value, e.Email.Value, e.Attivo);
}
