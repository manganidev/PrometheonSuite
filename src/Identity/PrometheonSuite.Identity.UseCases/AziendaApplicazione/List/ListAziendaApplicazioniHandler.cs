using PrometheonSuite.Identity.Core.Interfaces;
using PrometheonSuite.Identity.Entities.AziendaApplicazioneAggregate;

namespace PrometheonSuite.Identity.UseCases.AziendaApplicazioni.List;

public class ListAziendaApplicazioniHandler(ICoreRepository<AziendaApplicazione> repository) : IQueryHandler<ListAziendaApplicazioniQuery, Result<PagedResult<AziendaApplicazioneDto>>>
{
  public async ValueTask<Result<PagedResult<AziendaApplicazioneDto>>> Handle(ListAziendaApplicazioniQuery request, CancellationToken cancellationToken)
  {
    int page = request.Page ?? 1;
    int perPage = request.PerPage ?? Constants.DEFAULT_PAGE_SIZE;
    if (page < 1) return Result<PagedResult<AziendaApplicazioneDto>>.Error("Page must be >= 1");
    if (perPage < 1 || perPage > Constants.MAX_PAGE_SIZE) return Result<PagedResult<AziendaApplicazioneDto>>.Error($"PerPage must be between 1 and {Constants.MAX_PAGE_SIZE}");

    var totalCount = await repository.CountAsync(cancellationToken);
    var items = (await repository.ListAsync(cancellationToken)).OrderBy(x => x.AziendaId.Value).ThenBy(x => x.ApplicazioneId.Value).Skip((page - 1) * perPage).Take(perPage)
      .Select(x => new AziendaApplicazioneDto(x.Id, x.AziendaId, x.ApplicazioneId, x.IsActive)).ToList();
    var totalPages = (int)Math.Ceiling(totalCount / (double)perPage);
    return Result<PagedResult<AziendaApplicazioneDto>>.Success(new PagedResult<AziendaApplicazioneDto>(items, page, perPage, totalCount, totalPages));
  }
}
