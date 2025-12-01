using PrometheonSuite.Identity.Core.Interfaces;

namespace PrometheonSuite.Infrastructure.Identity.Data;

// inherit from Ardalis.Specification type
public class CoreEfRepository<T>
  : RepositoryBase<T>, ICoreRepository<T>, ICoreReadRepository<T>
  where T : class, IAggregateRoot
{
  public CoreEfRepository(CoreDbContext dbContext)
    : base(dbContext)
  {
  }
}
