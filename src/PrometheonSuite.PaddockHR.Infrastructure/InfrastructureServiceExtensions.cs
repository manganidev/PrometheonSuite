using PrometheonSuite.PaddockHR.Core.Interfaces;
using PrometheonSuite.PaddockHR.Infrastructure.Data;

namespace PrometheonSuite.PaddockHR.Infrastructure;
public static class InfrastructureServiceExtensions
{
  public static IServiceCollection AddPaddockHRInfrastructureServices(
    this IServiceCollection services,
    ConfigurationManager config,
    ILogger logger)
  {
    // Try to get connection strings in order of priority:
    // 1. "cleanarchitecture" - provided by Aspire when using .WithReference(cleanArchDb)
    // 2. "DefaultConnection" - traditional SQL Server connection
    // 3. "SqliteConnection" - fallback to SQLite
    string? connectionString = config.GetConnectionString("PaddockHRDB");
                    
    Guard.Against.Null(connectionString);

    services.AddScoped<PaddockDbEventDispatchInterceptor>();
    services.AddScoped<IDomainEventDispatcher, MediatorDomainEventDispatcher>();

    services.AddDbContext<PaddockHRDbContext>((provider, options) =>
    {
      var eventDispatchInterceptor = provider.GetRequiredService<PaddockDbEventDispatchInterceptor>();

      // Use SQL Server if Aspire or DefaultConnection is available, otherwise use SQLite
      if (config.GetConnectionString("PaddockHRDB") != null)
      {
        options.UseSqlServer(connectionString);
      }
      
      options.AddInterceptors(eventDispatchInterceptor);
    });

    services.AddScoped(typeof(IPaddockRepository<>), typeof(PaddockEfRepository<>))
           .AddScoped(typeof(IPaddockReadRepository<>), typeof(PaddockEfRepository<>));
           //.AddScoped<IListContributorsQueryService, ListContributorsQueryService>()
           //.AddScoped<IDeleteContributorService, DeleteContributorService>();

    logger.LogInformation("{Project} services registered", "Infrastructure");

    return services;
  }
}
