using PrometheonSuite.Identity.Entities.UtenteAggregate;
using Vogen;


namespace PrometheonSuite.Infrastructure.Identity.Data.Config;

[EfCoreConverter<UtenteId>]
[EfCoreConverter<Username>]
[EfCoreConverter<Email>]
internal partial class VogenEfCoreConverters;
