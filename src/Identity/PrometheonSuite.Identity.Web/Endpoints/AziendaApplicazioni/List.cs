using FluentValidation;
using PrometheonSuite.Identity.UseCases.AziendaApplicazioni;
using PrometheonSuite.Identity.UseCases.AziendaApplicazioni.List;

namespace PrometheonSuite.Identity.Web.Endpoints.AziendaApplicazioni;

public class List(IMediator mediator) : Endpoint<ListAziendaApplicazioniRequest, AziendaApplicazioneListResponse, AziendaApplicazioneListMapper>
{
  public override void Configure()
  {
    Get("/AziendaApplicazioni");
    Policies("RequireAuthenticatedUser");
    Tags("AziendaApplicazioni");

    Summary(s =>
    {
      s.Summary = "List aziendaapplicazioni with pagination";
      s.Description = "Retrieves a paginated list of aziendaapplicazioni.";
    });
  }

  public override async Task HandleAsync(ListAziendaApplicazioniRequest req, CancellationToken ct)
  {
    var result = await mediator.Send(new ListAziendaApplicazioniQuery(req.Page, req.PerPage), ct);
    if (!result.IsSuccess)
    {
      await Send.ErrorsAsync(400, ct);
      return;
    }

    await Send.OkAsync(Map.FromEntity(result.Value), ct);
  }
}

public sealed class ListAziendaApplicazioniRequest
{
  [BindFrom("page")] public int Page { get; init; } = 1;
  [BindFrom("per_page")] public int PerPage { get; init; } = PrometheonSuite.Identity.UseCases.Constants.DEFAULT_PAGE_SIZE;
}

public sealed class ListAziendaApplicazioniValidator : Validator<ListAziendaApplicazioniRequest>
{
  public ListAziendaApplicazioniValidator()
  {
    RuleFor(x => x.Page).GreaterThanOrEqualTo(1);
    RuleFor(x => x.PerPage).InclusiveBetween(1, PrometheonSuite.Identity.UseCases.Constants.MAX_PAGE_SIZE);
  }
}

public sealed record AziendaApplicazioneListResponse : UseCases.PagedResult<AziendaApplicazioneResponse>
{
  public AziendaApplicazioneListResponse(IReadOnlyList<AziendaApplicazioneResponse> items, int page, int perPage, int totalCount, int totalPages)
    : base(items, page, perPage, totalCount, totalPages)
  {
  }
}

public sealed class AziendaApplicazioneListMapper : Mapper<ListAziendaApplicazioniRequest, AziendaApplicazioneListResponse, UseCases.PagedResult<AziendaApplicazioneDto>>
{
  public override AziendaApplicazioneListResponse FromEntity(UseCases.PagedResult<AziendaApplicazioneDto> e)
    => new(e.Items.Select(MapItem).ToList(), e.Page, e.PerPage, e.TotalCount, e.TotalPages);

  private static AziendaApplicazioneResponse MapItem(AziendaApplicazioneDto dto)
    => new(dto.Id.Value, dto.AziendaId.Value, dto.ApplicazioneId.Value, dto.IsActive);
}
