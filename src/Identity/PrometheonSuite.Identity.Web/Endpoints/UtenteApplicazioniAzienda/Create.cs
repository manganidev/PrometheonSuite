using FluentValidation;
using Microsoft.AspNetCore.Http.HttpResults;
using PrometheonSuite.Identity.Entities.ApplicazioneAggregate;
using PrometheonSuite.Identity.Entities.AziendaAggregate;
using PrometheonSuite.Identity.Entities.UtenteAggregate;
using PrometheonSuite.Identity.UseCases.UtenteApplicazioneAziendas.Create;
using PrometheonSuite.Identity.Web.Extensions;

namespace PrometheonSuite.Identity.Web.Endpoints.UtenteApplicazioniAzienda;

public class Create(IMediator mediator)
  : Endpoint<CreateUtenteApplicazioneAziendaRequest, Results<Created<UtenteApplicazioneAziendaResponse>, ValidationProblem, ProblemHttpResult>>
{
  public override void Configure()
  {
    Post("/UtenteApplicazioniAzienda");
    Policies("RequireAuthenticatedUser");
    Tags("UtenteApplicazioniAzienda");
  }

  public override async Task<Results<Created<UtenteApplicazioneAziendaResponse>, ValidationProblem, ProblemHttpResult>> ExecuteAsync(CreateUtenteApplicazioneAziendaRequest req, CancellationToken ct)
  {
    var result = await mediator.Send(new CreateUtenteApplicazioneAziendaCommand(
      UtenteId.From(req.UserId),
      AziendaId.From(req.AziendaId),
      ApplicazioneId.From(req.ApplicazioneId),
      req.DefaultLocale), ct);

    return result.ToCreatedResult(x => $"/UtenteApplicazioniAzienda/{x.Id.Value}", UtenteApplicazioneAziendaMapper.MapResponse);
  }
}

public sealed class CreateUtenteApplicazioneAziendaRequest
{
  public Guid UserId { get; init; }
  public Guid AziendaId { get; init; }
  public Guid ApplicazioneId { get; init; }
  public string? DefaultLocale { get; init; }
}

public sealed class CreateUtenteApplicazioneAziendaValidator : Validator<CreateUtenteApplicazioneAziendaRequest>
{
  public CreateUtenteApplicazioneAziendaValidator()
  {
    RuleFor(x => x.UserId).NotEmpty();
    RuleFor(x => x.AziendaId).NotEmpty();
    RuleFor(x => x.ApplicazioneId).NotEmpty();
  }
}
