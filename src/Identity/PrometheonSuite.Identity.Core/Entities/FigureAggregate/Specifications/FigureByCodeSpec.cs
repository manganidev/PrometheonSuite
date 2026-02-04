namespace PrometheonSuite.Identity.Entities.FigureAggregate.Specifications;

public class FigureByCodeSpec : Specification<Figure>
{
  public FigureByCodeSpec(FigureCode code)
  {
    Query
      .Where(f => f.Code == code)
      .Include(f => f.FigureRoles);
  }
}
