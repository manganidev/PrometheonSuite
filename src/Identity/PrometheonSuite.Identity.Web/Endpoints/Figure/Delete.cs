using Microsoft.AspNetCore.Http.HttpResults;
using PrometheonSuite.Identity.Entities.FiguraAggregate;
using PrometheonSuite.Identity.UseCases.Figuras.Delete;
using PrometheonSuite.Identity.Web.Extensions;

namespace PrometheonSuite.Identity.Web.Endpoints.Figure;

public class Delete(IMediator mediator)
  : Endpoint<DeleteFiguraRequest, Results<NoContent, NotFound, ProblemHttpResult>>
{
  public override void Configure()
  {
    Delete("/Figure/{FiguraId:guid}");
    Policies("RequireAuthenticatedUser");
    Tags("Figure");

    Summary(s =>
    {
      s.Summary = "Delete figure";
      s.Description = "Deletes an existing figure.";
    });
  }

  public override async Task<Results<NoContent, NotFound, ProblemHttpResult>> ExecuteAsync(DeleteFiguraRequest req, CancellationToken ct)
  {
    var result = await mediator.Send(new DeleteFiguraCommand(FiguraId.From(req.FiguraId)), ct);
    return result.ToDeleteResult();
  }
}

public sealed class DeleteFiguraRequest
{
  public Guid FiguraId { get; init; }
}
