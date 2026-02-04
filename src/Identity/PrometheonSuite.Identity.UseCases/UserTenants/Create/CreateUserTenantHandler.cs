using PrometheonSuite.Identity.Entities.UserTenantAggregate;
using PrometheonSuite.Identity.Entities.UserTenantAggregate.Specifications;
using PrometheonSuite.Identity.Entities.UtenteAggregate;
using PrometheonSuite.Identity.Entities.UtenteAggregate.Specifications;
using PrometheonSuite.Identity.Entities.TenantAggregate;
using PrometheonSuite.Identity.Entities.TenantAggregate.Specifications;
using PrometheonSuite.Identity.Core.Interfaces;
namespace  PrometheonSuite.Identity.UseCases.UserTenants.Create;

public class CreateUserTenantHandler(
  ICoreRepository<UserTenant> userTenantRepository,
  ICoreRepository<Utente> userRepository,
  ICoreRepository<Tenant> tenantRepository)
  : ICommandHandler<CreateUserTenantCommand, Result<UserTenantDto>>
{
  private readonly ICoreRepository<UserTenant> _userTenantRepository = userTenantRepository;
  private readonly ICoreRepository<Utente> _userRepository = userRepository;
  private readonly ICoreRepository<Tenant> _tenantRepository = tenantRepository;

  public async ValueTask<Result<UserTenantDto>> Handle(CreateUserTenantCommand request, CancellationToken cancellationToken)
  {
    // Verify user exists
    var user = await _userRepository.FirstOrDefaultAsync(
      new UtenteByIdSpec(request.UserId), 
      cancellationToken);

    if (user == null)
    {
      return Result<UserTenantDto>.NotFound("User not found");
    }

    // Verify tenant exists
    var tenant = await _tenantRepository.FirstOrDefaultAsync(
      new TenantByIdSpec(request.TenantId), 
      cancellationToken);

    if (tenant == null)
    {
      return Result<UserTenantDto>.NotFound("Tenant not found");
    }

    // Check if association already exists
    var existing = await _userTenantRepository.FirstOrDefaultAsync(
      new UserTenantByUserAndTenantSpec(request.UserId, request.TenantId), 
      cancellationToken);

    if (existing != null)
    {
      return Result<UserTenantDto>.Error("User is already associated with this tenant");
    }

    var userTenant = new UserTenant(request.UserId, request.TenantId, request.DefaultLocale);
    
    var created = await _userTenantRepository.AddAsync(userTenant, cancellationToken);
    await _userTenantRepository.SaveChangesAsync(cancellationToken);

    var dto = new UserTenantDto(
      created.Id,
      created.UserId,
      created.TenantId,
      created.IsActive,
      created.DefaultLocale,
      new List<Guid>(),
      new List<Guid>());

    return Result<UserTenantDto>.Success(dto);
  }
}
