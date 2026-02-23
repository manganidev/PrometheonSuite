using FluentValidation;
using Microsoft.AspNetCore.Http.HttpResults;
using PrometheonSuite.Identity.Entities.ApplicazioneAggregate;
using PrometheonSuite.Identity.UseCases.Applicazioni;
using PrometheonSuite.Identity.UseCases.Applicazioni.Create;
using PrometheonSuite.Identity.UseCases.Applicazioni.Delete;
using PrometheonSuite.Identity.UseCases.Applicazioni.Get;
using PrometheonSuite.Identity.UseCases.Applicazioni.List;
using PrometheonSuite.Identity.UseCases.Applicazioni.Update;
using PrometheonSuite.Identity.Web.Extensions;

namespace PrometheonSuite.Identity.Web.Endpoints.Applicazioni;

public sealed record ApplicazioneResponse(Guid Id, string Name, string Code, bool IsActive);

public class List(IMediator mediator) : Endpoint<ListApplicazioniRequest, ApplicazioneListResponse, ApplicazioneListMapper>
{
  public override void Configure() { Get("/Applicazioni"); Policies("RequireAuthenticatedUser"); Tags("Applicazioni"); }
  public override async Task HandleAsync(ListApplicazioniRequest req, CancellationToken ct)
  {
    var result = await mediator.Send(new ListApplicazioniQuery(req.Page, req.PerPage), ct);
    if (!result.IsSuccess) { await Send.ErrorsAsync(400, ct); return; }
    await Send.OkAsync(Map.FromEntity(result.Value), ct);
  }
}

public sealed class ListApplicazioniRequest { [BindFrom("page")] public int Page { get; init; } = 1; [BindFrom("per_page")] public int PerPage { get; init; } = PrometheonSuite.Identity.UseCases.Constants.DEFAULT_PAGE_SIZE; }
public sealed class ListApplicazioniValidator : Validator<ListApplicazioniRequest> { public ListApplicazioniValidator() { RuleFor(x => x.Page).GreaterThanOrEqualTo(1); RuleFor(x => x.PerPage).InclusiveBetween(1, PrometheonSuite.Identity.UseCases.Constants.MAX_PAGE_SIZE); } }
public sealed record ApplicazioneListResponse : UseCases.PagedResult<ApplicazioneResponse> { public ApplicazioneListResponse(IReadOnlyList<ApplicazioneResponse> items, int page, int perPage, int totalCount, int totalPages) : base(items, page, perPage, totalCount, totalPages) { } }
public sealed class ApplicazioneListMapper : Mapper<ListApplicazioniRequest, ApplicazioneListResponse, UseCases.PagedResult<ApplicazioneDto>>
{ public override ApplicazioneListResponse FromEntity(UseCases.PagedResult<ApplicazioneDto> e) => new(e.Items.Select(MapApp).ToList(), e.Page, e.PerPage, e.TotalCount, e.TotalPages); private static ApplicazioneResponse MapApp(ApplicazioneDto dto) => new(dto.Id.Value, dto.Name.Value, dto.Code.Value, dto.IsActive); }

public class GetById(IMediator mediator) : Endpoint<ApplicazioneIdRequest, Results<Ok<ApplicazioneResponse>, NotFound, ProblemHttpResult>>
{ public override void Configure() { Get("/Applicazioni/{ApplicazioneId:guid}"); Policies("RequireAuthenticatedUser"); Tags("Applicazioni"); }
  public override async Task<Results<Ok<ApplicazioneResponse>, NotFound, ProblemHttpResult>> ExecuteAsync(ApplicazioneIdRequest req, CancellationToken ct)
    => (await mediator.Send(new GetApplicazioneQuery(ApplicazioneId.From(req.ApplicazioneId)), ct)).ToGetByIdResult(MapApp);
  private static ApplicazioneResponse MapApp(ApplicazioneDto dto) => new(dto.Id.Value, dto.Name.Value, dto.Code.Value, dto.IsActive);
}

public class Create(IMediator mediator) : Endpoint<CreateApplicazioneRequest, Results<Created<CreateApplicazioneResponse>, ValidationProblem, ProblemHttpResult>>
{ public override void Configure() { Post("/Applicazioni"); Policies("RequireAuthenticatedUser"); Tags("Applicazioni"); }
  public override async Task<Results<Created<CreateApplicazioneResponse>, ValidationProblem, ProblemHttpResult>> ExecuteAsync(CreateApplicazioneRequest req, CancellationToken ct)
    => (await mediator.Send(new CreateApplicazioneCommand(ApplicazioneName.From(req.Name), ApplicazioneCode.From(req.Code)), ct)).ToCreatedResult(x => $"/Applicazioni/{x.Value}", x => new CreateApplicazioneResponse(x.Value)); }

public sealed class CreateApplicazioneRequest { public string Name { get; init; } = string.Empty; public string Code { get; init; } = string.Empty; }
public sealed class CreateApplicazioneValidator : Validator<CreateApplicazioneRequest> { public CreateApplicazioneValidator() { RuleFor(x => x.Name).NotEmpty(); RuleFor(x => x.Code).NotEmpty(); } }
public sealed record CreateApplicazioneResponse(Guid Id);

public class Update(IMediator mediator) : Endpoint<UpdateApplicazioneRequest, Results<Ok<ApplicazioneResponse>, NotFound, ProblemHttpResult>>
{ public override void Configure() { Put("/Applicazioni/{ApplicazioneId:guid}"); Policies("RequireAuthenticatedUser"); Tags("Applicazioni"); }
  public override async Task<Results<Ok<ApplicazioneResponse>, NotFound, ProblemHttpResult>> ExecuteAsync(UpdateApplicazioneRequest req, CancellationToken ct)
    => (await mediator.Send(new UpdateApplicazioneCommand(ApplicazioneId.From(req.ApplicazioneId), ApplicazioneName.From(req.Name), ApplicazioneCode.From(req.Code)), ct)).ToUpdateResult(MapApp);
  private static ApplicazioneResponse MapApp(ApplicazioneDto dto) => new(dto.Id.Value, dto.Name.Value, dto.Code.Value, dto.IsActive);
}

public sealed class UpdateApplicazioneRequest { public Guid ApplicazioneId { get; init; } public string Name { get; init; } = string.Empty; public string Code { get; init; } = string.Empty; }
public sealed class UpdateApplicazioneValidator : Validator<UpdateApplicazioneRequest> { public UpdateApplicazioneValidator() { RuleFor(x => x.Name).NotEmpty(); RuleFor(x => x.Code).NotEmpty(); } }

public class Delete(IMediator mediator) : Endpoint<ApplicazioneIdRequest, Results<NoContent, NotFound, ProblemHttpResult>>
{ public override void Configure() { Delete("/Applicazioni/{ApplicazioneId:guid}"); Policies("RequireAuthenticatedUser"); Tags("Applicazioni"); }
  public override async Task<Results<NoContent, NotFound, ProblemHttpResult>> ExecuteAsync(ApplicazioneIdRequest req, CancellationToken ct)
    => (await mediator.Send(new DeleteApplicazioneCommand(ApplicazioneId.From(req.ApplicazioneId)), ct)).ToDeleteResult(); }

public sealed class ApplicazioneIdRequest { public Guid ApplicazioneId { get; init; } }
