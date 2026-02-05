using Vogen;

namespace PrometheonSuite.Identity.Entities.FiguraAggregate;

[ValueObject<Guid>]
public readonly partial struct FiguraId
{
}
public static class SeedFiguraIds
{
  public static readonly FiguraId Admin =
      FiguraId.From(Guid.Parse("E0C53C7E-8A6C-4E6A-9F13-2C0E91E6C001"));

  public static readonly FiguraId UtenteBase =
      FiguraId.From(Guid.Parse("A5A42F19-7D4B-4C33-9D59-EEA6A0B12001"));
}
