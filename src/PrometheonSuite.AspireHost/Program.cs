using System.Net.Sockets;
using Projects;

var builder = DistributedApplication.CreateBuilder(args);

// Add SQL Server container
var sqlServer = builder.AddSqlServer("sqlserver")
  .WithLifetime(ContainerLifetime.Persistent);

// Add the Core database (shared data: tenants, users, roles, etc.)
var coreDb = sqlServer.AddDatabase("CoreDB");

// Add the Business database (business data: contributors, etc.)
var paddockDB = sqlServer.AddDatabase("PaddockHRDB");


// Papercut SMTP container for email testing
var papercut = builder.AddContainer("papercut", "jijiechen/papercut", "latest")
  .WithEndpoint("smtp", e =>
  {
    e.TargetPort = 25;   // container port
    e.Port = 25;         // host port
    e.Protocol = ProtocolType.Tcp;
    e.UriScheme = "smtp";
  })
  .WithEndpoint("ui", e =>
  {
    e.TargetPort = 37408;
    e.Port = 37408;
    e.UriScheme = "http";
  });

// Add the web project with the database connection
builder.AddProject<Projects.PrometheonSuite_Identity_Web>("prometheonsuite-identity-web")
    .WithReference(coreDb)
    .WithEnvironment("Auth__Issuer", "prometheon-identity")   // PUÒ ESSERE ANCHE UN URL, ma non è obbligatorio
    .WithEnvironment("Auth__Audience", "prometheon-suite-api")
    .WithEnvironment("Auth__Key", "f\"ST,qxL|X9XWuQ9sw3UH\"tXnO*mLZ'iZkoiL\\qH$`o'=\\RWf'")
    .WithEnvironment("ASPNETCORE_ENVIRONMENT", builder.Environment.EnvironmentName)
    .WithEnvironment("Papercut__Smtp__Url", papercut.GetEndpoint("smtp"))
    .WaitFor(coreDb)
    .WaitFor(papercut);

builder.AddProject<Projects.PrometheonSuite_PaddockHr_Web>("prometheonsuite-paddockhr-web")
    .WithReference(paddockDB)
    .WithEnvironment("Auth__Issuer", "prometheon-identity")   // lo stesso issuer!
    .WithEnvironment("Auth__Audience", "prometheon-suite-api")
    .WithEnvironment("Auth__Key", "f\"ST,qxL|X9XWuQ9sw3UH\"tXnO*mLZ'iZkoiL\\qH$`o'=\\RWf'")
    .WithEnvironment("ASPNETCORE_ENVIRONMENT", builder.Environment.EnvironmentName)
    .WithEnvironment("Papercut__Smtp__Url", papercut.GetEndpoint("smtp"))
    .WaitFor(paddockDB)
    .WaitFor(papercut);



builder
  .Build()
  .Run();
