using PrometheonSuite.Identity.Entities.TenantAggregate;
using PrometheonSuite.Identity.Entities.TenantAggregate.Specifications;
using PrometheonSuite.Identity.Core.Interfaces;
namespace  PrometheonSuite.Identity.UseCases.Tenants.Delete;

public class DeleteTenantHandler(ICoreRepository<Tenant> repository)
  : ICommandHandler<DeleteTenantCommand, Result>
{
  private readonly ICoreRepository<Tenant> _repository = repository;

  public async ValueTask<Result> Handle(DeleteTenantCommand request, CancellationToken cancellationToken)
  {
    var tenant = await _repository.FirstOrDefaultAsync(
      new TenantByIdSpec(request.TenantId), 
      cancellationToken);

    if (tenant == null)
    {
      return Result.NotFound();
    }

    // Soft delete by setting IsActive to false
    tenant.Disattiva();
    await _repository.SaveChangesAsync(cancellationToken);

    return Result.Success();
  }
}
