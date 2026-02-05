using PrometheonSuite.Identity.Entities.UtenteApplicazioneAziendaAggregate;
using PrometheonSuite.Identity.Entities.FiguraAggregate;

namespace  PrometheonSuite.Identity.UseCases.UtenteApplicazioneAziendas.AggiungiFigura;

public record AddFiguraToUtenteApplicazioneAziendaCommand(
  UtenteApplicazioneAziendaId UtenteApplicazioneAziendaId,
  FiguraId FiguraId
) : ICommand<Result<UtenteApplicazioneAziendaDto>>;
