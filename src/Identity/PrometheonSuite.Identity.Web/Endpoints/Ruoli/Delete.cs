using Microsoft.AspNetCore.Http.HttpResults;
using PrometheonSuite.Identity.Entities.RuoloAggregate;
using PrometheonSuite.Identity.UseCases.Ruolos.Delete;
using PrometheonSuite.Identity.Web.Extensions;

namespace PrometheonSuite.Identity.Web.Endpoints.Ruoli;

public class Delete(IMediator mediator)
  : Endpoint<RuoloIdRequest, Results<NoContent, NotFound, ProblemHttpResult>>
{
  public override void Configure()
  {
    Delete("/Ruoli/{RuoloId:guid}");
    Policies("RequireAuthenticatedUser");
    Tags("Ruoli");

    Summary(s =>
    {
      s.Summary = "Delete ruoli";
      s.Description = "Deletes an existing ruoli.";
    });
  }

  public override async Task<Results<NoContent, NotFound, ProblemHttpResult>> ExecuteAsync(RuoloIdRequest req, CancellationToken ct)
    => (await mediator.Send(new DeleteRuoloCommand(RuoloId.From(req.RuoloId)), ct)).ToDeleteResult();
}
