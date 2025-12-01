using System;
using System.Collections.Generic;
using System.Text;

namespace PrometheonSuite.Identity.Entities.UtenteAggregate.Events;

public class UtenteUsernameAggiornatoEvent : DomainEventBase
{
  public Utente Utente { get; }

  public UtenteUsernameAggiornatoEvent(Utente utente)
  {
    Utente = utente;
  }
}
