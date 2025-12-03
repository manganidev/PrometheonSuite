using System.Threading;
using PrometheonSuite.Identity.Core.Interfaces;
using PrometheonSuite.Identity.Entities.UtenteAggregate;
using PrometheonSuite.Identity.Entities.UtenteAggregate.Specifications;

namespace PrometheonSuite.Identity.UseCases.Auth.Login;

public class LoginHandler(
  ICoreRepository<Utente> users,
  ITokenService tokenService)
  : ICommandHandler<LoginCommand, Result<AuthResult>>
{
  private readonly ICoreRepository<Utente> _users = users;
  private readonly ITokenService _tokenService = tokenService;

  public async ValueTask<Result<AuthResult>> Handle(LoginCommand request, CancellationToken cancellationToken)
  {
    var user = await _users.FirstOrDefaultAsync(
      new UtenteByUsernameSpec(request.Username),
      cancellationToken);

    if (user is null)
    {
      return Result<AuthResult>.NotFound("User not found");
    }

    if (!user.VerificaPassword(request.Password))
    {
      return Result<AuthResult>.Error("Invalid credentials");
    }

    var tokens = await _tokenService.GenerateTokensAsync(user, cancellationToken);

    return Result<AuthResult>.Success(tokens);
  }
}
