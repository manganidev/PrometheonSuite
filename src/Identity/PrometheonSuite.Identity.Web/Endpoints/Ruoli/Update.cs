using FluentValidation;
using Microsoft.AspNetCore.Http.HttpResults;
using PrometheonSuite.Identity.Entities.RuoloAggregate;
using PrometheonSuite.Identity.UseCases.Ruolos;
using PrometheonSuite.Identity.UseCases.Ruolos.Update;
using PrometheonSuite.Identity.Web.Extensions;

namespace PrometheonSuite.Identity.Web.Endpoints.Ruoli;

public class Update(IMediator mediator)
  : Endpoint<UpdateRuoloRequest, Results<Ok<RuoloResponse>, NotFound, ProblemHttpResult>>
{
  public override void Configure()
  {
    Put("/Ruoli/{RuoloId:guid}");
    Policies("RequireAuthenticatedUser");
    Tags("Ruoli");

    Summary(s =>
    {
      s.Summary = "Update ruoli";
      s.Description = "Updates an existing ruoli.";
    });
  }

  public override async Task<Results<Ok<RuoloResponse>, NotFound, ProblemHttpResult>> ExecuteAsync(UpdateRuoloRequest req, CancellationToken ct)
    => (await mediator.Send(new UpdateRuoloCommand(RuoloId.From(req.RuoloId), RuoloCode.From(req.Code), RuoloName.From(req.Name), req.Description), ct))
      .ToUpdateResult(MapRuolo);

  private static RuoloResponse MapRuolo(RuoloDto dto)
    => new(dto.Id.Value, dto.Code.Value, dto.Name.Value, dto.ApplicazioneId.Value, dto.Description);
}

public sealed class UpdateRuoloRequest
{
  public Guid RuoloId { get; init; }
  public string Code { get; init; } = string.Empty;
  public string Name { get; init; } = string.Empty;
  public string? Description { get; init; }
}

public sealed class UpdateRuoloValidator : Validator<UpdateRuoloRequest>
{
  public UpdateRuoloValidator()
  {
    RuleFor(x => x.Code).NotEmpty();
    RuleFor(x => x.Name).NotEmpty();
  }
}
