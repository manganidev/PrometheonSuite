using Ardalis.ListStartupServices;
using PrometheonSuite.Infrastructure.Identity.Data;
using PrometheonSuite.Infrastructure.PaddockHR.Data;
using Scalar.AspNetCore;

namespace PrometheonSuite.Web.Configurations;

public static class MiddlewareConfig
{
  public static async Task<IApplicationBuilder> UseAppMiddlewareAndSeedDatabase(this WebApplication app)
  {
    if (app.Environment.IsDevelopment())
    {
      app.UseDeveloperExceptionPage();
      app.UseShowAllServicesMiddleware(); // see https://github.com/ardalis/AspNetCoreStartupServices
    }
    else
    {   
      app.UseDefaultExceptionHandler(); // from FastEndpoints
      app.UseHsts();
    }

    app.UseFastEndpoints();

    if (app.Environment.IsDevelopment())
    {
      app.UseSwaggerGen(options =>
      {
        options.Path = "/openapi/{documentName}.json";
      });
      app.MapScalarApiReference();
    }

    app.UseHttpsRedirection(); // Note this will drop Authorization headers

    // Run migrations and seed in Development or when explicitly requested via environment variable
    var shouldMigrate = app.Environment.IsDevelopment() || 
                        app.Configuration.GetValue<bool>("Database:ApplyMigrationsOnStartup");
    
    if (shouldMigrate)
    {
      await MigrateDatabaseAsync(app);
      await SeedDatabaseAsync(app);
    }

    return app;
  }

  static async Task MigrateDatabaseAsync(WebApplication app)
  {
    using var scope = app.Services.CreateScope();
    var services = scope.ServiceProvider;
    var logger = services.GetRequiredService<ILogger<Program>>();

    try
    {
      logger.LogInformation("Applying CoreDbContext migrations...");
      var core = services.GetRequiredService<CoreDbContext>();
      await core.Database.MigrateAsync();
      logger.LogInformation("CoreDbContext migrations applied");

      logger.LogInformation("Applying PaddockDbContext migrations...");
      var paddockDB = services.GetRequiredService<PaddockHRDbContext>();
      await paddockDB.Database.MigrateAsync();
      logger.LogInformation("PaddockDbContext migrations applied");
    }
    catch (Exception ex)
    {
      logger.LogError(ex, "An error occurred migrating the DB. {exceptionMessage}", ex.Message);
      throw; // Re-throw to make startup fail if migrations fail
    }
  }

  static async Task SeedDatabaseAsync(WebApplication app)
  {
    using var scope = app.Services.CreateScope();
    var services = scope.ServiceProvider;
    var logger = services.GetRequiredService<ILogger<Program>>();

    try
    {
      logger.LogInformation("Seeding CoreDB database...");
      var context = services.GetRequiredService<CoreDbContext>();
      await CoreSeedData.InitializeAsync(context);
      logger.LogInformation("Database CoreDB seeded successfully");
      logger.LogInformation("Seeding PaddockDB database...");
      var paddockDB = services.GetRequiredService<PaddockHRDbContext>();
      await PaddockHRSeedData.InitializeAsync(paddockDB);
      logger.LogInformation("Database PaddockDB seeded successfully");

    }
    catch (Exception ex)
    {
      logger.LogError(ex, "An error occurred seeding the DB. {exceptionMessage}", ex.Message);
      // Don't re-throw for seeding errors - it's not critical
    }
  }
}
