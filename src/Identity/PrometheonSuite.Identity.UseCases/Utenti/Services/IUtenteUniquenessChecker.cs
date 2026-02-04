using System;
using System.Collections.Generic;
using System.Text;
using PrometheonSuite.Identity.Entities.UtenteAggregate;

namespace PrometheonSuite.Identity.UseCases.Utenti.Services;

public interface IUtenteUniquenessChecker
{
  Task<bool> EmailExistsAsync(Email email, CancellationToken ct);
  Task<bool> UsernameExistsAsync(Username username, CancellationToken ct);
  Task<bool> EmailExistsForAnotherAsync(Email email, UtenteId excludeId, CancellationToken ct);
  Task<bool> UsernameExistsForAnotherAsync(Username username, UtenteId excludeId, CancellationToken ct);
}
