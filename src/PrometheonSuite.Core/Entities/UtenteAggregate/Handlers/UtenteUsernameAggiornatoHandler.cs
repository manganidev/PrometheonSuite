using System;
using System.Collections.Generic;
using System.Text;
using PrometheonSuite.Identity.Entities.UtenteAggregate.Events;


namespace PrometheonSuite.Identity.Entities.UtenteAggregate.Handlers;

public sealed class UtenteUsernameAggiornatoHandler(
  ILogger<UtenteUsernameAggiornatoHandler> logger
) : INotificationHandler<UtenteUsernameAggiornatoEvent>
{
  public ValueTask Handle(UtenteUsernameAggiornatoEvent notification, CancellationToken cancellationToken)
  {
    logger.LogInformation(
      "Username utente aggiornato. UtenteId: {UtenteId}, NuovoUsername: {Username}",
      notification.Utente.Id,
      notification.Utente.Username
    );

    return ValueTask.CompletedTask;
  }
}
