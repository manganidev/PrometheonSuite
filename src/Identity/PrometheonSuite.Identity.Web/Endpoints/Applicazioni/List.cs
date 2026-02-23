using FluentValidation;
using PrometheonSuite.Identity.UseCases.Applicazioni;
using PrometheonSuite.Identity.UseCases.Applicazioni.List;

namespace PrometheonSuite.Identity.Web.Endpoints.Applicazioni;

public class List(IMediator mediator) : Endpoint<ListApplicazioniRequest, ApplicazioneListResponse, ApplicazioneListMapper>
{
  public override void Configure()
  {
    Get("/Applicazioni");
    Policies("RequireAuthenticatedUser");
    Tags("Applicazioni");

    Summary(s =>
    {
      s.Summary = "List applicazioni with pagination";
      s.Description = "Retrieves a paginated list of applicazioni.";
    });
  }

  public override async Task HandleAsync(ListApplicazioniRequest req, CancellationToken ct)
  {
    var result = await mediator.Send(new ListApplicazioniQuery(req.Page, req.PerPage), ct);
    if (!result.IsSuccess)
    {
      await Send.ErrorsAsync(400, ct);
      return;
    }

    await Send.OkAsync(Map.FromEntity(result.Value), ct);
  }
}

public sealed class ListApplicazioniRequest
{
  [BindFrom("page")] public int Page { get; init; } = 1;
  [BindFrom("per_page")] public int PerPage { get; init; } = PrometheonSuite.Identity.UseCases.Constants.DEFAULT_PAGE_SIZE;
}

public sealed class ListApplicazioniValidator : Validator<ListApplicazioniRequest>
{
  public ListApplicazioniValidator()
  {
    RuleFor(x => x.Page).GreaterThanOrEqualTo(1);
    RuleFor(x => x.PerPage).InclusiveBetween(1, PrometheonSuite.Identity.UseCases.Constants.MAX_PAGE_SIZE);
  }
}

public sealed record ApplicazioneListResponse : UseCases.PagedResult<ApplicazioneResponse>
{
  public ApplicazioneListResponse(IReadOnlyList<ApplicazioneResponse> items, int page, int perPage, int totalCount, int totalPages)
    : base(items, page, perPage, totalCount, totalPages)
  {
  }
}

public sealed class ApplicazioneListMapper : Mapper<ListApplicazioniRequest, ApplicazioneListResponse, UseCases.PagedResult<ApplicazioneDto>>
{
  public override ApplicazioneListResponse FromEntity(UseCases.PagedResult<ApplicazioneDto> e)
    => new(e.Items.Select(MapApplicazione).ToList(), e.Page, e.PerPage, e.TotalCount, e.TotalPages);

  private static ApplicazioneResponse MapApplicazione(ApplicazioneDto dto)
    => new(dto.Id.Value, dto.Name.Value, dto.Code.Value, dto.IsActive);
}
