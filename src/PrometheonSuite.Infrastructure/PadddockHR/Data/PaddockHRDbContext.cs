
namespace PrometheonSuite.Infrastructure.PaddockHR.Data;
public class PaddockHRDbContext(DbContextOptions<PaddockHRDbContext> options) : DbContext(options)
{


  protected override void OnModelCreating(ModelBuilder modelBuilder)
  {
    base.OnModelCreating(modelBuilder);
    modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
  }

  public override int SaveChanges() =>
        SaveChangesAsync().GetAwaiter().GetResult();
}
