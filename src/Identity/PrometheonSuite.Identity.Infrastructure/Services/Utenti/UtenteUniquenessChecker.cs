
using PrometheonSuite.Identity.Core.Entities.UtenteAggregate.Specifications;
using PrometheonSuite.Identity.Core.Interfaces;
using PrometheonSuite.Identity.Entities.UtenteAggregate;
using PrometheonSuite.Identity.Entities.UtenteAggregate.Specifications;
using PrometheonSuite.Identity.Infrastructure.Data;
using PrometheonSuite.Identity.UseCases.Utenti.Services;

namespace PrometheonSuite.Identity.Infrastructure.Services.Utenti;



public class UtenteUniquenessChecker : IUtenteUniquenessChecker
{
  private readonly IServiceScopeFactory _scopeFactory;

  public UtenteUniquenessChecker(IServiceScopeFactory scopeFactory)
  {
    _scopeFactory = scopeFactory;
  }

  public Task<bool> EmailExistsAsync(Email email, CancellationToken ct) =>
    AnyAsync(new UtenteByEmailSpec(email), ct);

  public Task<bool> UsernameExistsAsync(Username username, CancellationToken ct) =>
    AnyAsync(new UtenteByUsernameSpec(username), ct);

  public Task<bool> EmailExistsForAnotherAsync(Email email, UtenteId excludeId, CancellationToken ct) =>
    AnyAsync(new UtenteByEmailExcludingIdSpec(email, excludeId), ct);

  public Task<bool> UsernameExistsForAnotherAsync(Username username, UtenteId excludeId, CancellationToken ct) =>
    AnyAsync(new UtenteByUsernameExcludingIdSpec(username, excludeId), ct);

  private async Task<bool> AnyAsync(Ardalis.Specification.ISpecification<Utente> spec, CancellationToken ct)
  {
    using var scope = _scopeFactory.CreateScope();
    var repo = scope.ServiceProvider.GetRequiredService<ICoreRepository<Utente>>();
    return await repo.AnyAsync(spec, ct);
  }
}

