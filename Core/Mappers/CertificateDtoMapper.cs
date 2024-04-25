using Core.Models;
using Core.Dtos;

namespace Core.Mapper;

public static class CertificateDtoMapper
{
    public static CertificateDto? CertificateToDto(Certificate cert)
    {

        CertificateDto certDto = new()
        {
            Id = cert.Id,
            SubjectName = cert.SubjectName,

            IssueDate = cert.IssueDate,
            ExpirationDate = cert.ExpirationDate,

            Issuer = cert.Issuer.Name ?? "",
            CryptoAlgorithm = cert.CryptoAlgorithm.Name ?? "",

            SubjectAlternateNames = cert.SubjectAlternateNames
                                    != null ? cert.SubjectAlternateNames
                                    .Select(s => s.Name ?? "")
                                    .ToList() : null,

            SystemNode = cert.SystemNode
                         != null ? cert.SystemNode
                         .Select(s => s.Name ?? "")
                         .ToList() : null,

            // TODO: Not hardcode the threshold
            isExpiring = cert.isExpiring(30),

        };

        return certDto;
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
