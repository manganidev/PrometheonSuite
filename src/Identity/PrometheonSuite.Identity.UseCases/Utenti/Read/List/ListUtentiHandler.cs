using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PrometheonSuite.Identity.Entities.UtenteAggregate;
using PrometheonSuite.Identity.Core.Interfaces;

namespace  PrometheonSuite.Identity.UseCases.Utenti.Read.List;

public class ListUtentiHandler(ICoreReadRepository<Utente> repository)
    : IQueryHandler<ListUtentiQuery, Result<PagedResult<UtenteDto>>>
{
  public async ValueTask<Result<PagedResult<UtenteDto>>> Handle(ListUtentiQuery request, CancellationToken cancellationToken)
  {
    int page = request.Page ?? 1;
    int perPage = request.PerPage ?? Constants.DEFAULT_PAGE_SIZE;

    if (page < 1)
      return Result.Invalid(new ValidationError("page", "Page must be >= 1"));

    if (perPage < 1 || perPage > Constants.MAX_PAGE_SIZE)
      return Result.Invalid(new ValidationError("per_page", $"PerPage must be between 1 and {Constants.MAX_PAGE_SIZE}"));

    var allItems = await repository.ListAsync(cancellationToken);
    var totalCount = allItems.Count;
    var totalPages = (int)Math.Ceiling(totalCount / (double)perPage);

    var items = allItems
      .Skip((page - 1) * perPage)
      .Take(perPage)
      .Select(u => new UtenteDto(u.Id, u.Username, u.Email, u.Attivo))
      .ToList();

    var pagedResult = new PagedResult<UtenteDto>(items, page, perPage, totalCount, totalPages);

    return Result.Success(pagedResult);
  }
}
