namespace Core.Models;

public class CertificateModel
{
    public ulong Id { get; set; }
    public string? SubjectName { get; set; }
    public string? IssuerName { get; set; }
    public long IssueDate { get; set; }
    public long ExpirationDate { get; set; }
    public List<SubjectAlternateNames> SubjectAlternateNames { get; set; } = new();
    public string? SignatureAlgorithmId { get; set; }

    public bool isExpiring(int threshold)
    {
        return DateTimeOffset.FromUnixTimeMilliseconds(ExpirationDate) <= DateTime.UtcNow.AddDays(threshold);
    }
}

