using System;
using System.Collections.Generic;
using System.Text;
using PrometheonSuite.Identity.Entities.UtenteAggregate;

namespace PrometheonSuite.Identity.Core.Entities.UtenteAggregate.Specifications;

public sealed class UtenteByEmailExcludingIdSpec : Specification<Utente>
{
  public UtenteByEmailExcludingIdSpec(Email email, UtenteId excludeId) =>
    Query.Where(u => u.Email == email && u.Id != excludeId);
}
