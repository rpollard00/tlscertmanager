﻿// <auto-generated />
using System;
using Adapter.Api.SQLite.DataAccess;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Adapter.SQLite.Migrations
{
    [DbContext(typeof(CertDbContext))]
    [Migration("20240423223000_FixConstraints")]
    partial class FixConstraints
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "8.0.4");

            modelBuilder.Entity("CertificateSubjectAlternateName", b =>
                {
                    b.Property<long>("CertificatesId")
                        .HasColumnType("INTEGER");

                    b.Property<long>("SubjectAlternateNamesId")
                        .HasColumnType("INTEGER");

                    b.HasKey("CertificatesId", "SubjectAlternateNamesId");

                    b.HasIndex("SubjectAlternateNamesId");

                    b.ToTable("CertificateSubjectAlternateName");
                });

            modelBuilder.Entity("CertificateSystemNode", b =>
                {
                    b.Property<long>("CertificatesId")
                        .HasColumnType("INTEGER");

                    b.Property<long>("SystemNodeId")
                        .HasColumnType("INTEGER");

                    b.HasKey("CertificatesId", "SystemNodeId");

                    b.HasIndex("SystemNodeId");

                    b.ToTable("CertificateSystemNode");
                });

            modelBuilder.Entity("Core.Models.Certificate", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<long?>("CryptoAlgorithmId")
                        .HasColumnType("INTEGER");

                    b.Property<long>("ExpirationDate")
                        .HasColumnType("INTEGER");

                    b.Property<long>("IssueDate")
                        .HasColumnType("INTEGER");

                    b.Property<long?>("IssuerId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("SubjectName")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("CryptoAlgorithmId");

                    b.HasIndex("IssuerId");

                    b.ToTable("Certificates");
                });

            modelBuilder.Entity("Core.Models.CryptoAlgorithm", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("CryptoAlgorithms");
                });

            modelBuilder.Entity("Core.Models.Issuer", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Name")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Issuers");
                });

            modelBuilder.Entity("Core.Models.SubjectAlternateName", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Name")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("SubjectAlternateNames");
                });

            modelBuilder.Entity("Core.Models.SystemNode", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Name")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("SystemNodes");
                });

            modelBuilder.Entity("CertificateSubjectAlternateName", b =>
                {
                    b.HasOne("Core.Models.Certificate", null)
                        .WithMany()
                        .HasForeignKey("CertificatesId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Core.Models.SubjectAlternateName", null)
                        .WithMany()
                        .HasForeignKey("SubjectAlternateNamesId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("CertificateSystemNode", b =>
                {
                    b.HasOne("Core.Models.Certificate", null)
                        .WithMany()
                        .HasForeignKey("CertificatesId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Core.Models.SystemNode", null)
                        .WithMany()
                        .HasForeignKey("SystemNodeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Core.Models.Certificate", b =>
                {
                    b.HasOne("Core.Models.CryptoAlgorithm", "CryptoAlgorithm")
                        .WithMany("Certificates")
                        .HasForeignKey("CryptoAlgorithmId");

                    b.HasOne("Core.Models.Issuer", "Issuer")
                        .WithMany("Certificates")
                        .HasForeignKey("IssuerId");

                    b.Navigation("CryptoAlgorithm");

                    b.Navigation("Issuer");
                });

            modelBuilder.Entity("Core.Models.CryptoAlgorithm", b =>
                {
                    b.Navigation("Certificates");
                });

            modelBuilder.Entity("Core.Models.Issuer", b =>
                {
                    b.Navigation("Certificates");
                });
#pragma warning restore 612, 618
        }
    }
}
