using Mediator;
using Microsoft.AspNetCore.Http.HttpResults;
using PrometheonSuite.Identity.Entities.UtenteAggregate;
using PrometheonSuite.Identity.UseCases.Utenti.Delete;
using PrometheonSuite.Web.Extensions;

namespace PrometheonSuite.Web.Endpoints.Identity.Utenti;

public class Delete
  : Endpoint<DeleteUtenteRequest,
             Results<NoContent,
                     NotFound,
                     ProblemHttpResult>>
{
  private readonly IMediator _mediator;

  public Delete(IMediator mediator) => _mediator = mediator;

  public override void Configure()
  {
    Delete(DeleteUtenteRequest.Route);
    AllowAnonymous();

    Summary(s =>
    {
      s.Summary = "Delete an user";
      s.Description = "Deletes an existing user by ID. This action cannot be undone.";
      s.ExampleRequest = new DeleteUtenteRequest
      {
        UtenteId = Guid.NewGuid()
      };

      s.Responses[204] = "User deleted successfully";
      s.Responses[404] = "User not found";
      s.Responses[400] = "Invalid request or deletion failed";
    });

    Tags("Utenti");

    Description(builder => builder
      .Accepts<DeleteUtenteRequest>()
      .Produces(204)
      .ProducesProblem(404)
      .ProducesProblem(400));
  }

  public override async Task<Results<NoContent, NotFound, ProblemHttpResult>>
    ExecuteAsync(DeleteUtenteRequest req, CancellationToken ct)
  {
    // UtenteId è un Vogen<Guid>: prendo il Guid dalla request e lo converto
    var cmd = new DeleteUtenteCommand(UtenteId.From(req.UtenteId));

    var result = await _mediator.Send(cmd, ct);

    return result.ToDeleteResult();
  }
}
