using FluentValidation;
using Microsoft.AspNetCore.Http.HttpResults;
using PrometheonSuite.Identity.Entities.AziendaAggregate;
using PrometheonSuite.Identity.UseCases.Aziendas;
using PrometheonSuite.Identity.UseCases.Aziendas.Create;
using PrometheonSuite.Identity.UseCases.Aziendas.Delete;
using PrometheonSuite.Identity.UseCases.Aziendas.Get;
using PrometheonSuite.Identity.UseCases.Aziendas.List;
using PrometheonSuite.Identity.UseCases.Aziendas.Update;
using PrometheonSuite.Identity.Web.Extensions;

namespace PrometheonSuite.Identity.Web.Endpoints.Aziende;

public sealed record AziendaResponse(Guid Id, string Name, string Code, bool IsActive);

public class List(IMediator mediator) : Endpoint<ListAziendeRequest, AziendaListResponse, AziendaListMapper>
{
  public override void Configure()
  {
    Get("/Aziende");
    Policies("RequireAuthenticatedUser");
    Tags("Aziende");
  }

  public override async Task HandleAsync(ListAziendeRequest req, CancellationToken ct)
  {
    var result = await mediator.Send(new ListAziendasQuery(req.Page, req.PerPage), ct);
    if (!result.IsSuccess)
    {
      await Send.ErrorsAsync(400, ct);
      return;
    }

    await Send.OkAsync(Map.FromEntity(result.Value), ct);
  }
}

public sealed class ListAziendeRequest
{
  [BindFrom("page")] public int Page { get; init; } = 1;
  [BindFrom("per_page")] public int PerPage { get; init; } = PrometheonSuite.Identity.UseCases.Constants.DEFAULT_PAGE_SIZE;
}

public sealed class ListAziendeValidator : Validator<ListAziendeRequest>
{
  public ListAziendeValidator()
  {
    RuleFor(x => x.Page).GreaterThanOrEqualTo(1);
    RuleFor(x => x.PerPage).InclusiveBetween(1, PrometheonSuite.Identity.UseCases.Constants.MAX_PAGE_SIZE);
  }
}

public sealed record AziendaListResponse : UseCases.PagedResult<AziendaResponse>
{
  public AziendaListResponse(IReadOnlyList<AziendaResponse> items, int page, int perPage, int totalCount, int totalPages) : base(items, page, perPage, totalCount, totalPages) { }
}

public sealed class AziendaListMapper : Mapper<ListAziendeRequest, AziendaListResponse, UseCases.PagedResult<AziendaDto>>
{
  public override AziendaListResponse FromEntity(UseCases.PagedResult<AziendaDto> e)
    => new(e.Items.Select(MapAzienda).ToList(), e.Page, e.PerPage, e.TotalCount, e.TotalPages);

  private static AziendaResponse MapAzienda(AziendaDto dto) => new(dto.Id.Value, dto.Name.Value, dto.Code.Value, dto.IsActive);
}

public class GetById(IMediator mediator) : Endpoint<AziendaIdRequest, Results<Ok<AziendaResponse>, NotFound, ProblemHttpResult>>
{
  public override void Configure()
  {
    Get("/Aziende/{AziendaId:guid}");
    Policies("RequireAuthenticatedUser");
    Tags("Aziende");
  }

  public override async Task<Results<Ok<AziendaResponse>, NotFound, ProblemHttpResult>> ExecuteAsync(AziendaIdRequest req, CancellationToken ct)
    => (await mediator.Send(new GetAziendaQuery(AziendaId.From(req.AziendaId)), ct)).ToGetByIdResult(MapAzienda);

  private static AziendaResponse MapAzienda(AziendaDto dto) => new(dto.Id.Value, dto.Name.Value, dto.Code.Value, dto.IsActive);
}

public class Create(IMediator mediator) : Endpoint<CreateAziendaRequest, Results<Created<CreateAziendaResponse>, ValidationProblem, ProblemHttpResult>>
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

public class Update(IMediator mediator) : Endpoint<UpdateAziendaRequest, Results<Ok<AziendaResponse>, NotFound, ProblemHttpResult>>
{
  public override void Configure()
  {
    Put("/Aziende/{AziendaId:guid}");
    Policies("RequireAuthenticatedUser");
    Tags("Aziende");
  }

  public override async Task<Results<Ok<AziendaResponse>, NotFound, ProblemHttpResult>> ExecuteAsync(UpdateAziendaRequest req, CancellationToken ct)
    => (await mediator.Send(new UpdateAziendaCommand(AziendaId.From(req.AziendaId), AziendaName.From(req.Name), AziendaCode.From(req.Code)), ct)).ToUpdateResult(MapAzienda);

  private static AziendaResponse MapAzienda(AziendaDto dto) => new(dto.Id.Value, dto.Name.Value, dto.Code.Value, dto.IsActive);
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

public class Delete(IMediator mediator) : Endpoint<AziendaIdRequest, Results<NoContent, NotFound, ProblemHttpResult>>
{
  public override void Configure()
  {
    Delete("/Aziende/{AziendaId:guid}");
    Policies("RequireAuthenticatedUser");
    Tags("Aziende");
  }

  public override async Task<Results<NoContent, NotFound, ProblemHttpResult>> ExecuteAsync(AziendaIdRequest req, CancellationToken ct)
    => (await mediator.Send(new DeleteAziendaCommand(AziendaId.From(req.AziendaId)), ct)).ToDeleteResult();
}

public sealed class AziendaIdRequest
{
  public Guid AziendaId { get; init; }
}
