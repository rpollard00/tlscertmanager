using Core.Models;

namespace Core.Dtos;

public class IssuerDto
{
    public long Id { get; set; }
    public ICollection<Certificate> Certificates { get; } = new List<Certificate>();
    public string? Name { get; set; }
}
