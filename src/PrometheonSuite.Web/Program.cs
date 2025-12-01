using PrometheonSuite.Identity.Core.Interfaces;
using PrometheonSuite.Infrastructure.Identity.Data;
using PrometheonSuite.Infrastructure.PaddockHR.Data;
using PrometheonSuite.PaddockHR.Core.Interfaces;
using PrometheonSuite.Web.Configurations;

var builder = WebApplication.CreateBuilder(args);

builder.AddServiceDefaults()    // This sets up OpenTelemetry logging
       .AddLoggerConfigs();     // This adds Serilog for console formatting

using var loggerFactory = LoggerFactory.Create(config => config.AddConsole());
var startupLogger = loggerFactory.CreateLogger<Program>();

startupLogger.LogInformation("Starting web host");

builder.Services.AddOptionConfigs(builder.Configuration, startupLogger, builder);
builder.Services.AddServiceConfigs(startupLogger, builder);
// Core repository generico (sostituisci l’implementazione con la tua concreta)
builder.Services.AddScoped(typeof(ICoreRepository<>), typeof(CoreEfRepository<>));
// Paddock repository generico (se richiesto)
builder.Services.AddScoped(typeof(IPaddockRepository<>), typeof(PaddockEfRepository<>));
builder.Services.AddFastEndpoints()
                .SwaggerDocument(o =>
                {
                  o.ShortSchemaNames = true;
                });

var app = builder.Build();

await app.UseAppMiddlewareAndSeedDatabase();

app.MapDefaultEndpoints(); // Aspire health checks and metrics

app.Run();

// Make the implicit Program.cs class public, so integration tests can reference the correct assembly for host building
public partial class Program { }
