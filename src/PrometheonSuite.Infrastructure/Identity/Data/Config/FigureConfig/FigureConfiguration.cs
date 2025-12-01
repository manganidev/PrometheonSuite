

using PrometheonSuite.Identity.Entities.FigureAggregate;

namespace PrometheonSuite.Infrastructure.Identity.Data.Config;

public class FigureConfiguration : IEntityTypeConfiguration<Figure>
{
  public void Configure(EntityTypeBuilder<Figure> builder)
  {
    builder.ToTable("Figures");

    builder.HasKey(x => x.Id);
    builder.Property(x => x.Id)
      .HasConversion(
        id => id.Value,
        value => FigureId.From(value))
      .ValueGeneratedOnAdd();

    builder.Property(x => x.Code)
      .HasConversion(
        vo => vo.Value,
        v => FigureCode.From(v))
      .HasMaxLength(FigureCode.MaxLength)
      .IsRequired();

    builder.Property(x => x.Name)
      .HasConversion(
        vo => vo.Value,
        v => FigureName.From(v))
      .HasMaxLength(FigureName.MaxLength)
      .IsRequired();

    builder.HasIndex(x => x.Code).IsUnique();

    builder.Property(x => x.Description)
      .HasMaxLength(500);

    builder.HasMany(f => f.FigureRoles)
      .WithOne()
      .HasForeignKey(fr => fr.FigureId)
      .OnDelete(DeleteBehavior.Cascade);
  }
}
