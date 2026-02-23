using FluentValidation;
using Microsoft.AspNetCore.Http.HttpResults;
using PrometheonSuite.Identity.Entities.FiguraAggregate;
using PrometheonSuite.Identity.UseCases.Figuras;
using PrometheonSuite.Identity.UseCases.Figuras.Update;
using PrometheonSuite.Identity.Web.Extensions;

namespace PrometheonSuite.Identity.Web.Endpoints.Figure;

public class Update(IMediator mediator)
  : Endpoint<UpdateFiguraRequest, Results<Ok<FiguraResponse>, NotFound, ProblemHttpResult>>
{
  public override void Configure()
  {
    Put("/Figure/{FiguraId:guid}");
    Policies("RequireAuthenticatedUser");
    Tags("Figure");
  }

  public override async Task<Results<Ok<FiguraResponse>, NotFound, ProblemHttpResult>> ExecuteAsync(UpdateFiguraRequest req, CancellationToken ct)
  {
    var result = await mediator.Send(new UpdateFiguraCommand(FiguraId.From(req.FiguraId), FiguraCode.From(req.Code), FiguraName.From(req.Name), req.Description), ct);
    return result.ToUpdateResult(MapFigura);
  }

  private static FiguraResponse MapFigura(FiguraDto dto)
    => new(dto.Id.Value, dto.Code.Value, dto.Name.Value, dto.ApplicazioneId.Value, dto.Description, dto.RoleIds);
}

public sealed class UpdateFiguraRequest
{
  public Guid FiguraId { get; init; }
  public string Code { get; init; } = string.Empty;
  public string Name { get; init; } = string.Empty;
  public string? Description { get; init; }
}

public sealed class UpdateFiguraValidator : Validator<UpdateFiguraRequest>
{
  public UpdateFiguraValidator()
  {
    RuleFor(x => x.Code).NotEmpty();
    RuleFor(x => x.Name).NotEmpty();
  }
}
