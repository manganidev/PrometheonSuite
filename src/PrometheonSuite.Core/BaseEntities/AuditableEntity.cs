using System;
using System.Collections.Generic;
using System.Text;

namespace PrometheonSuite.Identity.BaseEntities;

public abstract class AuditableEntity<T, TId>
  : EntityBase<T, TId>
  where T : AuditableEntity<T, TId>, IAggregateRoot
{
  public DateTime CreatedDate { get; set; } = DateTime.UtcNow;
  public string? CreatedBy { get; set; }

  public DateTime? ModifiedDate { get; set; }
  public string? ModifiedBy { get; set; }
}
