using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Adapter.SQLite.Migrations
{
    /// <inheritdoc />
    public partial class FixConstraints : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CertificateId",
                table: "Issuers");

            migrationBuilder.DropColumn(
                name: "CertificateId",
                table: "CryptoAlgorithms");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "CertificateId",
                table: "Issuers",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<long>(
                name: "CertificateId",
                table: "CryptoAlgorithms",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0L);
        }
    }
}
