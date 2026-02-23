using Microsoft.AspNetCore.Http.HttpResults;
using PrometheonSuite.Identity.Entities.AziendaAggregate;
using PrometheonSuite.Identity.UseCases.Aziendas;
using PrometheonSuite.Identity.UseCases.Aziendas.Get;
using PrometheonSuite.Identity.Web.Extensions;

namespace PrometheonSuite.Identity.Web.Endpoints.Aziende;

public class GetById(IMediator mediator)
  : Endpoint<AziendaIdRequest, Results<Ok<AziendaResponse>, NotFound, ProblemHttpResult>>
{
  public override void Configure()
  {
    Get("/Aziende/{AziendaId:guid}");
    Policies("RequireAuthenticatedUser");
    Tags("Aziende");

    Summary(s =>
    {
      s.Summary = "Get aziende by id";
      s.Description = "Retrieves a single aziende by its identifier.";
    });
  }

  public override async Task<Results<Ok<AziendaResponse>, NotFound, ProblemHttpResult>> ExecuteAsync(AziendaIdRequest req, CancellationToken ct)
    => (await mediator.Send(new GetAziendaQuery(AziendaId.From(req.AziendaId)), ct)).ToGetByIdResult(MapAzienda);

  private static AziendaResponse MapAzienda(AziendaDto dto)
    => new(dto.Id.Value, dto.Name.Value, dto.Code.Value, dto.IsActive);
}
