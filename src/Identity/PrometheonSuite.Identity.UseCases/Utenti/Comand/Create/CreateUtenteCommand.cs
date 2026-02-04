using System;
using System.Collections.Generic;
using System.Text;
using PrometheonSuite.Identity.Entities.UtenteAggregate;

namespace  PrometheonSuite.Identity.UseCases.Utenti.Comand.Create;

public record CreateUtenteCommand(
  Username Username,
  Email Email,
  string Password // Password in chiaro - verrà hashata nell'handler
) : ICommand<Result<UtenteDto>>;
