using System;
using System.Collections.Generic;
using System.Text;
using PrometheonSuite.Identity.Entities.UtenteAggregate;

namespace PrometheonSuite.Identity.UseCases.Messaggistica.Contratti;

public record UtenteUsernameAggiornatoMessaggio(UtenteId idUtente, Username username);
