using Microsoft.AspNetCore.Http.HttpResults;
using PrometheonSuite.Identity.Entities.ApplicazioneAggregate;
using PrometheonSuite.Identity.UseCases.Applicazioni;
using PrometheonSuite.Identity.UseCases.Applicazioni.Get;
using PrometheonSuite.Identity.Web.Extensions;

namespace PrometheonSuite.Identity.Web.Endpoints.Applicazioni;

public class GetById(IMediator mediator)
  : Endpoint<ApplicazioneIdRequest, Results<Ok<ApplicazioneResponse>, NotFound, ProblemHttpResult>>
{
  public override void Configure()
  {
    Get("/Applicazioni/{ApplicazioneId:guid}");
    Policies("RequireAuthenticatedUser");
    Tags("Applicazioni");

    Summary(s =>
    {
      s.Summary = "Get applicazioni by id";
      s.Description = "Retrieves a single applicazioni by its identifier.";
    });
  }

  public override async Task<Results<Ok<ApplicazioneResponse>, NotFound, ProblemHttpResult>> ExecuteAsync(ApplicazioneIdRequest req, CancellationToken ct)
    => (await mediator.Send(new GetApplicazioneQuery(ApplicazioneId.From(req.ApplicazioneId)), ct)).ToGetByIdResult(MapApplicazione);

  private static ApplicazioneResponse MapApplicazione(ApplicazioneDto dto)
    => new(dto.Id.Value, dto.Name.Value, dto.Code.Value, dto.IsActive);
}
