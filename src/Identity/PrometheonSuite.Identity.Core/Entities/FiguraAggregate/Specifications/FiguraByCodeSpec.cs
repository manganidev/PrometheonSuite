namespace PrometheonSuite.Identity.Entities.FiguraAggregate.Specifications;

public class FiguraByCodeSpec : Specification<Figura>
{
  public FiguraByCodeSpec(FiguraCode code)
  {
    Query
      .Where(f => f.Code == code)
      .Include(f => f.FiguraRuolos);
  }
}
