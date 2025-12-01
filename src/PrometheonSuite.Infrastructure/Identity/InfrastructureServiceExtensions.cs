using PrometheonSuite.Identity.Core.Interfaces;
using PrometheonSuite.Infrastructure.Identity.Data;

namespace PrometheonSuite.Infrastructure.Identity;
public static class InfrastructureServiceExtensions
{
  public static IServiceCollection AddCoreInfrastructureServices(
    this IServiceCollection services,
    ConfigurationManager config,
    ILogger logger)
  {
    // Try to get connection strings in order of priority:
    // 1. "cleanarchitecture" - provided by Aspire when using .WithReference(cleanArchDb)
    // 2. "DefaultConnection" - traditional SQL Server connection
    // 3. "SqliteConnection" - fallback to SQLite
    string? connectionString = config.GetConnectionString("CoreDB");
    Guard.Against.Null(connectionString);

    services.AddScoped<Core_EventDispatchInterceptor>();
    services.AddScoped<IDomainEventDispatcher, MediatorDomainEventDispatcher>();

    services.AddDbContext<CoreDbContext>((provider, options) =>
    {
      var eventDispatchInterceptor = provider.GetRequiredService<Core_EventDispatchInterceptor>();
      
      // Use SQL Server if Aspire or DefaultConnection is available, otherwise use SQLite
      if (config.GetConnectionString("CoreDB") != null)
      {
        options.UseSqlServer(connectionString);
      }
   
      
      options.AddInterceptors(eventDispatchInterceptor);
    });

    services.AddScoped(typeof(ICoreRepository<>), typeof(CoreEfRepository<>))
           .AddScoped(typeof(ICoreReadRepository<>), typeof(CoreEfRepository<>));
           //.AddScoped<IListContributorsQueryService, ListContributorsQueryService>()
           //.AddScoped<IDeleteContributorService, DeleteContributorService>();

    logger.LogInformation("{Project} services registered", "Infrastructure");

    return services;
  }
}
