﻿using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Adapter.SQLite.Migrations
{
    /// <inheritdoc />
    public partial class UpdateFKs : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Certificates_CryptoAlgorithms_CryptoAlgorithmId",
                table: "Certificates");

            migrationBuilder.DropForeignKey(
                name: "FK_Certificates_Issuers_IssuerId",
                table: "Certificates");

            migrationBuilder.DropIndex(
                name: "IX_Certificates_CryptoAlgorithmId",
                table: "Certificates");

            migrationBuilder.DropIndex(
                name: "IX_Certificates_IssuerId",
                table: "Certificates");

            migrationBuilder.DropColumn(
                name: "CryptoAlgorithmId",
                table: "Certificates");

            migrationBuilder.DropColumn(
                name: "IssuerId",
                table: "Certificates");

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

            migrationBuilder.CreateIndex(
                name: "IX_Issuers_CertificateId",
                table: "Issuers",
                column: "CertificateId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_CryptoAlgorithms_CertificateId",
                table: "CryptoAlgorithms",
                column: "CertificateId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_CryptoAlgorithms_Certificates_CertificateId",
                table: "CryptoAlgorithms",
                column: "CertificateId",
                principalTable: "Certificates",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Issuers_Certificates_CertificateId",
                table: "Issuers",
                column: "CertificateId",
                principalTable: "Certificates",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CryptoAlgorithms_Certificates_CertificateId",
                table: "CryptoAlgorithms");

            migrationBuilder.DropForeignKey(
                name: "FK_Issuers_Certificates_CertificateId",
                table: "Issuers");

            migrationBuilder.DropIndex(
                name: "IX_Issuers_CertificateId",
                table: "Issuers");

            migrationBuilder.DropIndex(
                name: "IX_CryptoAlgorithms_CertificateId",
                table: "CryptoAlgorithms");

            migrationBuilder.DropColumn(
                name: "CertificateId",
                table: "Issuers");

            migrationBuilder.DropColumn(
                name: "CertificateId",
                table: "CryptoAlgorithms");

            migrationBuilder.AddColumn<long>(
                name: "CryptoAlgorithmId",
                table: "Certificates",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<long>(
                name: "IssuerId",
                table: "Certificates",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.CreateIndex(
                name: "IX_Certificates_CryptoAlgorithmId",
                table: "Certificates",
                column: "CryptoAlgorithmId");

            migrationBuilder.CreateIndex(
                name: "IX_Certificates_IssuerId",
                table: "Certificates",
                column: "IssuerId");

            migrationBuilder.AddForeignKey(
                name: "FK_Certificates_CryptoAlgorithms_CryptoAlgorithmId",
                table: "Certificates",
                column: "CryptoAlgorithmId",
                principalTable: "CryptoAlgorithms",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Certificates_Issuers_IssuerId",
                table: "Certificates",
                column: "IssuerId",
                principalTable: "Issuers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
