namespace PrometheonSuite.Identity.Infrastructure.Data;

public static class CoreDbContextExtensions
{
  public static void AddApplicationDbContext(this IServiceCollection services, string connectionString) =>
    services.AddDbContext<CoreDbContext>(options =>
         options.UseSqlServer(connectionString));

}
