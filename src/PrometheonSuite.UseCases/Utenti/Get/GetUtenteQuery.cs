using System;
using System.Collections.Generic;
using System.Text;
using PrometheonSuite.Identity.Entities.UtenteAggregate;

namespace  PrometheonSuite.Identity.UseCases.Utenti.Get;

public record GetUtenteQuery(UtenteId UtenteId)
    : IQuery<Result<UtenteDto>>;
