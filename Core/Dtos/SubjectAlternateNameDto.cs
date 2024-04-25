using Core.Models;

namespace Core.Dtos;

public class SubjectAlternateNameDto
{
    public long Id { get; set; }
    public string? Name { get; set; }
    public List<Certificate> Certificates { get; set; } = new();
}
