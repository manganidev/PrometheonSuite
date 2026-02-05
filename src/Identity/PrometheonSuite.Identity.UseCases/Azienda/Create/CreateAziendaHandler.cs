using PrometheonSuite.Identity.Core.Interfaces;
using PrometheonSuite.Identity.Entities.AziendaAggregate;
using PrometheonSuite.Identity.Entities.AziendaAggregate.Specifications;

namespace  PrometheonSuite.Identity.UseCases.Aziendas.Create;

public class CreateAziendaHandler(ICoreRepository<Azienda> repository)
  : ICommandHandler<CreateAziendaCommand, Result<AziendaId>>
{
  private readonly ICoreRepository<Azienda> _repository = repository;

  public async ValueTask<Result<AziendaId>> Handle(CreateAziendaCommand request, CancellationToken cancellationToken)
  {
    // Check if tenant code already exists
    var existingAzienda = await _repository.FirstOrDefaultAsync(
      new AziendaByCodeSpec(request.Code), 
      cancellationToken);

    if (existingAzienda != null)
    {
      return Result<AziendaId>.Error($"A tenant with code '{request.Code.Value}' already exists.");
    }

    var tenant = new Azienda(request.Name, request.Code);
    
    var createdAzienda = await _repository.AddAsync(tenant, cancellationToken);
    await _repository.SaveChangesAsync(cancellationToken);

    return Result<AziendaId>.Success(createdAzienda.Id);
  }
}
