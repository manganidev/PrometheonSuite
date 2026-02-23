using FluentValidation;
using Microsoft.AspNetCore.Http.HttpResults;
using PrometheonSuite.Identity.Entities.ApplicazioneAggregate;
using PrometheonSuite.Identity.Entities.AziendaAggregate;
using PrometheonSuite.Identity.Entities.FiguraAggregate;
using PrometheonSuite.Identity.Entities.UtenteAggregate;
using PrometheonSuite.Identity.Entities.UtenteApplicazioneAziendaAggregate;
using PrometheonSuite.Identity.UseCases.UtenteApplicazioneAziendas;
using PrometheonSuite.Identity.UseCases.UtenteApplicazioneAziendas.AggiungiFigura;
using PrometheonSuite.Identity.UseCases.UtenteApplicazioneAziendas.Create;
using PrometheonSuite.Identity.UseCases.UtenteApplicazioneAziendas.Get;
using PrometheonSuite.Identity.UseCases.UtenteApplicazioneAziendas.RemoveFigure;
using PrometheonSuite.Identity.Web.Extensions;

namespace PrometheonSuite.Identity.Web.Endpoints.UtenteApplicazioniAzienda;

public sealed record UtenteApplicazioneAziendaResponse(
  Guid Id,
  Guid UserId,
  Guid AziendaId,
  Guid ApplicazioneId,
  bool IsActive,
  string? DefaultLocale,
  IReadOnlyList<Guid> FiguraIds);

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

public class AddFigura(IMediator mediator)
  : Endpoint<UtenteApplicazioneAziendaFiguraRequest, Results<Ok<UtenteApplicazioneAziendaResponse>, NotFound, ProblemHttpResult>>
{
  public override void Configure()
  {
    Post("/UtenteApplicazioniAzienda/{UtenteApplicazioneAziendaId:guid}/Figure/{FiguraId:guid}");
    Policies("RequireAuthenticatedUser");
    Tags("UtenteApplicazioniAzienda");
  }

  public override async Task<Results<Ok<UtenteApplicazioneAziendaResponse>, NotFound, ProblemHttpResult>> ExecuteAsync(UtenteApplicazioneAziendaFiguraRequest req, CancellationToken ct)
  {
    var result = await mediator.Send(new AddFiguraToUtenteApplicazioneAziendaCommand(UtenteApplicazioneAziendaId.From(req.UtenteApplicazioneAziendaId), FiguraId.From(req.FiguraId)), ct);
    return result.ToUpdateResult(UtenteApplicazioneAziendaMapper.MapResponse);
  }
}

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

public sealed class UtenteApplicazioneAziendaIdRequest
{
  public Guid UtenteApplicazioneAziendaId { get; init; }
}

public sealed class UtenteApplicazioneAziendaFiguraRequest
{
  public Guid UtenteApplicazioneAziendaId { get; init; }
  public Guid FiguraId { get; init; }
}

public static class UtenteApplicazioneAziendaMapper
{
  public static UtenteApplicazioneAziendaResponse MapResponse(UtenteApplicazioneAziendaDto dto)
    => new(dto.Id.Value, dto.UserId.Value, dto.TenantId.Value, dto.ApplicazioneId.Value, dto.IsActive, dto.DefaultLocale, dto.FigureIds);
}
