

using PrometheonSuite.Identity.BaseEntities;

namespace PrometheonSuite.Identity.Entities.AziendaAggregate;

public class Azienda : AuditableEntity<Azienda, AziendaId>, IAggregateRoot
{
  public AziendaName Name { get; private set; }
  public AziendaCode Code { get; private set; }
  public bool IsActive { get; private set; } = true;

  public Azienda(AziendaName name, AziendaCode code)
  {
    Id = AziendaId.From(Guid.NewGuid());
    Name = name;
    Code = code;
  }

#pragma warning disable CS8618
  private Azienda()
  {
    Id = AziendaId.From(Guid.Empty);
  }
#pragma warning restore CS8618

  public Azienda Disattiva()
  {
    IsActive = false;
    return this;
  }

  public Azienda Attiva()
  {
    IsActive = true;
    return this;
  }

  public Azienda AggiornaInfo(AziendaName nuovoName, AziendaCode nuovoCode)
  {
    Name = nuovoName;
    Code = nuovoCode;
    return this;
  }
}
