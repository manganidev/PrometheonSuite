namespace  PrometheonSuite.Identity.UseCases.Figuras.List;

public record ListFigurasQuery(
  int? Page = 1,
  int? PerPage = Constants.DEFAULT_PAGE_SIZE
) : IQuery<Result<PagedResult<FiguraDto>>>;
