using FluentValidation;
using PrometheonSuite.Identity.UseCases.Ruolos;
using PrometheonSuite.Identity.UseCases.Ruolos.List;

namespace PrometheonSuite.Identity.Web.Endpoints.Ruoli;

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
  public RuoloListResponse(IReadOnlyList<RuoloResponse> items, int page, int perPage, int totalCount, int totalPages)
    : base(items, page, perPage, totalCount, totalPages)
  {
  }
}

public sealed class RuoloListMapper : Mapper<ListRuoliRequest, RuoloListResponse, UseCases.PagedResult<RuoloDto>>
{
  public override RuoloListResponse FromEntity(UseCases.PagedResult<RuoloDto> e)
    => new(e.Items.Select(MapRuolo).ToList(), e.Page, e.PerPage, e.TotalCount, e.TotalPages);

  private static RuoloResponse MapRuolo(RuoloDto dto)
    => new(dto.Id.Value, dto.Code.Value, dto.Name.Value, dto.ApplicazioneId.Value, dto.Description);
}
