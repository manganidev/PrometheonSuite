using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PrometheonSuite.Identity.Entities.UtenteAggregate;


namespace PrometheonSuite.Infrastructure.Identity.Data.Config;

public class UtenteConfiguration : IEntityTypeConfiguration<Utente>
{
  public void Configure(EntityTypeBuilder<Utente> builder)
  {
    builder.ToTable("Utenti");

    builder.HasKey(x => x.Id);
    builder.Property(x => x.Id)
      .HasConversion(
        id => id.Value,
        value => UtenteId.From(value))
      .ValueGeneratedOnAdd();

    builder.Property(x => x.Username)
      .HasConversion(
        vo => vo.Value,
        v => Username.From(v))
      .HasMaxLength(Username.MaxLength)
      .IsRequired();

    builder.Property(x => x.Email)
      .HasConversion(
        vo => vo.Value,
        v => Email.From(v))
      .HasMaxLength(Email.MaxLength)
      .IsRequired();

    builder.Property(x => x.Attivo)
      .IsRequired();

    builder.Property(u => u.Password)
      .HasConversion(
        pwd => pwd.Value,
        value => HashedPassword.FromHash(value))
      .HasColumnName("Password")
      .HasMaxLength(100)
      .IsRequired();
  }
} 

