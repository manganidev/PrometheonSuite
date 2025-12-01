namespace PrometheonSuite.Identity.Entities.FigureAggregate.Specifications;

public class FigureByIdSpec : Specification<Figure>
{
  public FigureByIdSpec(FigureId figureId)
  {
    Query
      .Where(f => f.Id == figureId)
      .Include(f => f.FigureRoles);
  }
}
