namespace PrometheonSuite.Identity.Entities.FiguraAggregate.Specifications;

public class FiguraByIdSpec : Specification<Figura>
{
  public FiguraByIdSpec(FiguraId figureId)
  {
    Query
      .Where(f => f.Id == figureId)
      .Include(f => f.FiguraRuolos);
  }
}
