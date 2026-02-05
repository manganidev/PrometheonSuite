using System;
using System.Collections.Generic;
using System.Text;
using PrometheonSuite.Identity.BaseEntities;
using PrometheonSuite.Identity.Entities.ApplicazioneAggregate;
using PrometheonSuite.Identity.Entities.AziendaAggregate;

namespace PrometheonSuite.Identity.Entities.AziendaApplicazioneAggregate;


public class AziendaApplicazione : AuditableEntity<AziendaApplicazione, AziendaApplicazioneId>, IAggregateRoot
{

  public AziendaId AziendaId { get; private set; }
  public ApplicazioneId ApplicazioneId { get; private set; }
  public bool IsActive { get; private set; } = true;

  public AziendaApplicazione(AziendaId aziendaId, ApplicazioneId applicazioneId)
  {
    Id = AziendaApplicazioneId.From(Guid.NewGuid());
    AziendaId = aziendaId;
    ApplicazioneId = applicazioneId;

  }

#pragma warning disable CS8618
  private AziendaApplicazione()
  {
    Id = AziendaApplicazioneId.From(Guid.Empty);
  }
#pragma warning restore CS8618

  public AziendaApplicazione Disattiva()
  {
    IsActive = false;
    return this;
  }

  public AziendaApplicazione Attiva()
  {
    IsActive = true;
    return this;
  }
}

