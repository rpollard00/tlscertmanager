namespace Core.Dtos;

public class CertificateDto
{
    public long Id { get; set; }
    public string? SubjectName { get; set; }

    public long IssueDate { get; set; }
    public long ExpirationDate { get; set; }

    public required string CryptoAlgorithm { get; set; }
    public required string Issuer { get; set; }

    public List<string>? SubjectAlternateNames { get; set; } = new();
    public List<string>? SystemNode { get; set; } = new();

    public bool isExpiring { get; set; }

}

