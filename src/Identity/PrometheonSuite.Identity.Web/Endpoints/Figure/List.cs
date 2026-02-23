using FluentValidation;
using PrometheonSuite.Identity.UseCases.Figuras;
using PrometheonSuite.Identity.UseCases.Figuras.List;

namespace PrometheonSuite.Identity.Web.Endpoints.Figure;

public class List(IMediator mediator) : Endpoint<ListFigureRequest, FiguraListResponse, FiguraListMapper>
{
  public override void Configure()
  {
    Get("/Figure");
    Policies("RequireAuthenticatedUser");
    Tags("Figure");

    Summary(s =>
    {
      s.Summary = "List figure with pagination";
      s.Description = "Retrieves a paginated list of figure.";
    });
  }

  public override async Task HandleAsync(ListFigureRequest req, CancellationToken ct)
  {
    var result = await mediator.Send(new ListFigurasQuery(req.Page, req.PerPage), ct);
    if (!result.IsSuccess)
    {
      await Send.ErrorsAsync(400, ct);
      return;
    }

    await Send.OkAsync(Map.FromEntity(result.Value), ct);
  }
}

public sealed class ListFigureRequest
{
  [BindFrom("page")]
  public int Page { get; init; } = 1;

  [BindFrom("per_page")]
  public int PerPage { get; init; } = PrometheonSuite.Identity.UseCases.Constants.DEFAULT_PAGE_SIZE;
}

public sealed class ListFigureValidator : Validator<ListFigureRequest>
{
  public ListFigureValidator()
  {
    RuleFor(x => x.Page).GreaterThanOrEqualTo(1);
    RuleFor(x => x.PerPage).InclusiveBetween(1, PrometheonSuite.Identity.UseCases.Constants.MAX_PAGE_SIZE);
  }
}

public sealed record FiguraListResponse : UseCases.PagedResult<FiguraResponse>
{
  public FiguraListResponse(IReadOnlyList<FiguraResponse> items, int page, int perPage, int totalCount, int totalPages)
    : base(items, page, perPage, totalCount, totalPages)
  {
  }
}

public sealed class FiguraListMapper : Mapper<ListFigureRequest, FiguraListResponse, UseCases.PagedResult<FiguraDto>>
{
  public override FiguraListResponse FromEntity(UseCases.PagedResult<FiguraDto> e)
  {
    var items = e.Items.Select(x => new FiguraResponse(x.Id.Value, x.Code.Value, x.Name.Value, x.ApplicazioneId.Value, x.Description, x.RoleIds)).ToList();
    return new FiguraListResponse(items, e.Page, e.PerPage, e.TotalCount, e.TotalPages);
  }
}
