using FluentValidation;
using Microsoft.AspNetCore.Http.HttpResults;
using PrometheonSuite.Identity.Entities.AziendaAggregate;
using PrometheonSuite.Identity.UseCases.Aziendas;
using PrometheonSuite.Identity.UseCases.Aziendas.Update;
using PrometheonSuite.Identity.Web.Extensions;

namespace PrometheonSuite.Identity.Web.Endpoints.Aziende;

public class Update(IMediator mediator)
  : Endpoint<UpdateAziendaRequest, Results<Ok<AziendaResponse>, NotFound, ProblemHttpResult>>
{
  public override void Configure()
  {
    Put("/Aziende/{AziendaId:guid}");
    Policies("RequireAuthenticatedUser");
    Tags("Aziende");
  }

  public override async Task<Results<Ok<AziendaResponse>, NotFound, ProblemHttpResult>> ExecuteAsync(UpdateAziendaRequest req, CancellationToken ct)
    => (await mediator.Send(new UpdateAziendaCommand(AziendaId.From(req.AziendaId), AziendaName.From(req.Name), AziendaCode.From(req.Code)), ct))
      .ToUpdateResult(MapAzienda);

  private static AziendaResponse MapAzienda(AziendaDto dto)
    => new(dto.Id.Value, dto.Name.Value, dto.Code.Value, dto.IsActive);
}

public sealed class UpdateAziendaRequest
{
  public Guid AziendaId { get; init; }
  public string Name { get; init; } = string.Empty;
  public string Code { get; init; } = string.Empty;
}

public sealed class UpdateAziendaValidator : Validator<UpdateAziendaRequest>
{
  public UpdateAziendaValidator()
  {
    RuleFor(x => x.Name).NotEmpty();
    RuleFor(x => x.Code).NotEmpty();
  }
}
