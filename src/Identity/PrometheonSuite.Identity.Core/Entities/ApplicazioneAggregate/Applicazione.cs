

using PrometheonSuite.Identity.BaseEntities;
using static System.Net.Mime.MediaTypeNames;

namespace PrometheonSuite.Identity.Entities.ApplicazioneAggregate;

public class Applicazione : AuditableEntity<Applicazione, ApplicazioneId>, IAggregateRoot
{
  public ApplicazioneName Name { get; private set; }
  public ApplicazioneCode Code { get; private set; }
  public bool IsActive { get; private set; } = true;

  public Applicazione(ApplicazioneName name, ApplicazioneCode code)
  {
    Id = ApplicazioneId.From(Guid.NewGuid());
    Name = name;
    Code = code;
  }

#pragma warning disable CS8618
  private Applicazione()
  {
    Id = ApplicazioneId.From(Guid.Empty);
  }
#pragma warning restore CS8618

  public Applicazione Disattiva()
  {
    IsActive = false;
    return this;
  }

  public Applicazione Attiva()
  {
    IsActive = true;
    return this;
  }

  public Applicazione AggiornaInfo(ApplicazioneName nuovoName, ApplicazioneCode nuovoCode)
  {
    Name = nuovoName;
    Code = nuovoCode;
    return this;
  }
}
