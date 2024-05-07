using Core.Dtos;
using Core.Models;

namespace Core.Mappers;

public static class CertificateDtoMapper
{
    public static CertificateDto CertificateToDto(Certificate cert)
    {
        CertificateDto certDto = new()
        {
            Id = cert.Id,
            SubjectName = cert.SubjectName,

            IssueDate = cert.IssueDate,
            ExpirationDate = cert.ExpirationDate,

            Issuer = cert.Issuer.Name ?? "",
            CryptoAlgorithm = cert.CryptoAlgorithm.Name ?? "",

            SubjectAlternateNames = cert.SubjectAlternateNames?.Select(s => s.Name ?? "").ToList() ?? new List<string>(),

            SystemNode = cert.SystemNode?.Select(s => s.Name ?? "").ToList() ?? new List<string>(),

            // TODO: Not hardcode the threshold
            IsExpiring = cert.isExpiring(30),
        };

        return certDto;
    }

    public static Certificate DtoToCertificate(CertificateDto dto)
    {
        Certificate cert = new()
        {
            Id = dto.Id,
            SubjectName = dto.SubjectName,
            
            CryptoAlgorithm = new CryptoAlgorithm() { Name = dto.CryptoAlgorithm },
            Issuer = new Issuer() { Name = dto.Issuer },
            IssueDate = dto.IssueDate,
            ExpirationDate = dto.ExpirationDate,
            SystemNode = dto.SystemNode != null ? 
                new List<SystemNode>(): null,
            SubjectAlternateNames = dto.SubjectAlternateNames != null ?
                new List<SubjectAlternateName>() : null,

        };

        dto.SystemNode?.ForEach(n => cert.SystemNode?.Add(new SystemNode() { Name= n }));
        dto.SubjectAlternateNames?.ForEach(
            s => cert.SubjectAlternateNames?.Add(new SubjectAlternateName() { Name = s }));

        return cert;
    } 
}

// public class CertificateDto
// {
//     public long Id { get; set; }
//     public string? SubjectName { get; set; }
//
//     public long IssueDate { get; set; }
//     public long ExpirationDate { get; set; }
//
//     public required string CryptoAlgorithm { get; set; }
//     public required string Issuer { get; set; }
//
//     public List<string> SubjectAlternateNames { get; set; } = new();
//     public List<string> SystemNode { get; set; } = new();
//
//     public bool isExpiring { get; set; }
//
// }