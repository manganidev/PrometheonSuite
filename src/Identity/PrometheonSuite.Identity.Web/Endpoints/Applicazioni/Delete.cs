using Microsoft.AspNetCore.Http.HttpResults;
using PrometheonSuite.Identity.Entities.ApplicazioneAggregate;
using PrometheonSuite.Identity.UseCases.Applicazioni.Delete;
using PrometheonSuite.Identity.Web.Extensions;

namespace PrometheonSuite.Identity.Web.Endpoints.Applicazioni;

public class Delete(IMediator mediator)
  : Endpoint<ApplicazioneIdRequest, Results<NoContent, NotFound, ProblemHttpResult>>
{
  public override void Configure()
  {
    Delete("/Applicazioni/{ApplicazioneId:guid}");
    Policies("RequireAuthenticatedUser");
    Tags("Applicazioni");

    Summary(s =>
    {
      s.Summary = "Delete applicazioni";
      s.Description = "Deletes an existing applicazioni.";
    });
  }

  public override async Task<Results<NoContent, NotFound, ProblemHttpResult>> ExecuteAsync(ApplicazioneIdRequest req, CancellationToken ct)
    => (await mediator.Send(new DeleteApplicazioneCommand(ApplicazioneId.From(req.ApplicazioneId)), ct)).ToDeleteResult();
}
