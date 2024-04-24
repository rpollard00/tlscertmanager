using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Adapter.SQLite.Migrations
{
    /// <inheritdoc />
    public partial class UniqueIndex : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Issuers_Name",
                table: "Issuers",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_CryptoAlgorithms_Name",
                table: "CryptoAlgorithms",
                column: "Name",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Issuers_Name",
                table: "Issuers");

            migrationBuilder.DropIndex(
                name: "IX_CryptoAlgorithms_Name",
                table: "CryptoAlgorithms");
        }
    }
}
