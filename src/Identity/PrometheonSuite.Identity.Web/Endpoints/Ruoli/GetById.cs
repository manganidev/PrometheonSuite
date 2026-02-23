using Microsoft.AspNetCore.Http.HttpResults;
using PrometheonSuite.Identity.Entities.RuoloAggregate;
using PrometheonSuite.Identity.UseCases.Ruolos;
using PrometheonSuite.Identity.UseCases.Ruolos.Get;
using PrometheonSuite.Identity.Web.Extensions;

namespace PrometheonSuite.Identity.Web.Endpoints.Ruoli;

public class GetById(IMediator mediator)
  : Endpoint<RuoloIdRequest, Results<Ok<RuoloResponse>, NotFound, ProblemHttpResult>>
{
  public override void Configure()
  {
    Get("/Ruoli/{RuoloId:guid}");
    Policies("RequireAuthenticatedUser");
    Tags("Ruoli");

    Summary(s =>
    {
      s.Summary = "Get ruoli by id";
      s.Description = "Retrieves a single ruoli by its identifier.";
    });
  }

  public override async Task<Results<Ok<RuoloResponse>, NotFound, ProblemHttpResult>> ExecuteAsync(RuoloIdRequest req, CancellationToken ct)
    => (await mediator.Send(new GetRuoloQuery(RuoloId.From(req.RuoloId)), ct)).ToGetByIdResult(MapRuolo);

  private static RuoloResponse MapRuolo(RuoloDto dto)
    => new(dto.Id.Value, dto.Code.Value, dto.Name.Value, dto.ApplicazioneId.Value, dto.Description);
}
