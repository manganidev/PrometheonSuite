using FluentValidation;
using Microsoft.AspNetCore.Http.HttpResults;
using PrometheonSuite.Identity.Entities.ApplicazioneAggregate;
using PrometheonSuite.Identity.Entities.RuoloAggregate;
using PrometheonSuite.Identity.UseCases.Ruolos.Create;
using PrometheonSuite.Identity.Web.Extensions;

namespace PrometheonSuite.Identity.Web.Endpoints.Ruoli;

public class Create(IMediator mediator)
  : Endpoint<CreateRuoloRequest, Results<Created<CreateRuoloResponse>, ValidationProblem, ProblemHttpResult>>
{
  public override void Configure()
  {
    Post("/Ruoli");
    Policies("RequireAuthenticatedUser");
    Tags("Ruoli");
  }

  public override async Task<Results<Created<CreateRuoloResponse>, ValidationProblem, ProblemHttpResult>> ExecuteAsync(CreateRuoloRequest req, CancellationToken ct)
  {
    var result = await mediator.Send(
      new CreateRuoloCommand(RuoloCode.From(req.Code), RuoloName.From(req.Name), ApplicazioneId.From(req.ApplicazioneId), req.Description),
      ct);

    return result.ToCreatedResult(x => $"/Ruoli/{x.Value}", x => new CreateRuoloResponse(x.Value));
  }
}

public sealed class CreateRuoloRequest
{
  public string Code { get; init; } = string.Empty;
  public string Name { get; init; } = string.Empty;
  public Guid ApplicazioneId { get; init; }
  public string? Description { get; init; }
}

public sealed class CreateRuoloValidator : Validator<CreateRuoloRequest>
{
  public CreateRuoloValidator()
  {
    RuleFor(x => x.Code).NotEmpty();
    RuleFor(x => x.Name).NotEmpty();
    RuleFor(x => x.ApplicazioneId).NotEmpty();
  }
}

public sealed record CreateRuoloResponse(Guid Id);
