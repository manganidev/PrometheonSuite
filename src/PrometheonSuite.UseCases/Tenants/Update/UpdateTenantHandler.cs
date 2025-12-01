using PrometheonSuite.Identity.Entities.TenantAggregate;
using PrometheonSuite.Identity.Entities.TenantAggregate.Specifications;

namespace  PrometheonSuite.Identity.UseCases.Tenants.Update;

public class UpdateTenantHandler(IRepository<Tenant> repository)
  : ICommandHandler<UpdateTenantCommand, Result<TenantDto>>
{
  private readonly IRepository<Tenant> _repository = repository;

  public async ValueTask<Result<TenantDto>> Handle(UpdateTenantCommand request, CancellationToken cancellationToken)
  {
    var tenant = await _repository.FirstOrDefaultAsync(
      new TenantByIdSpec(request.TenantId), 
      cancellationToken);

    if (tenant == null)
    {
      return Result<TenantDto>.NotFound();
    }

    // Check if new code already exists on another tenant
    if (tenant.Code != request.Code)
    {
      var existingTenant = await _repository.FirstOrDefaultAsync(
        new TenantByCodeSpec(request.Code), 
        cancellationToken);

      if (existingTenant != null && existingTenant.Id != tenant.Id)
      {
        return Result<TenantDto>.Error($"A tenant with code '{request.Code.Value}' already exists.");
      }
    }

    tenant.AggiornaInfo(request.Name, request.Code);
    await _repository.SaveChangesAsync(cancellationToken);

    var dto = new TenantDto(tenant.Id, tenant.Name, tenant.Code, tenant.IsActive);
    return Result<TenantDto>.Success(dto);
  }
}
