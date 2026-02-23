using Microsoft.AspNetCore.Http.HttpResults;
using PrometheonSuite.Identity.Entities.UtenteApplicazioneAziendaAggregate;
using PrometheonSuite.Identity.UseCases.UtenteApplicazioneAziendas.Get;
using PrometheonSuite.Identity.Web.Extensions;

namespace PrometheonSuite.Identity.Web.Endpoints.UtenteApplicazioniAzienda;

public class GetById(IMediator mediator)
  : Endpoint<UtenteApplicazioneAziendaIdRequest, Results<Ok<UtenteApplicazioneAziendaResponse>, NotFound, ProblemHttpResult>>
{
  public override void Configure()
  {
    Get("/UtenteApplicazioniAzienda/{UtenteApplicazioneAziendaId:guid}");
    Policies("RequireAuthenticatedUser");
    Tags("UtenteApplicazioniAzienda");
  }

  public override async Task<Results<Ok<UtenteApplicazioneAziendaResponse>, NotFound, ProblemHttpResult>> ExecuteAsync(UtenteApplicazioneAziendaIdRequest req, CancellationToken ct)
  {
    var result = await mediator.Send(new GetUtenteApplicazioneAziendaQuery(UtenteApplicazioneAziendaId.From(req.UtenteApplicazioneAziendaId)), ct);
    return result.ToGetByIdResult(UtenteApplicazioneAziendaMapper.MapResponse);
  }
}
