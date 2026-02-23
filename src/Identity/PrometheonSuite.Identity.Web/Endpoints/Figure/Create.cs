using FluentValidation;
using Microsoft.AspNetCore.Http.HttpResults;
using PrometheonSuite.Identity.Entities.ApplicazioneAggregate;
using PrometheonSuite.Identity.Entities.FiguraAggregate;
using PrometheonSuite.Identity.UseCases.Figuras.Create;
using PrometheonSuite.Identity.Web.Extensions;

namespace PrometheonSuite.Identity.Web.Endpoints.Figure;

public class Create(IMediator mediator)
  : Endpoint<CreateFiguraRequest, Results<Created<CreateFiguraResponse>, ValidationProblem, ProblemHttpResult>>
{
  public override void Configure()
  {
    Post("/Figure");
    Policies("RequireAuthenticatedUser");
    Tags("Figure");
  }

  public override async Task<Results<Created<CreateFiguraResponse>, ValidationProblem, ProblemHttpResult>> ExecuteAsync(CreateFiguraRequest req, CancellationToken ct)
  {
    var result = await mediator.Send(
      new CreateFiguraCommand(
        FiguraCode.From(req.Code),
        FiguraName.From(req.Name),
        ApplicazioneId.From(req.ApplicazioneId),
        req.Description,
        req.RuoloIds),
      ct);

    return result.ToCreatedResult(
      dto => $"/Figure/{dto.Value}",
      dto => new CreateFiguraResponse(dto.Value));
  }
}

public sealed class CreateFiguraRequest
{
  public string Code { get; init; } = string.Empty;
  public string Name { get; init; } = string.Empty;
  public Guid ApplicazioneId { get; init; }
  public string? Description { get; init; }
  public List<Guid>? RuoloIds { get; init; }
}

public sealed class CreateFiguraValidator : Validator<CreateFiguraRequest>
{
  public CreateFiguraValidator()
  {
    RuleFor(x => x.Code).NotEmpty();
    RuleFor(x => x.Name).NotEmpty();
    RuleFor(x => x.ApplicazioneId).NotEmpty();
  }
}

public sealed record CreateFiguraResponse(Guid Id);
