using Microsoft.AspNetCore.Http.HttpResults;
using PrometheonSuite.Identity.Entities.AziendaApplicazioneAggregate;
using PrometheonSuite.Identity.UseCases.AziendaApplicazioni.Delete;
using PrometheonSuite.Identity.Web.Extensions;

namespace PrometheonSuite.Identity.Web.Endpoints.AziendaApplicazioni;

public class Delete(IMediator mediator)
  : Endpoint<AziendaApplicazioneIdRequest, Results<NoContent, NotFound, ProblemHttpResult>>
{
  public override void Configure()
  {
    Delete("/AziendaApplicazioni/{AziendaApplicazioneId:guid}");
    Policies("RequireAuthenticatedUser");
    Tags("AziendaApplicazioni");
  }

  public override async Task<Results<NoContent, NotFound, ProblemHttpResult>> ExecuteAsync(AziendaApplicazioneIdRequest req, CancellationToken ct)
    => (await mediator.Send(new DeleteAziendaApplicazioneCommand(AziendaApplicazioneId.From(req.AziendaApplicazioneId)), ct)).ToDeleteResult();
}
