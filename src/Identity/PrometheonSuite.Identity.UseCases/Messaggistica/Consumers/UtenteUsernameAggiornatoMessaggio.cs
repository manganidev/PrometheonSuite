
using global::PrometheonSuite.Identity.UseCases.Messaggistica.Contratti;
using MassTransit;
using Microsoft.Extensions.Logging;
namespace PrometheonSuite.Identity.UseCases.Messaggistica.Consumers;

public sealed class UtenteUsernameAggiornatoConsumer : IConsumer<UtenteUsernameAggiornatoMessaggio>
{
  private readonly ILogger<UtenteUsernameAggiornatoConsumer> _logger;

  public UtenteUsernameAggiornatoConsumer(ILogger<UtenteUsernameAggiornatoConsumer> logger)
  {
    _logger = logger;
  }

  public Task Consume(ConsumeContext<UtenteUsernameAggiornatoMessaggio> contesto)
  {
    _logger.LogInformation(
      "Ricevuto messaggio: username aggiornato. IdUtente: {IdUtente}, Username: {Username}",
      contesto.Message.idUtente,
      contesto.Message.username
    );

    // qui fai orchestration applicativa (UseCases): es. aggiornare read-model, inviare notifica, ecc.
    return Task.CompletedTask;
  }
}
