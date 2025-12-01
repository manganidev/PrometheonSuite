using PrometheonSuite.Identity.Entities.TenantAggregate;
using PrometheonSuite.Identity.Entities.TenantAggregate.Specifications;
using PrometheonSuite.Identity.Core.Interfaces;
namespace  PrometheonSuite.Identity.UseCases.Tenants.Create;

public class CreateTenantHandler(ICoreRepository<Tenant> repository)
  : ICommandHandler<CreateTenantCommand, Result<TenantId>>
{
  private readonly ICoreRepository<Tenant> _repository = repository;

  public async ValueTask<Result<TenantId>> Handle(CreateTenantCommand request, CancellationToken cancellationToken)
  {
    // Check if tenant code already exists
    var existingTenant = await _repository.FirstOrDefaultAsync(
      new TenantByCodeSpec(request.Code), 
      cancellationToken);

    if (existingTenant != null)
    {
      return Result<TenantId>.Error($"A tenant with code '{request.Code.Value}' already exists.");
    }

    var tenant = new Tenant(request.Name, request.Code);
    
    var createdTenant = await _repository.AddAsync(tenant, cancellationToken);
    await _repository.SaveChangesAsync(cancellationToken);

    return Result<TenantId>.Success(createdTenant.Id);
  }
}
