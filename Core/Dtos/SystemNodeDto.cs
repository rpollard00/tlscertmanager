using Core.Models;

namespace Core.Dtos;

public class SystemNodeDto
{
    public long Id { get; set; }
    public string? Name { get; set; }
    public List<Certificate> Certificates { get; set; } = new();
}
