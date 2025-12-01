namespace PrometheonSuite.Infrastructure.PaddockHR.Data;

// inherit from Ardalis.Specification type
public class EfRepository<T>(PaddockHRDbContext dbContext) :
  RepositoryBase<T>(dbContext), IReadRepository<T>, IRepository<T> where T : class, IAggregateRoot
{
}
