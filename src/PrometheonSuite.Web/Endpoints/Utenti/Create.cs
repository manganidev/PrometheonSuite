using System.ComponentModel.DataAnnotations;

using FluentValidation;
using Microsoft.AspNetCore.Http.HttpResults;
using PrometheonSuite.Identity.Entities.UtenteAggregate;
using PrometheonSuite.Identity.UseCases.Utenti.Create;
using PrometheonSuite.Identity.Web.Extensions;

namespace PrometheonSuite.Identity.Web.Endpoints.Utenti;

public class Create(IMediator mediator)
  : Endpoint<CreateUtenteRequest,
          Results<Created<CreateUtenteResponse>,
                          ValidationProblem,
                          ProblemHttpResult>>
{
  private readonly IMediator _mediator = mediator;

  public override void Configure()
  {
    Post(CreateUtenteRequest.Route);
    AllowAnonymous();
    Summary(s =>
    {
      s.Summary = "Create a new user";
      s.Description = "Creates a new user with the provided username, email and password. Password must be at least 8 characters long.";
      s.ExampleRequest = new CreateUtenteRequest { Username = "johndoe", Email = "john.doe@example.com", Password = "SecureP@ss123" };
      s.ResponseExamples[201] = new CreateUtenteResponse(Guid.NewGuid(), "johndoe", "john.doe@example.com", true);

      s.Responses[201] = "User created successfully";
      s.Responses[400] = "Invalid input data - validation errors";
      s.Responses[500] = "Internal server error";
    });

    Tags("Utenti");

    Description(builder => builder
      .Accepts<CreateUtenteRequest>("application/json")
      .Produces<CreateUtenteResponse>(201, "application/json")
      .ProducesProblem(400)
      .ProducesProblem(500));
  }

  public override async Task<Results<Created<CreateUtenteResponse>, ValidationProblem, ProblemHttpResult>>
    ExecuteAsync(CreateUtenteRequest request, CancellationToken cancellationToken)
  {
    var result = await _mediator.Send(new CreateUtenteCommand(
      Username.From(request.Username!), 
      Email.From(request.Email!),
      request.Password!), cancellationToken);

    return result.ToCreatedResult(
      dto => $"/Utenti/{dto.Id.Value}",
      dto => new CreateUtenteResponse(dto.Id.Value, dto.Username.Value, dto.Email.Value, dto.Attivo));
  }
}

public class CreateUtenteRequest
{
  public const string Route = "/Utenti";

  [Required]
  public string Username { get; set; } = string.Empty;
  
  [Required]
  public string Email { get; set; } = string.Empty;

  [Required]
  public string Password { get; set; } = string.Empty;
}

public class CreateUtenteValidator : Validator<CreateUtenteRequest>
{
  public CreateUtenteValidator()
  {
    RuleFor(x => x.Username)
      .NotEmpty()
      .WithMessage("Username is required.")
      .MinimumLength(2)
      .MaximumLength(Username.MaxLength);

    RuleFor(x => x.Email)
      .NotEmpty()
      .WithMessage("Email is required.")
      .EmailAddress()
      .WithMessage("Email must be valid.");

    RuleFor(x => x.Password)
      .NotEmpty()
      .WithMessage("Password is required.")
      .MinimumLength(8)
      .WithMessage("Password must be at least 8 characters long.")
      .Matches(@"[A-Z]")
      .WithMessage("Password must contain at least one uppercase letter.")
      .Matches(@"[a-z]")
      .WithMessage("Password must contain at least one lowercase letter.")
      .Matches(@"\d")
      .WithMessage("Password must contain at least one number.");
  }
}

public record CreateUtenteResponse(Guid Id, string Username, string Email, bool Attivo);
