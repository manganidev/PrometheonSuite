namespace  PrometheonSuite.Identity.UseCases.Aziendas.List;

public record ListAziendasQuery(
  int? Page = 1,
  int? PerPage = Constants.DEFAULT_PAGE_SIZE
) : IQuery<Result<PagedResult<AziendaDto>>>;
