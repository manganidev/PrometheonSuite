using System;
using System.Collections.Generic;
using System.Text;
using Mediator;
using PrometheonSuite.Identity.Entities.UtenteAggregate.Events;
namespace PrometheonSuite.Identity.UseCases.Utenti.Messaggistica;

public sealed class UtenteUsernameAggiornatoHandlerIntegrazione(
  PubblicaEventiUtenteService pubblicatore
) : INotificationHandler<UtenteUsernameAggiornatoEvent>
{
  public ValueTask Handle(UtenteUsernameAggiornatoEvent notification, CancellationToken cancellationToken)
  {
    return new ValueTask(
      pubblicatore.PubblicaUsernameAggiornatoAsync(
        notification.Utente.Id,
        notification.Utente.Username,
        cancellationToken
      )
    );
  }
}
