using Ardalis.SharedKernel;
using PrometheonSuite.PaddockHR.Core.Interfaces;

namespace PrometheonSuite.PaddockHR.Infrastructure.Data;

// inherit from Ardalis.Specification type

public class PaddockEfRepository<T>
  : RepositoryBase<T>, IPaddockRepository<T>, IPaddockReadRepository<T>
  where T : class, IAggregateRoot
{
  public PaddockEfRepository(PaddockHRDbContext dbContext)
    : base(dbContext)
  {
  }
}
