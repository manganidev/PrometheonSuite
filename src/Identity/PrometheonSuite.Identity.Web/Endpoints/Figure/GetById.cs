using Microsoft.AspNetCore.Http.HttpResults;
using PrometheonSuite.Identity.Entities.FiguraAggregate;
using PrometheonSuite.Identity.UseCases.Figuras;
using PrometheonSuite.Identity.UseCases.Figuras.Get;
using PrometheonSuite.Identity.Web.Extensions;

namespace PrometheonSuite.Identity.Web.Endpoints.Figure;

public class GetById(IMediator mediator)
  : Endpoint<GetFiguraRequest, Results<Ok<FiguraResponse>, NotFound, ProblemHttpResult>>
{
  public override void Configure()
  {
    Get("/Figure/{FiguraId:guid}");
    Policies("RequireAuthenticatedUser");
    Tags("Figure");

    Summary(s =>
    {
      s.Summary = "Get figure by id";
      s.Description = "Retrieves a single figure by its identifier.";
    });
  }

  public override async Task<Results<Ok<FiguraResponse>, NotFound, ProblemHttpResult>> ExecuteAsync(GetFiguraRequest req, CancellationToken ct)
  {
    var result = await mediator.Send(new GetFiguraQuery(FiguraId.From(req.FiguraId)), ct);
    return result.ToGetByIdResult(MapFigura);
  }

  private static FiguraResponse MapFigura(FiguraDto dto)
    => new(dto.Id.Value, dto.Code.Value, dto.Name.Value, dto.ApplicazioneId.Value, dto.Description, dto.RoleIds);
}

public sealed class GetFiguraRequest
{
  public Guid FiguraId { get; init; }
}

public sealed record FiguraResponse(Guid Id, string Code, string Name, Guid ApplicazioneId, string? Description, IReadOnlyList<Guid> RuoloIds);
