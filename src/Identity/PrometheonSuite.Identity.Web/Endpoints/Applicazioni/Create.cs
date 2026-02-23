using FluentValidation;
using Microsoft.AspNetCore.Http.HttpResults;
using PrometheonSuite.Identity.Entities.ApplicazioneAggregate;
using PrometheonSuite.Identity.UseCases.Applicazioni.Create;
using PrometheonSuite.Identity.Web.Extensions;

namespace PrometheonSuite.Identity.Web.Endpoints.Applicazioni;

public class Create(IMediator mediator)
  : Endpoint<CreateApplicazioneRequest, Results<Created<CreateApplicazioneResponse>, ValidationProblem, ProblemHttpResult>>
{
  public override void Configure()
  {
    Post("/Applicazioni");
    Policies("RequireAuthenticatedUser");
    Tags("Applicazioni");
  }

  public override async Task<Results<Created<CreateApplicazioneResponse>, ValidationProblem, ProblemHttpResult>> ExecuteAsync(CreateApplicazioneRequest req, CancellationToken ct)
    => (await mediator.Send(new CreateApplicazioneCommand(ApplicazioneName.From(req.Name), ApplicazioneCode.From(req.Code)), ct))
      .ToCreatedResult(x => $"/Applicazioni/{x.Value}", x => new CreateApplicazioneResponse(x.Value));
}

public sealed class CreateApplicazioneRequest
{
  public string Name { get; init; } = string.Empty;
  public string Code { get; init; } = string.Empty;
}

public sealed class CreateApplicazioneValidator : Validator<CreateApplicazioneRequest>
{
  public CreateApplicazioneValidator()
  {
    RuleFor(x => x.Name).NotEmpty();
    RuleFor(x => x.Code).NotEmpty();
  }
}

public sealed record CreateApplicazioneResponse(Guid Id);
