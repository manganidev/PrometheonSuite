

using PrometheonSuite.Identity.BaseEntities;
using PrometheonSuite.Identity.Entities.ApplicazioneAggregate;
using PrometheonSuite.Identity.Entities.AziendaAggregate;
using PrometheonSuite.Identity.Entities.FiguraAggregate;
using PrometheonSuite.Identity.Entities.RuoloAggregate;
using PrometheonSuite.Identity.Entities.UtenteAggregate;

namespace PrometheonSuite.Identity.Entities.UtenteApplicazioneAziendaAggregate;

public class UtenteApplicazioneAzienda : AuditableEntity<UtenteApplicazioneAzienda, UtenteApplicazioneAziendaId>, IAggregateRoot
{
  public UtenteId UserId { get; private set; }
  public AziendaId AziendaId { get; private set; }
  public ApplicazioneId ApplicazioneId { get; private set; }
  public bool IsActive { get; private set; } = true;
  public string? DefaultLocale { get; private set; }

  private readonly List<UtenteApplicazioneAziendaFigura> _userAziendaFiguras = new();
  public IReadOnlyCollection<UtenteApplicazioneAziendaFigura> UtenteApplicazioneAziendaFiguras => _userAziendaFiguras.AsReadOnly();

  //private readonly List<UtenteApplicazioneAziendaRuolo> _userAziendaRuolos = new();
  //public IReadOnlyCollection<UtenteApplicazioneAziendaRuolo> UtenteApplicazioneAziendaRuolos => _userAziendaRuolos.AsReadOnly();

  public UtenteApplicazioneAzienda(UtenteId userId, AziendaId tenantId, ApplicazioneId applicazioneId, string? defaultLocale = null)
  {
    Id = UtenteApplicazioneAziendaId.From(Guid.NewGuid());
    UserId = userId;
    ApplicazioneId = applicazioneId;
    AziendaId = tenantId;
    DefaultLocale = defaultLocale;
  }

#pragma warning disable CS8618
  private UtenteApplicazioneAzienda()
  {
    Id = UtenteApplicazioneAziendaId.From(Guid.Empty);
  }
#pragma warning restore CS8618

  public UtenteApplicazioneAzienda Disattiva()
  {
    IsActive = false;
    return this;
  }

  public UtenteApplicazioneAzienda Attiva()
  {
    IsActive = true;
    return this;
  }

  public UtenteApplicazioneAzienda AggiornaLocale(string? nuovaLocale)
  {
    DefaultLocale = nuovaLocale;
    return this;
  }

  public UtenteApplicazioneAzienda AggiungiFigura(FiguraId figureId)
  {
    if (!_userAziendaFiguras.Any(utf => utf.FiguraId == figureId))
    {
      _userAziendaFiguras.Add(new UtenteApplicazioneAziendaFigura(Id, figureId));
    }
    return this;
  }

  public UtenteApplicazioneAzienda RimuoviFigura(FiguraId figureId)
  {
    var userAziendaFigura = _userAziendaFiguras.FirstOrDefault(utf => utf.FiguraId == figureId);
    if (userAziendaFigura != null)
    {
      _userAziendaFiguras.Remove(userAziendaFigura);
    }
    return this;
  }


}
