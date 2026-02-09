
using MassTransit;
using PrometheonSuite.Identity.UseCases.Messaggistica.Consumers;

namespace PrometheonSuite.Identity.Infrastructure.Messaggistica;


public static class DependencyInjectionMessaggistica
{
  public static IServiceCollection AggiungiMessaggistica(
     this IServiceCollection servizi,
     IConfiguration configurazione)
  {
    servizi.AddMassTransit(configurazioneBus =>
    {
      configurazioneBus.AddConsumers(typeof(UtenteUsernameAggiornatoConsumer).Assembly);

      configurazioneBus.UsingRabbitMq((contesto, cfg) =>
      {
        // ✅ Aspire: la connection string viene iniettata qui (nome risorsa: "rabbitmq")
        var stringaConnessione = configurazione.GetConnectionString("rabbitmq")
                               ?? configurazione.GetConnectionString("RabbitMQ");

        if (string.IsNullOrWhiteSpace(stringaConnessione))
          throw new InvalidOperationException("Connection string RabbitMQ mancante. Verifica .WithReference(rabbitMq) nell'AppHost.");

        cfg.Host(new Uri(stringaConnessione));

        cfg.UseMessageRetry(r => r.Interval(3, TimeSpan.FromSeconds(5)));
        cfg.ConfigureEndpoints(contesto);
      });
    });

    return servizi;
  }
}
