

using PrometheonSuite.Identity.BaseEntities;

namespace PrometheonSuite.Identity.Entities.TenantAggregate;

public class Tenant : AuditableEntity<Tenant, TenantId>, IAggregateRoot
{
  public TenantName Name { get; private set; }
  public TenantCode Code { get; private set; }
  public bool IsActive { get; private set; } = true;

  public Tenant(TenantName name, TenantCode code)
  {
    Id = TenantId.From(Guid.NewGuid());
    Name = name;
    Code = code;
  }

#pragma warning disable CS8618
  private Tenant()
  {
    Id = TenantId.From(Guid.Empty);
  }
#pragma warning restore CS8618

  public Tenant Disattiva()
  {
    IsActive = false;
    return this;
  }

  public Tenant Attiva()
  {
    IsActive = true;
    return this;
  }

  public Tenant AggiornaInfo(TenantName nuovoName, TenantCode nuovoCode)
  {
    Name = nuovoName;
    Code = nuovoCode;
    return this;
  }
}
