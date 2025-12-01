using PrometheonSuite.Identity.Entities.TenantAggregate;
using PrometheonSuite.Identity.Entities.TenantAggregate.Specifications;

namespace  PrometheonSuite.Identity.UseCases.Tenants.Delete;

public class DeleteTenantHandler(IRepository<Tenant> repository)
  : ICommandHandler<DeleteTenantCommand, Result>
{
  private readonly IRepository<Tenant> _repository = repository;

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
