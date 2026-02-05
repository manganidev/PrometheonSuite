using PrometheonSuite.Identity.Core.Entities.TokenAggregate;
using PrometheonSuite.Identity.Entities.AziendaAggregate;
using PrometheonSuite.Identity.Entities.FiguraAggregate;
using PrometheonSuite.Identity.Entities.RuoloAggregate;
using PrometheonSuite.Identity.Entities.UtenteApplicazioneAziendaAggregate;
using PrometheonSuite.Identity.Entities.UtenteAggregate;
using PrometheonSuite.Identity.Entities.AziendaApplicazioneAggregate;

namespace PrometheonSuite.Identity.Infrastructure.Data;
public class CoreDbContext(DbContextOptions<CoreDbContext> options) : DbContext(options)
{
  public DbSet<Utente> Utenti => Set<Utente>();
  public DbSet<Azienda> Aziendas => Set<Azienda>();
  public DbSet<Ruolo> Ruolos => Set<Ruolo>();
  public DbSet<Figura> Figuras => Set<Figura>();
  public DbSet<AziendaApplicazione> AziendaApplicaziones => Set<AziendaApplicazione>();
  public DbSet<UtenteApplicazioneAzienda> UtenteApplicazioneAziendas => Set<UtenteApplicazioneAzienda>();
  public DbSet<RefreshToken> RefreshTokens => Set<RefreshToken>();

  protected override void OnModelCreating(ModelBuilder modelBuilder)
  {
    base.OnModelCreating(modelBuilder);
    modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
  }

  public override int SaveChanges() =>
        SaveChangesAsync().GetAwaiter().GetResult();
}
