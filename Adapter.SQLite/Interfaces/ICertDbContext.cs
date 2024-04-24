using Core.Models;
using Microsoft.EntityFrameworkCore;

public interface ICertDbContext
{
    public DbSet<Certificate> Certificates { get; set; }
    public DbSet<Issuer> Issuers { get; set; }
    public DbSet<SubjectAlternateName> SubjectAlternateNames { get; set; }
    public DbSet<CryptoAlgorithm> CryptoAlgorithms { get; set; }
    public DbSet<SystemNode> SystemNodes { get; set; }

    int SaveChanges();
}
