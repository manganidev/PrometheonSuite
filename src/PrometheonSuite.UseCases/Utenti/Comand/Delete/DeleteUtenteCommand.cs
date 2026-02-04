using System;
using System.Collections.Generic;
using System.Text;
using PrometheonSuite.Identity.Entities.UtenteAggregate;

namespace  PrometheonSuite.Identity.UseCases.Utenti.Comand.Delete;


public record DeleteUtenteCommand(UtenteId UtenteId)
    : ICommand<Result>;
