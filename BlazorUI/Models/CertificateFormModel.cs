namespace BlazorUI.Models;

public class CertificateFormModel
{
    public string? SubjectName { get; set; }

    public DateTime IssueDate { get; set; }
    public DateTime ExpirationDate { get; set; }

    public string? CryptoAlgorithm { get; set; }
    public string? Issuer { get; set; }

    public List<string>? SubjectAlternateNames { get; set; } = new();

    public List<string>? SystemNode { get; set; } = new();
}
