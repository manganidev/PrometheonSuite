using PrometheonSuite.Identity.Entities.UtenteAggregate;
using PrometheonSuite.Identity.Entities.TenantAggregate;

namespace  PrometheonSuite.Identity.UseCases.UserTenants.Create;

public record CreateUserTenantCommand(
  UtenteId UserId,
  TenantId TenantId,
  string? DefaultLocale = null
) : ICommand<Result<UserTenantDto>>;
