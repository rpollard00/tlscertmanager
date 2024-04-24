using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace Core.Models;

[Index(nameof(Name), IsUnique = true)]
public class Issuer
{
    public long Id { get; set; }
    public ICollection<Certificate> Certificates { get; } = new List<Certificate>();
    public string? Name { get; set; }
}
