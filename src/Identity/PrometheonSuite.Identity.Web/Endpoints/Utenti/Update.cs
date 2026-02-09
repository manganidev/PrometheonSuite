
using FluentValidation;
using Microsoft.AspNetCore.Http.HttpResults;
using PrometheonSuite.Identity.Entities.UtenteAggregate;
using PrometheonSuite.Identity.UseCases.Utenti;
using PrometheonSuite.Identity.UseCases.Utenti.Comand.Update;
using PrometheonSuite.Identity.UseCases.Utenti.Services;
using PrometheonSuite.Identity.Web.Extensions;

namespace PrometheonSuite.Identity.Web.Endpoints.Utenti;

public class Update(IMediator mediator)
  : Endpoint<
        UpdateUtenteRequest,
        Results<Ok<UpdateUtenteResponse>, NotFound, ProblemHttpResult>,
        UpdateUtenteMapper>
{
  private readonly IMediator _mediator = mediator;

  public override void Configure()
  {
    Put(UpdateUtenteRequest.Route);
    AllowAnonymous();

    Summary(s =>
    {
      s.Summary = "Update a user";
      s.Description = "Updates an existing user's information. The username must be between 2 and 150 characters long and the email must be valid.";
      s.ExampleRequest = new UpdateUtenteRequest { UtenteId = Guid.NewGuid(), Username = "Updated Name", Email = "updated@example.com" };
      s.ResponseExamples[200] = new UpdateUtenteResponse(new UtenteRecord(Guid.NewGuid(), "Updated Name", "updated@example.com", true));

      s.Responses[200] = "User updated successfully";
      s.Responses[404] = "User with specified ID not found";
      s.Responses[400] = "Invalid input data or business rule violation";
    });

    Tags("Utenti");

    Description(builder => builder
      .Accepts<UpdateUtenteRequest>("application/json")
      .Produces<UpdateUtenteResponse>(200, "application/json")
      .ProducesProblem(404)
      .ProducesProblem(400));
  }

  public override async Task<Results<Ok<UpdateUtenteResponse>, NotFound, ProblemHttpResult>>
    ExecuteAsync(UpdateUtenteRequest request, CancellationToken ct)
  {
    var cmd = new UpdateUtenteCommand(
      UtenteId.From(request.UtenteId),
      Username.From(request.Username!),
      Email.From(request.Email!));

    var result = await _mediator.Send(cmd, ct);

    return result.ToUpdateResult(Map.FromEntity);
  }
}

public class UpdateUtenteRequest
{
  public const string Route = "/Utenti/{UtenteId:guid}";

  public static string BuildRoute(Guid utenteId) =>
    Route.Replace("{UtenteId:guid}", utenteId.ToString());

  public Guid UtenteId { get; set; }
  public string Username { get; set; } = string.Empty;
  public string Email { get; set; } = string.Empty;
}

public class UpdateUtenteValidator : Validator<UpdateUtenteRequest>
{
  public UpdateUtenteValidator(IUtenteUniquenessChecker uniqueness)
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

    RuleFor(x => x.Email)
  .NotEmpty()
  .EmailAddress()
  .MustAsync(async (req, email, ct) =>
    !await uniqueness.EmailExistsForAnotherAsync(
      Email.From(email),
      UtenteId.From(req.UtenteId),
      ct))
  .WithMessage("Email already exists.");

    RuleFor(x => x.Username)
      .NotEmpty()
      .MinimumLength(2)
      .MaximumLength(Username.MaxLength)
      .MustAsync(async (req, username, ct) =>
        !await uniqueness.UsernameExistsForAnotherAsync(
          Username.From(username),
          UtenteId.From(req.UtenteId),
          ct))
      .WithMessage("Username already exists.");
  }

}

public record UpdateUtenteResponse(UtenteRecord Utente);

public sealed class UpdateUtenteMapper
  : Mapper<UpdateUtenteRequest, UpdateUtenteResponse, UtenteDto>
{
  public override UpdateUtenteResponse FromEntity(UtenteDto e)
    => new(new UtenteRecord(e.Id.Value, e.Username.Value, e.Email.Value, e.Attivo));
}
