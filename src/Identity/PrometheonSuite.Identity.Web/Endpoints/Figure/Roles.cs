using Microsoft.AspNetCore.Http.HttpResults;
using PrometheonSuite.Identity.Entities.FiguraAggregate;
using PrometheonSuite.Identity.Entities.RuoloAggregate;
using PrometheonSuite.Identity.UseCases.Figuras;
using PrometheonSuite.Identity.UseCases.Figuras.AddRuolo;
using PrometheonSuite.Identity.UseCases.Figuras.RemoveRuolo;
using PrometheonSuite.Identity.Web.Extensions;

namespace PrometheonSuite.Identity.Web.Endpoints.Figure;

public class AddRole(IMediator mediator)
  : Endpoint<FiguraRoleRequest, Results<Ok<FiguraResponse>, NotFound, ProblemHttpResult>>
{
  public override void Configure()
  {
    Post("/Figure/{FiguraId:guid}/Ruoli/{RuoloId:guid}");
    Policies("RequireAuthenticatedUser");
    Tags("Figure");
  }

  public override async Task<Results<Ok<FiguraResponse>, NotFound, ProblemHttpResult>> ExecuteAsync(FiguraRoleRequest req, CancellationToken ct)
  {
    var result = await mediator.Send(new AddRuoloToFiguraCommand(FiguraId.From(req.FiguraId), RuoloId.From(req.RuoloId)), ct);
    return result.ToUpdateResult(MapFigura);
  }

  private static FiguraResponse MapFigura(FiguraDto dto)
    => new(dto.Id.Value, dto.Code.Value, dto.Name.Value, dto.ApplicazioneId.Value, dto.Description, dto.RoleIds);
}

public class RemoveRole(IMediator mediator)
  : Endpoint<FiguraRoleRequest, Results<Ok<FiguraResponse>, NotFound, ProblemHttpResult>>
{
  public override void Configure()
  {
    Delete("/Figure/{FiguraId:guid}/Ruoli/{RuoloId:guid}");
    Policies("RequireAuthenticatedUser");
    Tags("Figure");
  }

  public override async Task<Results<Ok<FiguraResponse>, NotFound, ProblemHttpResult>> ExecuteAsync(FiguraRoleRequest req, CancellationToken ct)
  {
    var result = await mediator.Send(new RemoveRuoloFromFiguraCommand(FiguraId.From(req.FiguraId), RuoloId.From(req.RuoloId)), ct);
    return result.ToUpdateResult(MapFigura);
  }

  private static FiguraResponse MapFigura(FiguraDto dto)
    => new(dto.Id.Value, dto.Code.Value, dto.Name.Value, dto.ApplicazioneId.Value, dto.Description, dto.RoleIds);
}

public sealed class FiguraRoleRequest
{
  public Guid FiguraId { get; init; }
  public Guid RuoloId { get; init; }
}
