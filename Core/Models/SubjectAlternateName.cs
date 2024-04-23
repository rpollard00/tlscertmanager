namespace Core.Models;

public class SubjectAlternateName
{
    public long Id { get; set; }
    public string? Name { get; set; }
    public List<Certificate> Certificates { get; set; } = new();
}
