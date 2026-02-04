using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace PrometheonSuite.Identity.Infrastructure.Data;

public class DesignTimeCoreDbContextFactory : IDesignTimeDbContextFactory<CoreDbContext>
{
  public CoreDbContext CreateDbContext(string[] args)
  {
    // Prefer environment variable if provided (e.g., set by CI or local shell)
    var connectionString =
      System.Environment.GetEnvironmentVariable("ConnectionStrings__CoreDB")
      ?? "Server=(localdb)\\mssqllocaldb;Database=CoreDB;Trusted_Connection=True;MultipleActiveResultSets=true";

    var optionsBuilder = new DbContextOptionsBuilder<CoreDbContext>();
    optionsBuilder.UseSqlServer(connectionString);

    return new CoreDbContext(optionsBuilder.Options);
  }
}
