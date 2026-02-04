using System;
using System.Collections.Generic;
using System.Text;
using PrometheonSuite.Identity.BaseEntities;
using PrometheonSuite.Identity.Entities.UtenteAggregate.Events;


namespace PrometheonSuite.Identity.Entities.UtenteAggregate;

public class Utente : AuditableEntity<Utente, UtenteId>, IAggregateRoot
{
  public Username Username { get; private set; }
  public Email Email { get; private set; }
  public HashedPassword Password { get; private set; }
  public bool Attivo { get; private set; } = true;

  public Utente(Username username, Email email, HashedPassword password)
  {
    // Initialize ID with a new Guid immediately to avoid Vogen uninitialized errors
    Id = UtenteId.From(Guid.NewGuid());
    Username = username;  
    Email = email;
    Password = password;
  }

  // EF Core requires a parameterless constructor
  #pragma warning disable CS8618
  private Utente()
  {
    // Initialize with temp Guid - EF will set the real value from database
    Id = UtenteId.From(Guid.Empty);
  }
  #pragma warning restore CS8618

  public Utente Disattiva()
  {
    Attivo = false;
    return this;
  }

  public Utente Attiva()
  {
    Attivo = true;
    return this;
  }

  public Utente AggiornaAnagrafica(Username nuovoUsername, Email nuovaEmail)
  {
    bool nomeCambiato = Username != nuovoUsername;

    Username = nuovoUsername;
    Email = nuovaEmail;

    if (nomeCambiato)
    {
      RegisterDomainEvent(new UtenteUsernameAggiornatoEvent(this));
    }

    return this;
  }

  public Utente CambiaPassword(HashedPassword nuovaPassword)
  {
    Password = nuovaPassword;
    return this;
  }

  public bool VerificaPassword(string plainTextPassword)
  {
    return Password.Verify(plainTextPassword);
  }
}


