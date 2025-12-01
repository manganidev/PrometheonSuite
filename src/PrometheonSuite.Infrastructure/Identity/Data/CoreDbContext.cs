
namespace PrometheonSuite.Infrastructure.Identity.Data;
public class CoreDbContext(DbContextOptions<CoreDbContext> options) : DbContext(options)
{


  protected override void OnModelCreating(ModelBuilder modelBuilder)
  {
    base.OnModelCreating(modelBuilder);
    modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
  }

  public override int SaveChanges() =>
        SaveChangesAsync().GetAwaiter().GetResult();
}
