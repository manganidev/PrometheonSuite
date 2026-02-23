using Microsoft.AspNetCore.Http.HttpResults;
using PrometheonSuite.Identity.Entities.FiguraAggregate;
using PrometheonSuite.Identity.Entities.UtenteApplicazioneAziendaAggregate;
using PrometheonSuite.Identity.UseCases.UtenteApplicazioneAziendas.RemoveFigure;
using PrometheonSuite.Identity.Web.Extensions;

namespace PrometheonSuite.Identity.Web.Endpoints.UtenteApplicazioniAzienda;

public class RemoveFigura(IMediator mediator)
  : Endpoint<UtenteApplicazioneAziendaFiguraRequest, Results<Ok<UtenteApplicazioneAziendaResponse>, NotFound, ProblemHttpResult>>
{
  public override void Configure()
  {
    Delete("/UtenteApplicazioniAzienda/{UtenteApplicazioneAziendaId:guid}/Figure/{FiguraId:guid}");
    Policies("RequireAuthenticatedUser");
    Tags("UtenteApplicazioniAzienda");
  }

  public override async Task<Results<Ok<UtenteApplicazioneAziendaResponse>, NotFound, ProblemHttpResult>> ExecuteAsync(UtenteApplicazioneAziendaFiguraRequest req, CancellationToken ct)
  {
    var result = await mediator.Send(new RemoveFigureFromUtenteApplicazioneAziendaCommand(UtenteApplicazioneAziendaId.From(req.UtenteApplicazioneAziendaId), FiguraId.From(req.FiguraId)), ct);
    return result.ToUpdateResult(UtenteApplicazioneAziendaMapper.MapResponse);
  }
}
