namespace Core.Models;

public class Issuer
{
    public long Id { get; set; }
    public long CertificateId { get; set; }
    public string? Name { get; set; }
}
