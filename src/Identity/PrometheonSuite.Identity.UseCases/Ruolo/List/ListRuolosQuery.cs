namespace  PrometheonSuite.Identity.UseCases.Ruolos.List;

public record ListRuolosQuery(
  int? Page = 1,
  int? PerPage = Constants.DEFAULT_PAGE_SIZE
) : IQuery<Result<PagedResult<RuoloDto>>>;
