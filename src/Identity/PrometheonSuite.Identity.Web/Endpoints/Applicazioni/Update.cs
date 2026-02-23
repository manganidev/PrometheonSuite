using FluentValidation;
using Microsoft.AspNetCore.Http.HttpResults;
using PrometheonSuite.Identity.Entities.ApplicazioneAggregate;
using PrometheonSuite.Identity.UseCases.Applicazioni;
using PrometheonSuite.Identity.UseCases.Applicazioni.Update;
using PrometheonSuite.Identity.Web.Extensions;

namespace PrometheonSuite.Identity.Web.Endpoints.Applicazioni;

public class Update(IMediator mediator)
  : Endpoint<UpdateApplicazioneRequest, Results<Ok<ApplicazioneResponse>, NotFound, ProblemHttpResult>>
{
  public override void Configure()
  {
    Put("/Applicazioni/{ApplicazioneId:guid}");
    Policies("RequireAuthenticatedUser");
    Tags("Applicazioni");

    Summary(s =>
    {
      s.Summary = "Update applicazioni";
      s.Description = "Updates an existing applicazioni.";
    });
  }

  public override async Task<Results<Ok<ApplicazioneResponse>, NotFound, ProblemHttpResult>> ExecuteAsync(UpdateApplicazioneRequest req, CancellationToken ct)
    => (await mediator.Send(new UpdateApplicazioneCommand(ApplicazioneId.From(req.ApplicazioneId), ApplicazioneName.From(req.Name), ApplicazioneCode.From(req.Code)), ct))
      .ToUpdateResult(MapApplicazione);

  private static ApplicazioneResponse MapApplicazione(ApplicazioneDto dto)
    => new(dto.Id.Value, dto.Name.Value, dto.Code.Value, dto.IsActive);
}

public sealed class UpdateApplicazioneRequest
{
  public Guid ApplicazioneId { get; init; }
  public string Name { get; init; } = string.Empty;
  public string Code { get; init; } = string.Empty;
}

public sealed class UpdateApplicazioneValidator : Validator<UpdateApplicazioneRequest>
{
  public UpdateApplicazioneValidator()
  {
    RuleFor(x => x.Name).NotEmpty();
    RuleFor(x => x.Code).NotEmpty();
  }
}
