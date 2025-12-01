namespace PrometheonSuite.PaddockHR.Core.Interfaces;


// Repository RW
public interface IPaddockRepository<T> : IRepositoryBase<T>
  where T : class, IAggregateRoot
{
}

// Repository R
public interface IPaddockReadRepository<T> : IReadRepositoryBase<T>
  where T : class, IAggregateRoot
{
}
