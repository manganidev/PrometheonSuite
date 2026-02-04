using System;
using Ardalis.GuardClauses;

namespace PrometheonSuite.Identity.Entities.UtenteAggregate;

/// <summary>
/// Rappresenta una password già hashata. Non memorizza mai password in chiaro.
/// </summary>
public record HashedPassword
{
  private const int MinHashLength = 20; // BCrypt produce hash di ~60 caratteri
  public string Value { get; }

  private HashedPassword(string hashedValue)
  {
    Value = hashedValue;
  }

  /// <summary>
  /// Crea un'istanza da una password già hashata (es. da database)
  /// </summary>
  public static HashedPassword FromHash(string hashedPassword)
  {
    Guard.Against.NullOrWhiteSpace(hashedPassword, nameof(hashedPassword));
    Guard.Against.InvalidInput(hashedPassword, nameof(hashedPassword), 
      h => h.Length >= MinHashLength, 
      "Hash della password non valido");
    
    return new HashedPassword(hashedPassword);
  }

  /// <summary>
  /// Crea un'istanza hashando una password in chiaro
  /// </summary>
  public static HashedPassword FromPlainText(string plainTextPassword)
  {
    Guard.Against.NullOrWhiteSpace(plainTextPassword, nameof(plainTextPassword));
    Guard.Against.InvalidInput(plainTextPassword, nameof(plainTextPassword),
      p => p.Length >= 8,
      "La password deve essere di almeno 8 caratteri");

    // Usa BCrypt con work factor 12 (bilanciamento sicurezza/performance)
    var hashedValue = BCrypt.Net.BCrypt.HashPassword(plainTextPassword, 12);
    return new HashedPassword(hashedValue);
  }

  /// <summary>
  /// Verifica se una password in chiaro corrisponde a questo hash
  /// </summary>
  public bool Verify(string plainTextPassword)
  {
    if (string.IsNullOrWhiteSpace(plainTextPassword))
      return false;

    try
    {
      return BCrypt.Net.BCrypt.Verify(plainTextPassword, Value);
    }
    catch
    {
      return false;
    }
  }
}
