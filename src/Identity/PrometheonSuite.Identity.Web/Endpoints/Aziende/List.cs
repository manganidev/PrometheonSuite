using FluentValidation;
using PrometheonSuite.Identity.UseCases.Aziendas;
using PrometheonSuite.Identity.UseCases.Aziendas.List;

namespace PrometheonSuite.Identity.Web.Endpoints.Aziende;

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
  public AziendaListResponse(IReadOnlyList<AziendaResponse> items, int page, int perPage, int totalCount, int totalPages)
    : base(items, page, perPage, totalCount, totalPages)
  {
  }
}

public sealed class AziendaListMapper : Mapper<ListAziendeRequest, AziendaListResponse, UseCases.PagedResult<AziendaDto>>
{
  public override AziendaListResponse FromEntity(UseCases.PagedResult<AziendaDto> e)
    => new(e.Items.Select(MapAzienda).ToList(), e.Page, e.PerPage, e.TotalCount, e.TotalPages);

  private static AziendaResponse MapAzienda(AziendaDto dto)
    => new(dto.Id.Value, dto.Name.Value, dto.Code.Value, dto.IsActive);
}
