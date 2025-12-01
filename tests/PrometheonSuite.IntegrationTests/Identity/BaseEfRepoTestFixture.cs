using PrometheonSuite.Infrastructure.Identity.Data;

namespace PrometheonSuite.IntegrationTests.Identity;

public abstract class BaseEfRepoTestFixture
{
  protected CoreDbContext _dbContext;

  protected BaseEfRepoTestFixture()
  {
    var options = CreateNewContextOptions();
    _dbContext = new CoreDbContext(options);
  }

  protected static DbContextOptions<CoreDbContext> CreateNewContextOptions()
  {
    var fakeEventDispatcher = Substitute.For<IDomainEventDispatcher>();
    // Create a fresh service provider, and therefore a fresh
    // InMemory database instance.
    var serviceProvider = new ServiceCollection()
        .AddEntityFrameworkInMemoryDatabase()
        .AddScoped<IDomainEventDispatcher>(_ => fakeEventDispatcher)
        .AddScoped<Core_EventDispatchInterceptor>()
        .BuildServiceProvider();

    // Create a new options instance telling the context to use an
    // InMemory database and the new service provider.
    var interceptor = serviceProvider.GetRequiredService<Core_EventDispatchInterceptor>();

    var builder = new DbContextOptionsBuilder<CoreDbContext>();
    builder.UseInMemoryDatabase("cleanarchitecture")
           .UseInternalServiceProvider(serviceProvider)
           .AddInterceptors(interceptor);

    return builder.Options;
  }

  //protected EfRepository<Contributor> GetRepository()
  //{
  //  return new EfRepository<Contributor>(_dbContext);
  //}
}
