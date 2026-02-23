using PrometheonSuite.Identity.Core.Interfaces;
using PrometheonSuite.Identity.Entities.ApplicazioneAggregate;

namespace PrometheonSuite.Identity.UseCases.Applicazioni.List;

public class ListApplicazioniHandler(ICoreRepository<Applicazione> repository) : IQueryHandler<ListApplicazioniQuery, Result<PagedResult<ApplicazioneDto>>>
{
  public async ValueTask<Result<PagedResult<ApplicazioneDto>>> Handle(ListApplicazioniQuery request, CancellationToken cancellationToken)
  {
    int page = request.Page ?? 1;
    int perPage = request.PerPage ?? Constants.DEFAULT_PAGE_SIZE;
    if (page < 1) return Result<PagedResult<ApplicazioneDto>>.Error("Page must be >= 1");
    if (perPage < 1 || perPage > Constants.MAX_PAGE_SIZE) return Result<PagedResult<ApplicazioneDto>>.Error($"PerPage must be between 1 and {Constants.MAX_PAGE_SIZE}");

    var totalCount = await repository.CountAsync(cancellationToken);
    var apps = (await repository.ListAsync(cancellationToken)).OrderBy(x => x.Name.Value).Skip((page - 1) * perPage).Take(perPage)
      .Select(x => new ApplicazioneDto(x.Id, x.Name, x.Code, x.IsActive)).ToList();
    var totalPages = (int)Math.Ceiling(totalCount / (double)perPage);
    return Result<PagedResult<ApplicazioneDto>>.Success(new PagedResult<ApplicazioneDto>(apps, page, perPage, totalCount, totalPages));
  }
}
