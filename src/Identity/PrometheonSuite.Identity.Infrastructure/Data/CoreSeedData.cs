using Microsoft.EntityFrameworkCore;
using PrometheonSuite.Identity.Entities.FiguraAggregate;
using PrometheonSuite.Identity.Entities.UtenteAggregate;

namespace PrometheonSuite.Identity.Infrastructure.Data;

public static class CoreSeedData
{
  public static async Task InitializeAsync(CoreDbContext dbContext)
  {
    await SeedCoreDataAsync(dbContext);
  }

  public static async Task SeedCoreDataAsync(CoreDbContext dbContext)
  {
    // 1) Prima seed delle figure (così esistono per eventuali assegnazioni future)
    await SeedFiguresAsync(dbContext);

    // 2) Poi seed admin user
    await SeedAdminAsync(dbContext);
  }

  private static async Task SeedAdminAsync(CoreDbContext dbContext)
  {
    var adminId = UtenteId.From(Guid.Parse("9C4436B6-6EA4-4562-B0BA-5E7CD331A751"));
    var adminEmail = Email.From("biagiomangani.dev@gmail.com");

    // Idempotenza: non uscire da tutta la seed, solo da questa parte
    var exists = await dbContext.Utenti.AnyAsync(u => u.Id == adminId /* || u.Email == adminEmail */);
    if (exists)
      return;

    var admin = Utente.CreateSeeded(
        adminId,
        Username.From("admin"),
        adminEmail,
        HashedPassword.FromHash("$2a$12$daTGXyeFPHelYaYQxjnuROTM1PgdQ5I/JG0ua.wqrqdXXQF/jgSGO"),
        attivo: true
    );

    dbContext.Utenti.Add(admin);
    await dbContext.SaveChangesAsync();
  }

  private static async Task SeedFiguresAsync(CoreDbContext dbContext)
  {
    // Se vuoi essere più robusto, controlla per code invece che "Any"
    //var hasAdmin = await dbContext.Figures.AnyAsync(f => f.Code == FigureCode.From("ADMIN"));
    //var hasUtenteBase = await dbContext.Figures.AnyAsync(f => f.Code == FigureCode.From("UTENTE_BASE"));

    //if (hasAdmin && hasUtenteBase)
    //  return;

    //if (!hasAdmin)
    //{
    //  var adminFigure = Figure.CreateSeeded(
    //      SeedFigureIds.Admin,
    //      FigureCode.From("ADMIN"),
    //      FigureName.From("Amministratore"),
    //      "Figura amministrativa di sistema"
    //  );

    //  dbContext.Figures.Add(adminFigure);
    //}

    //if (!hasUtenteBase)
    //{
    //  var utenteBaseFigure = Figure.CreateSeeded(
    //      SeedFigureIds.UtenteBase,
    //      FigureCode.From("UTENTE_BASE"),
    //      FigureName.From("Utente Base"),
    //      "Figura standard dell'applicazione"
    //  );

    //  dbContext.Figures.Add(utenteBaseFigure);
    //}

    await dbContext.SaveChangesAsync();
  }
}
