using PrometheonSuite.Identity.Entities.UtenteApplicazioneAziendaAggregate;
using PrometheonSuite.Identity.Entities.FiguraAggregate;

namespace  PrometheonSuite.Identity.UseCases.UtenteApplicazioneAziendas.RemoveFigure;

public record RemoveFigureFromUtenteApplicazioneAziendaCommand(
  UtenteApplicazioneAziendaId UtenteApplicazioneAziendaId,
  FiguraId FigureId
) : ICommand<Result<UtenteApplicazioneAziendaDto>>;
