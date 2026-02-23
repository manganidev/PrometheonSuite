using FluentValidation;
using Microsoft.AspNetCore.Http.HttpResults;
using PrometheonSuite.Identity.Entities.ApplicazioneAggregate;
using PrometheonSuite.Identity.Entities.RuoloAggregate;
using PrometheonSuite.Identity.UseCases.Ruolos;
using PrometheonSuite.Identity.UseCases.Ruolos.Create;
using PrometheonSuite.Identity.UseCases.Ruolos.Delete;
using PrometheonSuite.Identity.UseCases.Ruolos.Get;
using PrometheonSuite.Identity.UseCases.Ruolos.List;
using PrometheonSuite.Identity.UseCases.Ruolos.Update;
using PrometheonSuite.Identity.Web.Extensions;

namespace PrometheonSuite.Identity.Web.Endpoints.Ruoli;

public sealed record RuoloResponse(Guid Id, string Code, string Name, Guid ApplicazioneId, string? Description);

public class List(IMediator mediator) : Endpoint<ListRuoliRequest, RuoloListResponse, RuoloListMapper>
{
  public override void Configure()
  {
    Get("/Ruoli");
    Policies("RequireAuthenticatedUser");
    Tags("Ruoli");
  }

  public override async Task HandleAsync(ListRuoliRequest req, CancellationToken ct)
  {
    var result = await mediator.Send(new ListRuolosQuery(req.Page, req.PerPage), ct);
    if (!result.IsSuccess)
    {
      await Send.ErrorsAsync(400, ct);
      return;
    }

    await Send.OkAsync(Map.FromEntity(result.Value), ct);
  }
}

public sealed class ListRuoliRequest
{
  [BindFrom("page")] public int Page { get; init; } = 1;
  [BindFrom("per_page")] public int PerPage { get; init; } = PrometheonSuite.Identity.UseCases.Constants.DEFAULT_PAGE_SIZE;
}

public sealed class ListRuoliValidator : Validator<ListRuoliRequest>
{
  public ListRuoliValidator()
  {
    RuleFor(x => x.Page).GreaterThanOrEqualTo(1);
    RuleFor(x => x.PerPage).InclusiveBetween(1, PrometheonSuite.Identity.UseCases.Constants.MAX_PAGE_SIZE);
  }
}

public sealed record RuoloListResponse : UseCases.PagedResult<RuoloResponse>
{
  public RuoloListResponse(IReadOnlyList<RuoloResponse> items, int page, int perPage, int totalCount, int totalPages) : base(items, page, perPage, totalCount, totalPages) { }
}

public sealed class RuoloListMapper : Mapper<ListRuoliRequest, RuoloListResponse, UseCases.PagedResult<RuoloDto>>
{
  public override RuoloListResponse FromEntity(UseCases.PagedResult<RuoloDto> e)
    => new(e.Items.Select(MapRuolo).ToList(), e.Page, e.PerPage, e.TotalCount, e.TotalPages);

  private static RuoloResponse MapRuolo(RuoloDto dto) => new(dto.Id.Value, dto.Code.Value, dto.Name.Value, dto.ApplicazioneId.Value, dto.Description);
}

public class GetById(IMediator mediator) : Endpoint<RuoloIdRequest, Results<Ok<RuoloResponse>, NotFound, ProblemHttpResult>>
{
  public override void Configure()
  {
    Get("/Ruoli/{RuoloId:guid}");
    Policies("RequireAuthenticatedUser");
    Tags("Ruoli");
  }

  public override async Task<Results<Ok<RuoloResponse>, NotFound, ProblemHttpResult>> ExecuteAsync(RuoloIdRequest req, CancellationToken ct)
    => (await mediator.Send(new GetRuoloQuery(RuoloId.From(req.RuoloId)), ct)).ToGetByIdResult(MapRuolo);

  private static RuoloResponse MapRuolo(RuoloDto dto) => new(dto.Id.Value, dto.Code.Value, dto.Name.Value, dto.ApplicazioneId.Value, dto.Description);
}

public class Create(IMediator mediator) : Endpoint<CreateRuoloRequest, Results<Created<CreateRuoloResponse>, ValidationProblem, ProblemHttpResult>>
{
  public override void Configure()
  {
    Post("/Ruoli");
    Policies("RequireAuthenticatedUser");
    Tags("Ruoli");
  }

  public override async Task<Results<Created<CreateRuoloResponse>, ValidationProblem, ProblemHttpResult>> ExecuteAsync(CreateRuoloRequest req, CancellationToken ct)
  {
    var result = await mediator.Send(new CreateRuoloCommand(RuoloCode.From(req.Code), RuoloName.From(req.Name), ApplicazioneId.From(req.ApplicazioneId), req.Description), ct);
    return result.ToCreatedResult(x => $"/Ruoli/{x.Value}", x => new CreateRuoloResponse(x.Value));
  }
}

public sealed class CreateRuoloRequest
{
  public string Code { get; init; } = string.Empty;
  public string Name { get; init; } = string.Empty;
  public Guid ApplicazioneId { get; init; }
  public string? Description { get; init; }
}

public sealed class CreateRuoloValidator : Validator<CreateRuoloRequest>
{
  public CreateRuoloValidator()
  {
    RuleFor(x => x.Code).NotEmpty();
    RuleFor(x => x.Name).NotEmpty();
    RuleFor(x => x.ApplicazioneId).NotEmpty();
  }
}

public sealed record CreateRuoloResponse(Guid Id);

public class Update(IMediator mediator) : Endpoint<UpdateRuoloRequest, Results<Ok<RuoloResponse>, NotFound, ProblemHttpResult>>
{
  public override void Configure()
  {
    Put("/Ruoli/{RuoloId:guid}");
    Policies("RequireAuthenticatedUser");
    Tags("Ruoli");
  }

  public override async Task<Results<Ok<RuoloResponse>, NotFound, ProblemHttpResult>> ExecuteAsync(UpdateRuoloRequest req, CancellationToken ct)
    => (await mediator.Send(new UpdateRuoloCommand(RuoloId.From(req.RuoloId), RuoloCode.From(req.Code), RuoloName.From(req.Name), req.Description), ct)).ToUpdateResult(MapRuolo);

  private static RuoloResponse MapRuolo(RuoloDto dto) => new(dto.Id.Value, dto.Code.Value, dto.Name.Value, dto.ApplicazioneId.Value, dto.Description);
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

public class Delete(IMediator mediator) : Endpoint<RuoloIdRequest, Results<NoContent, NotFound, ProblemHttpResult>>
{
  public override void Configure()
  {
    Delete("/Ruoli/{RuoloId:guid}");
    Policies("RequireAuthenticatedUser");
    Tags("Ruoli");
  }

  public override async Task<Results<NoContent, NotFound, ProblemHttpResult>> ExecuteAsync(RuoloIdRequest req, CancellationToken ct)
    => (await mediator.Send(new DeleteRuoloCommand(RuoloId.From(req.RuoloId)), ct)).ToDeleteResult();
}

public sealed class RuoloIdRequest
{
  public Guid RuoloId { get; init; }
}
