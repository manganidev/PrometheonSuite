using MassTransit;
using PrometheonSuite.Identity.Entities.UtenteAggregate;
using PrometheonSuite.Identity.UseCases.Messaggistica.Contratti;

public sealed class PubblicaEventiUtenteService
{
  private readonly IPublishEndpoint _publisher;

  public PubblicaEventiUtenteService(IPublishEndpoint publisher)
  {
    _publisher = publisher;
  }

  public Task PubblicaUsernameAggiornatoAsync(UtenteId idUtente, Username username, CancellationToken ct)
  {
    return _publisher.Publish(new UtenteUsernameAggiornatoMessaggio(idUtente, username), ct);
  }
}
