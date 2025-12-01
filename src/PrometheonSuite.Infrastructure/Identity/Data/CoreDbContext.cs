
using PrometheonSuite.Identity.Entities.FigureAggregate;
using PrometheonSuite.Identity.Entities.RoleAggregate;
using PrometheonSuite.Identity.Entities.TenantAggregate;
using PrometheonSuite.Identity.Entities.UserTenantAggregate;
using PrometheonSuite.Identity.Entities.UtenteAggregate;

namespace PrometheonSuite.Infrastructure.Identity.Data;
public class CoreDbContext(DbContextOptions<CoreDbContext> options) : DbContext(options)
{
  public DbSet<Utente> Utenti => Set<Utente>();
  public DbSet<Tenant> Tenants => Set<Tenant>();
  public DbSet<Role> Roles => Set<Role>();
  public DbSet<Figure> Figures => Set<Figure>();
  public DbSet<UserTenant> UserTenants => Set<UserTenant>();

  protected override void OnModelCreating(ModelBuilder modelBuilder)
  {
    base.OnModelCreating(modelBuilder);
    modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
  }

  public override int SaveChanges() =>
        SaveChangesAsync().GetAwaiter().GetResult();
}
