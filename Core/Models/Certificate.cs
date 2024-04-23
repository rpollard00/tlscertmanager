using System.ComponentModel.DataAnnotations;

namespace Core.Models;

public class Certificate
{
    public long Id { get; set; }
    public string? SubjectName { get; set; }

    public long IssueDate { get; set; }
    public long ExpirationDate { get; set; }

    public CryptoAlgorithm CryptoAlgorithm { get; set; } = new(); // FK
    public Issuer Issuer { get; set; } = new(); // FK

    public List<SubjectAlternateName>? SubjectAlternateNames { get; set; } = new(); // Many-to-many FK
    public List<SystemNode>? SystemNode { get; set; } = new(); // Many-to-many FK

    public bool isExpiring(int threshold)
    {
        return DateTimeOffset.FromUnixTimeMilliseconds(ExpirationDate) <= DateTime.UtcNow.AddDays(threshold);
    }
}

