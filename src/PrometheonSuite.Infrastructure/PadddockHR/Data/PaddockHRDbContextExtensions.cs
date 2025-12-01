namespace PrometheonSuite.Infrastructure.PaddockHR.Data;

public static class PaddockHRContextExtensions
{
  public static void AddApplicationDbContext(this IServiceCollection services, string connectionString) =>
    services.AddDbContext<PaddockHRDbContext>(options =>
         options.UseSqlServer(connectionString));

}
