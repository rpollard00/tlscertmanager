using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Adapter.SQLite.Migrations
{
    /// <inheritdoc />
    public partial class CreateDatabase : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CryptoAlgorithms",
                columns: table => new
                {
                    Id = table.Column<long>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CryptoAlgorithms", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Issuers",
                columns: table => new
                {
                    Id = table.Column<long>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Issuers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SubjectAlternateNames",
                columns: table => new
                {
                    Id = table.Column<long>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SubjectAlternateNames", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SystemNodes",
                columns: table => new
                {
                    Id = table.Column<long>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SystemNodes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Certificates",
                columns: table => new
                {
                    Id = table.Column<long>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    SubjectName = table.Column<string>(type: "TEXT", nullable: true),
                    IssueDate = table.Column<long>(type: "INTEGER", nullable: false),
                    ExpirationDate = table.Column<long>(type: "INTEGER", nullable: false),
                    CryptoAlgorithmId = table.Column<long>(type: "INTEGER", nullable: false),
                    IssuerId = table.Column<long>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Certificates", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Certificates_CryptoAlgorithms_CryptoAlgorithmId",
                        column: x => x.CryptoAlgorithmId,
                        principalTable: "CryptoAlgorithms",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Certificates_Issuers_IssuerId",
                        column: x => x.IssuerId,
                        principalTable: "Issuers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CertificateSubjectAlternateName",
                columns: table => new
                {
                    CertificatesId = table.Column<long>(type: "INTEGER", nullable: false),
                    SubjectAlternateNamesId = table.Column<long>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CertificateSubjectAlternateName", x => new { x.CertificatesId, x.SubjectAlternateNamesId });
                    table.ForeignKey(
                        name: "FK_CertificateSubjectAlternateName_Certificates_CertificatesId",
                        column: x => x.CertificatesId,
                        principalTable: "Certificates",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CertificateSubjectAlternateName_SubjectAlternateNames_SubjectAlternateNamesId",
                        column: x => x.SubjectAlternateNamesId,
                        principalTable: "SubjectAlternateNames",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CertificateSystemNode",
                columns: table => new
                {
                    CertificatesId = table.Column<long>(type: "INTEGER", nullable: false),
                    SystemNodeId = table.Column<long>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CertificateSystemNode", x => new { x.CertificatesId, x.SystemNodeId });
                    table.ForeignKey(
                        name: "FK_CertificateSystemNode_Certificates_CertificatesId",
                        column: x => x.CertificatesId,
                        principalTable: "Certificates",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CertificateSystemNode_SystemNodes_SystemNodeId",
                        column: x => x.SystemNodeId,
                        principalTable: "SystemNodes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Certificates_CryptoAlgorithmId",
                table: "Certificates",
                column: "CryptoAlgorithmId");

            migrationBuilder.CreateIndex(
                name: "IX_Certificates_IssuerId",
                table: "Certificates",
                column: "IssuerId");

            migrationBuilder.CreateIndex(
                name: "IX_CertificateSubjectAlternateName_SubjectAlternateNamesId",
                table: "CertificateSubjectAlternateName",
                column: "SubjectAlternateNamesId");

            migrationBuilder.CreateIndex(
                name: "IX_CertificateSystemNode_SystemNodeId",
                table: "CertificateSystemNode",
                column: "SystemNodeId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CertificateSubjectAlternateName");

            migrationBuilder.DropTable(
                name: "CertificateSystemNode");

            migrationBuilder.DropTable(
                name: "SubjectAlternateNames");

            migrationBuilder.DropTable(
                name: "Certificates");

            migrationBuilder.DropTable(
                name: "SystemNodes");

            migrationBuilder.DropTable(
                name: "CryptoAlgorithms");

            migrationBuilder.DropTable(
                name: "Issuers");
        }
    }
}
