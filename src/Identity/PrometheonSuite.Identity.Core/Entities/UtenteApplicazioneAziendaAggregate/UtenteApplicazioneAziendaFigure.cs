
using PrometheonSuite.Identity.Entities.FiguraAggregate;

namespace PrometheonSuite.Identity.Entities.UtenteApplicazioneAziendaAggregate;

public class UtenteApplicazioneAziendaFigura
{
  public UtenteApplicazioneAziendaId UtenteApplicazioneAziendaId { get; private set; }
  public FiguraId FiguraId { get; private set; }

  public UtenteApplicazioneAziendaFigura(UtenteApplicazioneAziendaId userTenantId, FiguraId figureId)
  {
    UtenteApplicazioneAziendaId = userTenantId;
    FiguraId = figureId;
  }

#pragma warning disable CS8618
  private UtenteApplicazioneAziendaFigura()
  {
  }
#pragma warning restore CS8618
}
