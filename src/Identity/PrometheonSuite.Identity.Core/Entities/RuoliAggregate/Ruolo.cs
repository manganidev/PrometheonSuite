

using PrometheonSuite.Identity.BaseEntities;
using PrometheonSuite.Identity.Entities.ApplicazioneAggregate;

namespace PrometheonSuite.Identity.Entities.RuoloAggregate;

public class Ruolo : AuditableEntity<Ruolo, RuoloId>, IAggregateRoot
{
  public RuoloCode Code { get; private set; }
  public RuoloName Name { get; private set; }
  public string? Description { get; private set; }
  public ApplicazioneId ApplicazioneId { get; private set; }

  public Ruolo(RuoloCode code, RuoloName name, ApplicazioneId applicazioneId, string? description = null)
  {
    Id = RuoloId.From(Guid.NewGuid());
    Code = code;
    Name = name;
    ApplicazioneId = applicazioneId;
    Description = description;
  }

#pragma warning disable CS8618
  private Ruolo()
  {
    Id = RuoloId.From(Guid.Empty);
  }
#pragma warning restore CS8618

  public Ruolo AggiornaInfo(RuoloCode nuovoCode, RuoloName nuovoName, string? nuovaDescription)
  {
    Code = nuovoCode;
    Name = nuovoName;
    Description = nuovaDescription;
    return this;
  }
}
