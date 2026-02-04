using System;
using System.Collections.Generic;
using System.Text;

namespace  PrometheonSuite.Identity.UseCases.Utenti.Read.List;

public record ListUtentiQuery(int? Page = 1, int? PerPage = Constants.DEFAULT_PAGE_SIZE)
    : IQuery<Result<PagedResult<UtenteDto>>>;
