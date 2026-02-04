using FluentValidation;
using Microsoft.AspNetCore.Http.HttpResults;
using PrometheonSuite.Identity.Entities.UtenteAggregate;
using PrometheonSuite.Identity.UseCases.Auth.Login;
using PrometheonSuite.Identity.Web.Extensions;

namespace PrometheonSuite.Identity.Web.Endpoints.Auth;

public class Login(IMediator mediator)
  : Endpoint<LoginRequest,
             Results<Ok<LoginResponse>,
                     ValidationProblem,
                     ProblemHttpResult>>
{
  private readonly IMediator _mediator = mediator;

  public override void Configure()
  {
    Post(LoginRequest.Route);
    AllowAnonymous();

    Summary(s =>
    {
      s.Summary = "Authenticate user";
      s.Description = "Authenticates a user with username and password and returns JWT access and refresh tokens.";
      s.ExampleRequest = new LoginRequest { Username = "johndoe", Password = "SecureP@ss123" };

      s.Responses[200] = "Login successful";
      s.Responses[400] = "Invalid input data - validation errors";
      s.Responses[401] = "Invalid credentials";
    });

    Tags("Auth");

    Description(builder => builder
      .Accepts<LoginRequest>("application/json")
      .Produces<LoginResponse>(200, "application/json")
      .ProducesProblem(400)
      .ProducesProblem(401));
  }

  public override async Task<Results<Ok<LoginResponse>, ValidationProblem, ProblemHttpResult>>
    ExecuteAsync(LoginRequest request, CancellationToken cancellationToken)
  {
    var result = await _mediator.Send(new LoginCommand(Username.From(request.Username!), request.Password!), cancellationToken);

    return result.Status switch
    {
      ResultStatus.Ok => TypedResults.Ok(new LoginResponse(
        result.Value.AccessToken,
        result.Value.RefreshToken,
        result.Value.AccessTokenExpiresAt)),
      ResultStatus.NotFound => TypedResults.Problem(
        title: "Invalid credentials",
        detail: "User not found",
        statusCode: StatusCodes.Status401Unauthorized),
      ResultStatus.Error => TypedResults.Problem(
        title: "Invalid credentials",
        detail: string.Join("; ", result.Errors),
        statusCode: StatusCodes.Status401Unauthorized),
      _ => TypedResults.Problem(
        title: "Login failed",
        detail: string.Join("; ", result.Errors),
        statusCode: StatusCodes.Status400BadRequest)
    };
  }
}

public class LoginRequest
{
  public const string Route = "/Auth/Login";

  public string Username { get; set; } = string.Empty;
  public string Password { get; set; } = string.Empty;
}

public class LoginValidator : Validator<LoginRequest>
{
  public LoginValidator()
  {
    RuleFor(x => x.Username)
      .NotEmpty()
      .MinimumLength(2)
      .MaximumLength(Username.MaxLength);

    RuleFor(x => x.Password)
      .NotEmpty()
      .MinimumLength(8);
  }
}

public record LoginResponse(string AccessToken, string RefreshToken, DateTimeOffset AccessTokenExpiresAt);
