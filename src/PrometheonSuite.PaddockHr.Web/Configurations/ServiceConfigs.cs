
using PrometheonSuite.PaddockHR.Core.Interfaces;
using PrometheonSuite.PaddockHR.Infrastructure.Email;
using PrometheonSuite.PaddockHR.Infrastructure;

namespace PrometheonSuite.PaddockHR.Web.Configurations;

public static class ServiceConfigs
{
  public static IServiceCollection AddServiceConfigs(this IServiceCollection services, Microsoft.Extensions.Logging.ILogger logger, WebApplicationBuilder builder)
  {
    services
      .AddPaddockHRInfrastructureServices(builder.Configuration, logger)
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
