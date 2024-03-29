﻿// <auto-generated />
using System;
using FlowHouse.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace FlowHouse.Migrations
{
    [DbContext(typeof(SkladContext))]
    [Migration("20210929162743_ComplexDataModel")]
    partial class ComplexDataModel
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.10")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("FlowHouse.Models.Nakup", b =>
                {
                    b.Property<int>("NakupID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("Cena")
                        .HasColumnType("int");

                    b.Property<int>("PolozkaID")
                        .HasColumnType("int");

                    b.Property<int>("ProdavacID")
                        .HasColumnType("int");

                    b.Property<int>("ZakaznikID")
                        .HasColumnType("int");

                    b.HasKey("NakupID");

                    b.HasIndex("PolozkaID");

                    b.HasIndex("ProdavacID");

                    b.HasIndex("ZakaznikID");

                    b.ToTable("Nakup");
                });

            modelBuilder.Entity("FlowHouse.Models.Oddeleni", b =>
                {
                    b.Property<int>("OddeleniID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<decimal>("Budget")
                        .HasColumnType("money");

                    b.Property<string>("Jmeno")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<int?>("ProdavacID")
                        .HasColumnType("int");

                    b.Property<DateTime>("StartDate")
                        .HasColumnType("datetime2");

                    b.HasKey("OddeleniID");

                    b.HasIndex("ProdavacID")
                        .IsUnique()
                        .HasFilter("[ProdavacID] IS NOT NULL");

                    b.ToTable("Oddělení");
                });

            modelBuilder.Entity("FlowHouse.Models.Pobocka", b =>
                {
                    b.Property<int>("ProdavacID")
                        .HasColumnType("int");

                    b.Property<string>("Location")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<int>("PobockaID")
                        .HasColumnType("int");

                    b.HasKey("ProdavacID");

                    b.ToTable("Pobocka");
                });

            modelBuilder.Entity("FlowHouse.Models.Polozka", b =>
                {
                    b.Property<int>("PolozkaID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("Cena")
                        .HasColumnType("int");

                    b.Property<DateTime>("DatumNaskladneni")
                        .HasColumnType("datetime2");

                    b.Property<string>("Nazev")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<int?>("OddeleniID")
                        .HasColumnType("int");

                    b.Property<int>("Pocet")
                        .HasColumnType("int");

                    b.HasKey("PolozkaID");

                    b.HasIndex("OddeleniID");

                    b.ToTable("Polozka");
                });

            modelBuilder.Entity("FlowHouse.Models.Prodavac", b =>
                {
                    b.Property<int>("ProdavacID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("HireDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Jmeno")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)")
                        .HasColumnName("Jméno");

                    b.Property<string>("Prijmeni")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("ProdavacID");

                    b.ToTable("Prodavac");
                });

            modelBuilder.Entity("FlowHouse.Models.ProdejZadani", b =>
                {
                    b.Property<int>("PolozkaID")
                        .HasColumnType("int");

                    b.Property<int>("ProdavacID")
                        .HasColumnType("int");

                    b.HasKey("PolozkaID", "ProdavacID");

                    b.HasIndex("ProdavacID");

                    b.ToTable("ProdejZadani");
                });

            modelBuilder.Entity("FlowHouse.Models.Zakaznik", b =>
                {
                    b.Property<int>("ZakaznikID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("DatumRegistrace")
                        .HasColumnType("datetime2");

                    b.Property<string>("Jmeno")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("Prijmeni")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("ZakaznikID");

                    b.ToTable("Zakaznik");
                });

            modelBuilder.Entity("FlowHouse.Models.Nakup", b =>
                {
                    b.HasOne("FlowHouse.Models.Polozka", "Polozka")
                        .WithMany("Nakupy")
                        .HasForeignKey("PolozkaID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("FlowHouse.Models.Prodavac", "Prodavac")
                        .WithMany()
                        .HasForeignKey("ProdavacID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("FlowHouse.Models.Zakaznik", "Zakaznik")
                        .WithMany("Nakupy")
                        .HasForeignKey("ZakaznikID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Polozka");

                    b.Navigation("Prodavac");

                    b.Navigation("Zakaznik");
                });

            modelBuilder.Entity("FlowHouse.Models.Oddeleni", b =>
                {
                    b.HasOne("FlowHouse.Models.Prodavac", "Administrator")
                        .WithOne("Oddeleni")
                        .HasForeignKey("FlowHouse.Models.Oddeleni", "ProdavacID");

                    b.Navigation("Administrator");
                });

            modelBuilder.Entity("FlowHouse.Models.Pobocka", b =>
                {
                    b.HasOne("FlowHouse.Models.Prodavac", "Prodavac")
                        .WithOne("Pobocka")
                        .HasForeignKey("FlowHouse.Models.Pobocka", "ProdavacID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Prodavac");
                });

            modelBuilder.Entity("FlowHouse.Models.Polozka", b =>
                {
                    b.HasOne("FlowHouse.Models.Oddeleni", "Oddeleni")
                        .WithMany("Polozky")
                        .HasForeignKey("OddeleniID");

                    b.Navigation("Oddeleni");
                });

            modelBuilder.Entity("FlowHouse.Models.ProdejZadani", b =>
                {
                    b.HasOne("FlowHouse.Models.Polozka", "Polozka")
                        .WithMany("ProdejZadanis")
                        .HasForeignKey("PolozkaID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("FlowHouse.Models.Prodavac", "Prodavac")
                        .WithMany("ProdejZadani")
                        .HasForeignKey("ProdavacID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Polozka");

                    b.Navigation("Prodavac");
                });

            modelBuilder.Entity("FlowHouse.Models.Oddeleni", b =>
                {
                    b.Navigation("Polozky");
                });

            modelBuilder.Entity("FlowHouse.Models.Polozka", b =>
                {
                    b.Navigation("Nakupy");

                    b.Navigation("ProdejZadanis");
                });

            modelBuilder.Entity("FlowHouse.Models.Prodavac", b =>
                {
                    b.Navigation("Oddeleni");

                    b.Navigation("Pobocka");

                    b.Navigation("ProdejZadani");
                });

            modelBuilder.Entity("FlowHouse.Models.Zakaznik", b =>
                {
                    b.Navigation("Nakupy");
                });
#pragma warning restore 612, 618
        }
    }
}
