
using FluentValidation;
using PrometheonSuite.Identity.UseCases.Utenti;
using PrometheonSuite.Identity.UseCases.Utenti.Read.List;

namespace PrometheonSuite.Identity.Web.Endpoints.Utenti;

public class List(IMediator mediator) : Endpoint<ListUtentiRequest, UtenteListResponse, ListUtentiMapper>
{
  private readonly IMediator _mediator = mediator;

  public override void Configure()
  {
    Get("/Utenti");
    AllowAnonymous();

    Summary(s =>
    {
      s.Summary = "List users with pagination";
      s.Description = "Retrieves a paginated list of all users. Supports GitHub-style pagination with 1-based page indexing and configurable page size.";
      s.ExampleRequest = new ListUtentiRequest { Page = 1, PerPage = 10 };
      s.ResponseExamples[200] = new UtenteListResponse(
        new List<UtenteRecord>
        {
          new(Guid.NewGuid(), "johndoe", "john.doe@example.com", true),
          new(Guid.NewGuid(), "janedoe", "jane.doe@example.com", true)
        },
        1, 10, 2, 1);

      s.Params["page"] = "1-based page index (default 1)";
      s.Params["per_page"] = $"Page size 1–{PrometheonSuite.Identity.UseCases.Constants.MAX_PAGE_SIZE} (default {PrometheonSuite.Identity.UseCases.Constants.DEFAULT_PAGE_SIZE})";

      s.Responses[200] = "Paginated list of users returned successfully";
      s.Responses[400] = "Invalid pagination parameters";
    });

    Tags("Utenti");

    Description(builder => builder
      .Accepts<ListUtentiRequest>()
      .Produces<UtenteListResponse>(200, "application/json")
      .ProducesProblem(400));
  }

  public override async Task HandleAsync(ListUtentiRequest request, CancellationToken cancellationToken)
  {
    var result = await _mediator.Send(new ListUtentiQuery(request.Page, request.PerPage), cancellationToken);
    if (!result.IsSuccess)
    {
      await Send.ErrorsAsync(statusCode: 400, cancellationToken);
      return;
    }

    var pagedResult = result.Value;
    AddLinkHeader(pagedResult.Page, pagedResult.PerPage, pagedResult.TotalPages);

    var response = Map.FromEntity(pagedResult);
    await Send.OkAsync(response, cancellationToken);
  }

  private void AddLinkHeader(int page, int perPage, int totalPages)
  {
    var baseUrl = $"{HttpContext.Request.Scheme}://{HttpContext.Request.Host}{HttpContext.Request.Path}";
    string Link(string rel, int p) => $"<{baseUrl}?page={p}&per_page={perPage}>; rel=\"{rel}\"";

    var parts = new List<string>();
    if (page > 1)
    {
      parts.Add(Link("first", 1));
      parts.Add(Link("prev", page - 1));
    }
    if (page < totalPages)
    {
      parts.Add(Link("next", page + 1));
      parts.Add(Link("last", totalPages));
    }

    if (parts.Count > 0)
      HttpContext.Response.Headers["Link"] = string.Join(", ", parts);
  }
}

public sealed class ListUtentiRequest
{
  [BindFrom("page")]
  public int Page { get; init; } = 1;

  [BindFrom("per_page")]
  public int PerPage { get; init; } = PrometheonSuite.Identity.UseCases.Constants.DEFAULT_PAGE_SIZE;
}

public record UtenteListResponse : UseCases.PagedResult<UtenteRecord>
{
  public UtenteListResponse(IReadOnlyList<UtenteRecord> Items, int Page, int PerPage, int TotalCount, int TotalPages)
    : base(Items, Page, PerPage, TotalCount, TotalPages)
  {
  }
}

public sealed class ListUtentiValidator : Validator<ListUtentiRequest>
{
  public ListUtentiValidator()
  {
    RuleFor(x => x.Page)
      .GreaterThanOrEqualTo(1)
      .WithMessage("page must be >= 1");

    RuleFor(x => x.PerPage)
      .InclusiveBetween(1, PrometheonSuite.Identity.UseCases.Constants.MAX_PAGE_SIZE)
      .WithMessage($"per_page must be between 1 and {PrometheonSuite.Identity.UseCases.Constants.MAX_PAGE_SIZE}");
  }
}

public sealed class ListUtentiMapper
  : Mapper<ListUtentiRequest, UtenteListResponse, UseCases.PagedResult<UtenteDto>>
{
  public override UtenteListResponse FromEntity(UseCases.PagedResult<UtenteDto> e)
  {
    var items = e.Items
      .Select(u => new UtenteRecord(u.Id.Value, u.Username.Value, u.Email.Value, u.Attivo))
      .ToList();

    return new UtenteListResponse(items, e.Page, e.PerPage, e.TotalCount, e.TotalPages);
  }
}
