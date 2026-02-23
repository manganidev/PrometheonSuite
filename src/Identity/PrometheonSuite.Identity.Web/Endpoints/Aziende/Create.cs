using FluentValidation;
using Microsoft.AspNetCore.Http.HttpResults;
using PrometheonSuite.Identity.Entities.AziendaAggregate;
using PrometheonSuite.Identity.UseCases.Aziendas.Create;
using PrometheonSuite.Identity.Web.Extensions;

namespace PrometheonSuite.Identity.Web.Endpoints.Aziende;

public class Create(IMediator mediator)
  : Endpoint<CreateAziendaRequest, Results<Created<CreateAziendaResponse>, ValidationProblem, ProblemHttpResult>>
{
  public override void Configure()
  {
    Post("/Aziende");
    Policies("RequireAuthenticatedUser");
    Tags("Aziende");
  }

  public override async Task<Results<Created<CreateAziendaResponse>, ValidationProblem, ProblemHttpResult>> ExecuteAsync(CreateAziendaRequest req, CancellationToken ct)
  {
    var result = await mediator.Send(new CreateAziendaCommand(AziendaName.From(req.Name), AziendaCode.From(req.Code)), ct);
    return result.ToCreatedResult(x => $"/Aziende/{x.Value}", x => new CreateAziendaResponse(x.Value));
  }
}

public sealed class CreateAziendaRequest
{
  public string Name { get; init; } = string.Empty;
  public string Code { get; init; } = string.Empty;
}

public sealed class CreateAziendaValidator : Validator<CreateAziendaRequest>
{
  public CreateAziendaValidator()
  {
    RuleFor(x => x.Name).NotEmpty();
    RuleFor(x => x.Code).NotEmpty();
  }
}

public sealed record CreateAziendaResponse(Guid Id);
