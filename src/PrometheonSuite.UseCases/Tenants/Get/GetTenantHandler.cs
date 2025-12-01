using PrometheonSuite.Identity.Entities.TenantAggregate;
using PrometheonSuite.Identity.Entities.TenantAggregate.Specifications;
using PrometheonSuite.Identity.Core.Interfaces;
namespace  PrometheonSuite.Identity.UseCases.Tenants.Get;

public class GetTenantHandler(ICoreRepository<Tenant> repository)
  : IQueryHandler<GetTenantQuery, Result<TenantDto>>
{
  private readonly ICoreRepository<Tenant> _repository = repository;

  public async ValueTask<Result<TenantDto>> Handle(GetTenantQuery request, CancellationToken cancellationToken)
  {
    var tenant = await _repository.FirstOrDefaultAsync(
      new TenantByIdSpec(request.TenantId), 
      cancellationToken);

    if (tenant == null)
    {
      return Result<TenantDto>.NotFound();
    }

    var dto = new TenantDto(tenant.Id, tenant.Name, tenant.Code, tenant.IsActive);
    return Result<TenantDto>.Success(dto);
  }
}
