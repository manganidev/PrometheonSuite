using FluentValidation;
using Microsoft.AspNetCore.Http.HttpResults;
using PrometheonSuite.Identity.Entities.ApplicazioneAggregate;
using PrometheonSuite.Identity.Entities.AziendaAggregate;
using PrometheonSuite.Identity.UseCases.AziendaApplicazioni;
using PrometheonSuite.Identity.UseCases.AziendaApplicazioni.Create;
using PrometheonSuite.Identity.Web.Extensions;

namespace PrometheonSuite.Identity.Web.Endpoints.AziendaApplicazioni;

public class Create(IMediator mediator)
  : Endpoint<CreateAziendaApplicazioneRequest, Results<Created<AziendaApplicazioneResponse>, ValidationProblem, ProblemHttpResult>>
{
  public override void Configure()
  {
    Post("/AziendaApplicazioni");
    Policies("RequireAuthenticatedUser");
    Tags("AziendaApplicazioni");
  }

  public override async Task<Results<Created<AziendaApplicazioneResponse>, ValidationProblem, ProblemHttpResult>> ExecuteAsync(CreateAziendaApplicazioneRequest req, CancellationToken ct)
    => (await mediator.Send(new CreateAziendaApplicazioneCommand(AziendaId.From(req.AziendaId), ApplicazioneId.From(req.ApplicazioneId)), ct))
      .ToCreatedResult(x => $"/AziendaApplicazioni/{x.Id.Value}", MapItem);

  private static AziendaApplicazioneResponse MapItem(AziendaApplicazioneDto dto)
    => new(dto.Id.Value, dto.AziendaId.Value, dto.ApplicazioneId.Value, dto.IsActive);
}

public sealed class CreateAziendaApplicazioneRequest
{
  public Guid AziendaId { get; init; }
  public Guid ApplicazioneId { get; init; }
}

public sealed class CreateAziendaApplicazioneValidator : Validator<CreateAziendaApplicazioneRequest>
{
  public CreateAziendaApplicazioneValidator()
  {
    RuleFor(x => x.AziendaId).NotEmpty();
    RuleFor(x => x.ApplicazioneId).NotEmpty();
  }
}
