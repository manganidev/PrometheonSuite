using FluentValidation;
using Microsoft.AspNetCore.Http.HttpResults;
using PrometheonSuite.Identity.Entities.ApplicazioneAggregate;
using PrometheonSuite.Identity.Entities.AziendaAggregate;
using PrometheonSuite.Identity.Entities.AziendaApplicazioneAggregate;
using PrometheonSuite.Identity.UseCases.AziendaApplicazioni;
using PrometheonSuite.Identity.UseCases.AziendaApplicazioni.Create;
using PrometheonSuite.Identity.UseCases.AziendaApplicazioni.Delete;
using PrometheonSuite.Identity.UseCases.AziendaApplicazioni.Get;
using PrometheonSuite.Identity.UseCases.AziendaApplicazioni.List;
using PrometheonSuite.Identity.Web.Extensions;

namespace PrometheonSuite.Identity.Web.Endpoints.AziendaApplicazioni;

public sealed record AziendaApplicazioneResponse(Guid Id, Guid AziendaId, Guid ApplicazioneId, bool IsActive);

public class List(IMediator mediator) : Endpoint<ListAziendaApplicazioniRequest, AziendaApplicazioneListResponse, AziendaApplicazioneListMapper>
{
  public override void Configure() { Get("/AziendaApplicazioni"); Policies("RequireAuthenticatedUser"); Tags("AziendaApplicazioni"); }
  public override async Task HandleAsync(ListAziendaApplicazioniRequest req, CancellationToken ct)
  {
    var result = await mediator.Send(new ListAziendaApplicazioniQuery(req.Page, req.PerPage), ct);
    if (!result.IsSuccess) { await Send.ErrorsAsync(400, ct); return; }
    await Send.OkAsync(Map.FromEntity(result.Value), ct);
  }
}

public sealed class ListAziendaApplicazioniRequest { [BindFrom("page")] public int Page { get; init; } = 1; [BindFrom("per_page")] public int PerPage { get; init; } = PrometheonSuite.Identity.UseCases.Constants.DEFAULT_PAGE_SIZE; }
public sealed class ListAziendaApplicazioniValidator : Validator<ListAziendaApplicazioniRequest> { public ListAziendaApplicazioniValidator() { RuleFor(x => x.Page).GreaterThanOrEqualTo(1); RuleFor(x => x.PerPage).InclusiveBetween(1, PrometheonSuite.Identity.UseCases.Constants.MAX_PAGE_SIZE); } }
public sealed record AziendaApplicazioneListResponse : UseCases.PagedResult<AziendaApplicazioneResponse> { public AziendaApplicazioneListResponse(IReadOnlyList<AziendaApplicazioneResponse> items, int page, int perPage, int totalCount, int totalPages) : base(items, page, perPage, totalCount, totalPages) { } }
public sealed class AziendaApplicazioneListMapper : Mapper<ListAziendaApplicazioniRequest, AziendaApplicazioneListResponse, UseCases.PagedResult<AziendaApplicazioneDto>>
{ public override AziendaApplicazioneListResponse FromEntity(UseCases.PagedResult<AziendaApplicazioneDto> e) => new(e.Items.Select(MapItem).ToList(), e.Page, e.PerPage, e.TotalCount, e.TotalPages); private static AziendaApplicazioneResponse MapItem(AziendaApplicazioneDto dto) => new(dto.Id.Value, dto.AziendaId.Value, dto.ApplicazioneId.Value, dto.IsActive); }

public class GetById(IMediator mediator) : Endpoint<AziendaApplicazioneIdRequest, Results<Ok<AziendaApplicazioneResponse>, NotFound, ProblemHttpResult>>
{ public override void Configure() { Get("/AziendaApplicazioni/{AziendaApplicazioneId:guid}"); Policies("RequireAuthenticatedUser"); Tags("AziendaApplicazioni"); }
  public override async Task<Results<Ok<AziendaApplicazioneResponse>, NotFound, ProblemHttpResult>> ExecuteAsync(AziendaApplicazioneIdRequest req, CancellationToken ct)
    => (await mediator.Send(new GetAziendaApplicazioneQuery(AziendaApplicazioneId.From(req.AziendaApplicazioneId)), ct)).ToGetByIdResult(MapItem);
  private static AziendaApplicazioneResponse MapItem(AziendaApplicazioneDto dto) => new(dto.Id.Value, dto.AziendaId.Value, dto.ApplicazioneId.Value, dto.IsActive);
}

public class Create(IMediator mediator) : Endpoint<CreateAziendaApplicazioneRequest, Results<Created<AziendaApplicazioneResponse>, ValidationProblem, ProblemHttpResult>>
{ public override void Configure() { Post("/AziendaApplicazioni"); Policies("RequireAuthenticatedUser"); Tags("AziendaApplicazioni"); }
  public override async Task<Results<Created<AziendaApplicazioneResponse>, ValidationProblem, ProblemHttpResult>> ExecuteAsync(CreateAziendaApplicazioneRequest req, CancellationToken ct)
    => (await mediator.Send(new CreateAziendaApplicazioneCommand(AziendaId.From(req.AziendaId), ApplicazioneId.From(req.ApplicazioneId)), ct)).ToCreatedResult(x => $"/AziendaApplicazioni/{x.Id.Value}", MapItem);
  private static AziendaApplicazioneResponse MapItem(AziendaApplicazioneDto dto) => new(dto.Id.Value, dto.AziendaId.Value, dto.ApplicazioneId.Value, dto.IsActive);
}

public sealed class CreateAziendaApplicazioneRequest { public Guid AziendaId { get; init; } public Guid ApplicazioneId { get; init; } }
public sealed class CreateAziendaApplicazioneValidator : Validator<CreateAziendaApplicazioneRequest> { public CreateAziendaApplicazioneValidator() { RuleFor(x => x.AziendaId).NotEmpty(); RuleFor(x => x.ApplicazioneId).NotEmpty(); } }

public class Delete(IMediator mediator) : Endpoint<AziendaApplicazioneIdRequest, Results<NoContent, NotFound, ProblemHttpResult>>
{ public override void Configure() { Delete("/AziendaApplicazioni/{AziendaApplicazioneId:guid}"); Policies("RequireAuthenticatedUser"); Tags("AziendaApplicazioni"); }
  public override async Task<Results<NoContent, NotFound, ProblemHttpResult>> ExecuteAsync(AziendaApplicazioneIdRequest req, CancellationToken ct)
    => (await mediator.Send(new DeleteAziendaApplicazioneCommand(AziendaApplicazioneId.From(req.AziendaApplicazioneId)), ct)).ToDeleteResult(); }

public sealed class AziendaApplicazioneIdRequest { public Guid AziendaApplicazioneId { get; init; } }
