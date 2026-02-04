using System;
using System.Collections.Generic;
using System.Text;
using PrometheonSuite.Identity.Entities.UtenteAggregate;

namespace PrometheonSuite.Identity.Core.Entities.UtenteAggregate.Specifications;

public sealed class UtenteByUsernameExcludingIdSpec : Specification<Utente>
{
  public UtenteByUsernameExcludingIdSpec(Username username, UtenteId excludeId)
  {
    Query.Where(u =>
      u.Username == username &&
      u.Id != excludeId);
  }
}
