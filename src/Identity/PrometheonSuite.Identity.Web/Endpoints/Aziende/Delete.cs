using Microsoft.AspNetCore.Http.HttpResults;
using PrometheonSuite.Identity.Entities.AziendaAggregate;
using PrometheonSuite.Identity.UseCases.Aziendas.Delete;
using PrometheonSuite.Identity.Web.Extensions;

namespace PrometheonSuite.Identity.Web.Endpoints.Aziende;

public class Delete(IMediator mediator)
  : Endpoint<AziendaIdRequest, Results<NoContent, NotFound, ProblemHttpResult>>
{
  public override void Configure()
  {
    Delete("/Aziende/{AziendaId:guid}");
    Policies("RequireAuthenticatedUser");
    Tags("Aziende");

    Summary(s =>
    {
      s.Summary = "Delete aziende";
      s.Description = "Deletes an existing aziende.";
    });
  }

  public override async Task<Results<NoContent, NotFound, ProblemHttpResult>> ExecuteAsync(AziendaIdRequest req, CancellationToken ct)
    => (await mediator.Send(new DeleteAziendaCommand(AziendaId.From(req.AziendaId)), ct)).ToDeleteResult();
}
