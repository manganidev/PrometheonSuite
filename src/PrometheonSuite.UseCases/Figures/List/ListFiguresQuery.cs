namespace  PrometheonSuite.Identity.UseCases.Figures.List;

public record ListFiguresQuery(
  int? Page = 1,
  int? PerPage = Constants.DEFAULT_PAGE_SIZE
) : IQuery<Result<PagedResult<FigureDto>>>;
