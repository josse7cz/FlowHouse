using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace FlowHouse.Migrations
{
    public partial class ComplexDataModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Prodavac",
                columns: table => new
                {
                    ProdavacID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Prijmeni = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Jméno = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    HireDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Prodavac", x => x.ProdavacID);
                });

            migrationBuilder.CreateTable(
                name: "Zakaznik",
                columns: table => new
                {
                    ZakaznikID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Jmeno = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Prijmeni = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    DatumRegistrace = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Zakaznik", x => x.ZakaznikID);
                });

            migrationBuilder.CreateTable(
                name: "Oddělení",
                columns: table => new
                {
                    OddeleniID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Jmeno = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Budget = table.Column<decimal>(type: "money", nullable: false),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ProdavacID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Oddělení", x => x.OddeleniID);
                    table.ForeignKey(
                        name: "FK_Oddělení_Prodavac_ProdavacID",
                        column: x => x.ProdavacID,
                        principalTable: "Prodavac",
                        principalColumn: "ProdavacID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Pobocka",
                columns: table => new
                {
                    ProdavacID = table.Column<int>(type: "int", nullable: false),
                    PobockaID = table.Column<int>(type: "int", nullable: false),
                    Location = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pobocka", x => x.ProdavacID);
                    table.ForeignKey(
                        name: "FK_Pobocka_Prodavac_ProdavacID",
                        column: x => x.ProdavacID,
                        principalTable: "Prodavac",
                        principalColumn: "ProdavacID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Polozka",
                columns: table => new
                {
                    PolozkaID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nazev = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Cena = table.Column<int>(type: "int", nullable: false),
                    Pocet = table.Column<int>(type: "int", nullable: false),
                    DatumNaskladneni = table.Column<DateTime>(type: "datetime2", nullable: false),
                    OddeleniID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Polozka", x => x.PolozkaID);
                    table.ForeignKey(
                        name: "FK_Polozka_Oddělení_OddeleniID",
                        column: x => x.OddeleniID,
                        principalTable: "Oddělení",
                        principalColumn: "OddeleniID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Nakup",
                columns: table => new
                {
                    NakupID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PolozkaID = table.Column<int>(type: "int", nullable: false),
                    ZakaznikID = table.Column<int>(type: "int", nullable: false),
                    ProdavacID = table.Column<int>(type: "int", nullable: false),
                    Cena = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Nakup", x => x.NakupID);
                    table.ForeignKey(
                        name: "FK_Nakup_Polozka_PolozkaID",
                        column: x => x.PolozkaID,
                        principalTable: "Polozka",
                        principalColumn: "PolozkaID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Nakup_Prodavac_ProdavacID",
                        column: x => x.ProdavacID,
                        principalTable: "Prodavac",
                        principalColumn: "ProdavacID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Nakup_Zakaznik_ZakaznikID",
                        column: x => x.ZakaznikID,
                        principalTable: "Zakaznik",
                        principalColumn: "ZakaznikID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProdejZadani",
                columns: table => new
                {
                    ProdavacID = table.Column<int>(type: "int", nullable: false),
                    PolozkaID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProdejZadani", x => new { x.PolozkaID, x.ProdavacID });
                    table.ForeignKey(
                        name: "FK_ProdejZadani_Polozka_PolozkaID",
                        column: x => x.PolozkaID,
                        principalTable: "Polozka",
                        principalColumn: "PolozkaID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProdejZadani_Prodavac_ProdavacID",
                        column: x => x.ProdavacID,
                        principalTable: "Prodavac",
                        principalColumn: "ProdavacID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Nakup_PolozkaID",
                table: "Nakup",
                column: "PolozkaID");

            migrationBuilder.CreateIndex(
                name: "IX_Nakup_ProdavacID",
                table: "Nakup",
                column: "ProdavacID");

            migrationBuilder.CreateIndex(
                name: "IX_Nakup_ZakaznikID",
                table: "Nakup",
                column: "ZakaznikID");

            migrationBuilder.CreateIndex(
                name: "IX_Oddělení_ProdavacID",
                table: "Oddělení",
                column: "ProdavacID",
                unique: true,
                filter: "[ProdavacID] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Polozka_OddeleniID",
                table: "Polozka",
                column: "OddeleniID");

            migrationBuilder.CreateIndex(
                name: "IX_ProdejZadani_ProdavacID",
                table: "ProdejZadani",
                column: "ProdavacID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Nakup");

            migrationBuilder.DropTable(
                name: "Pobocka");

            migrationBuilder.DropTable(
                name: "ProdejZadani");

            migrationBuilder.DropTable(
                name: "Zakaznik");

            migrationBuilder.DropTable(
                name: "Polozka");

            migrationBuilder.DropTable(
                name: "Oddělení");

            migrationBuilder.DropTable(
                name: "Prodavac");
        }
    }
}
