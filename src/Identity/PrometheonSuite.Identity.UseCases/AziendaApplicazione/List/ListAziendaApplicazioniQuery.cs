namespace PrometheonSuite.Identity.UseCases.AziendaApplicazioni.List;

public record ListAziendaApplicazioniQuery(int? Page = 1, int? PerPage = Constants.DEFAULT_PAGE_SIZE) : IQuery<Result<PagedResult<AziendaApplicazioneDto>>>;
