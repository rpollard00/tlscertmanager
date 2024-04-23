namespace Core.Models;

public class CryptoAlgorithm
{
    public long Id { get; set; }
    public long CertificateId { get; set; }
    public string Name { get; set; } = String.Empty;
}
