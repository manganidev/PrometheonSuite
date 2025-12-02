using PrometheonSuite.Identity.Core.Interfaces;
using PrometheonSuite.Identity.Infrastructure;
using PrometheonSuite.Identity.Infrastructure.SenderEmail;

namespace PrometheonSuite.Identity.Web.Configurations;

public static class ServiceConfigs
{
  public static IServiceCollection AddServiceConfigs(this IServiceCollection services, Microsoft.Extensions.Logging.ILogger logger, WebApplicationBuilder builder)
  {
    services
      .AddCoreInfrastructureServices(builder.Configuration, logger)
      .AddMediatorSourceGen(logger);

    if (builder.Environment.IsDevelopment())
    {
      services.AddScoped<IEmailSender, MimeKitEmailSender>();
    }
    else
    {
      services.AddScoped<IEmailSender, MimeKitEmailSender>();
    }

    logger.LogInformation("{Project} services registered", "Mediator Source Generator and Email Sender");

    return services;
  }


}
