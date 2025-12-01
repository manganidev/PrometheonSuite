namespace PrometheonSuite.Identity.Core.Interfaces;

// Repository RW
public interface ICoreRepository<T> : IRepositoryBase<T>
  where T : class, IAggregateRoot
{
}

// Repository R
public interface ICoreReadRepository<T> : IReadRepositoryBase<T>
  where T : class, IAggregateRoot
{
}
