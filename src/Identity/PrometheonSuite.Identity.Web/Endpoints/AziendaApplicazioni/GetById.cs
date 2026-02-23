using Microsoft.AspNetCore.Http.HttpResults;
using PrometheonSuite.Identity.Entities.AziendaApplicazioneAggregate;
using PrometheonSuite.Identity.UseCases.AziendaApplicazioni;
using PrometheonSuite.Identity.UseCases.AziendaApplicazioni.Get;
using PrometheonSuite.Identity.Web.Extensions;

namespace PrometheonSuite.Identity.Web.Endpoints.AziendaApplicazioni;

public class GetById(IMediator mediator)
  : Endpoint<AziendaApplicazioneIdRequest, Results<Ok<AziendaApplicazioneResponse>, NotFound, ProblemHttpResult>>
{
  public override void Configure()
  {
    Get("/AziendaApplicazioni/{AziendaApplicazioneId:guid}");
    Policies("RequireAuthenticatedUser");
    Tags("AziendaApplicazioni");
  }

  public override async Task<Results<Ok<AziendaApplicazioneResponse>, NotFound, ProblemHttpResult>> ExecuteAsync(AziendaApplicazioneIdRequest req, CancellationToken ct)
    => (await mediator.Send(new GetAziendaApplicazioneQuery(AziendaApplicazioneId.From(req.AziendaApplicazioneId)), ct)).ToGetByIdResult(MapItem);

  private static AziendaApplicazioneResponse MapItem(AziendaApplicazioneDto dto)
    => new(dto.Id.Value, dto.AziendaId.Value, dto.ApplicazioneId.Value, dto.IsActive);
}
