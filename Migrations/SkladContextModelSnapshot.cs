﻿// <auto-generated />
using System;
using FlowHouse.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace FlowHouse.Migrations
{
    [DbContext(typeof(SkladContext))]
    partial class SkladContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
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

                    b.Property<int>("ZakaznikID")
                        .HasColumnType("int");

                    b.HasKey("NakupID");

                    b.HasIndex("PolozkaID");

                    b.HasIndex("ZakaznikID");

                    b.ToTable("Nakup");
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
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Pocet")
                        .HasColumnType("int");

                    b.HasKey("PolozkaID");

                    b.ToTable("Polozka");
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
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Prijmeni")
                        .HasColumnType("nvarchar(max)");

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

                    b.HasOne("FlowHouse.Models.Zakaznik", "Zakaznik")
                        .WithMany("Nakupy")
                        .HasForeignKey("ZakaznikID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Polozka");

                    b.Navigation("Zakaznik");
                });

            modelBuilder.Entity("FlowHouse.Models.Polozka", b =>
                {
                    b.Navigation("Nakupy");
                });

            modelBuilder.Entity("FlowHouse.Models.Zakaznik", b =>
                {
                    b.Navigation("Nakupy");
                });
#pragma warning restore 612, 618
        }
    }
}