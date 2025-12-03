using PrometheonSuite.Identity.Core.Interfaces;
using PrometheonSuite.Identity.Entities.UtenteAggregate;

namespace  PrometheonSuite.Identity.UseCases.Auth.Login;

public record LoginCommand(
  Username Username,
  string Password
) : ICommand<Result<AuthResult>>;
