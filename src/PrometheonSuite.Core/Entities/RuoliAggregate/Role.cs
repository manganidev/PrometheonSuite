

using PrometheonSuite.Identity.BaseEntities;

namespace PrometheonSuite.Identity.Entities.RoleAggregate;

public class Role : AuditableEntity<Role, RoleId>, IAggregateRoot
{
  public RoleCode Code { get; private set; }
  public RoleName Name { get; private set; }
  public string? Description { get; private set; }

  public Role(RoleCode code, RoleName name, string? description = null)
  {
    Id = RoleId.From(Guid.NewGuid());
    Code = code;
    Name = name;
    Description = description;
  }

#pragma warning disable CS8618
  private Role()
  {
    Id = RoleId.From(Guid.Empty);
  }
#pragma warning restore CS8618

  public Role AggiornaInfo(RoleCode nuovoCode, RoleName nuovoName, string? nuovaDescription)
  {
    Code = nuovoCode;
    Name = nuovoName;
    Description = nuovaDescription;
    return this;
  }
}
