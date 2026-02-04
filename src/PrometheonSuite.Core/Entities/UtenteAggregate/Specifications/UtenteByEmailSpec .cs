using System;
using System.Collections.Generic;
using System.Text;
using PrometheonSuite.Identity.Entities.UtenteAggregate;

namespace PrometheonSuite.Identity.Core.Entities.UtenteAggregate.Specifications;

public sealed class UtenteByEmailSpec : Specification<Utente>
{
  public UtenteByEmailSpec(Email email) =>
    Query.Where(u => u.Email == email);
}
