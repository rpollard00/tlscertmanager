using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Core.Models;

namespace Adapter.Api.SQLite.DataAccess;

public class CertDbContext : DbContext, ICertDbContext
{
    public DbSet<Certificate> Certificates { get; set; }
    public DbSet<Issuer> Issuers { get; set; }
    public DbSet<SubjectAlternateName> SubjectAlternateNames { get; set; }
    public DbSet<CryptoAlgorithm> CryptoAlgorithms { get; set; }
    public DbSet<SystemNode> SystemNodes { get; set; }

    private string _connectionString;

    public CertDbContext()
    {

    }

    public CertDbContext(string ConnectionString)
    {
        _connectionString = ConnectionString;

    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {

        if (!optionsBuilder.IsConfigured)
        {
            if (_connectionString != null)
            {
                optionsBuilder.UseSqlite(_connectionString);
            }
            else
            {

                base.OnConfiguring(optionsBuilder);

                var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json");
                var config = builder.Build();

                var connectionStr = config.GetSection("ConnectionStrings")["Default"];
                optionsBuilder.UseSqlite(connectionStr);
            }
        }
        optionsBuilder.LogTo(Console.WriteLine);
    }

    public void EnsureDatabaseCreated()
    {
        Database.EnsureCreated();
    }
}
