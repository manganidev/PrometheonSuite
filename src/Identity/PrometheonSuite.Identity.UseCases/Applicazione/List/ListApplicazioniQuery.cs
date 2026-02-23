namespace PrometheonSuite.Identity.UseCases.Applicazioni.List;

public record ListApplicazioniQuery(int? Page = 1, int? PerPage = Constants.DEFAULT_PAGE_SIZE) : IQuery<Result<PagedResult<ApplicazioneDto>>>;
