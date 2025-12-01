namespace PrometheonSuite.Infrastructure.Identity.Data;

// inherit from Ardalis.Specification type
public class EfRepository<T>(CoreDbContext dbContext) :
  RepositoryBase<T>(dbContext), IReadRepository<T>, IRepository<T> where T : class, IAggregateRoot
{
}
