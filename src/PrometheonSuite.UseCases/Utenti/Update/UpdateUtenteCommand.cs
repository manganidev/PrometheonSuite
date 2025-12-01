using System;
using System.Collections.Generic;
using System.Text;
using PrometheonSuite.Identity.Entities.UtenteAggregate;

namespace  PrometheonSuite.Identity.UseCases.Utenti.Update;

public record UpdateUtenteCommand(
    UtenteId UtenteId,
    Username Username,
    Email Email
) : ICommand<Result<UtenteDto>>;
