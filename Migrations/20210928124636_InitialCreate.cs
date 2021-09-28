using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace FlowHouse.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Polozka",
                columns: table => new
                {
                    PolozkaID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nazev = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Cena = table.Column<int>(type: "int", nullable: false),
                    Pocet = table.Column<int>(type: "int", nullable: false),
                    DatumNaskladneni = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Polozka", x => x.PolozkaID);
                });

            migrationBuilder.CreateTable(
                name: "Zakaznik",
                columns: table => new
                {
                    ZakaznikID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Jmeno = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Prijmeni = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DatumRegistrace = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Zakaznik", x => x.ZakaznikID);
                });

            migrationBuilder.CreateTable(
                name: "Nakup",
                columns: table => new
                {
                    NakupID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PolozkaID = table.Column<int>(type: "int", nullable: false),
                    ZakaznikID = table.Column<int>(type: "int", nullable: false),
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
                        name: "FK_Nakup_Zakaznik_ZakaznikID",
                        column: x => x.ZakaznikID,
                        principalTable: "Zakaznik",
                        principalColumn: "ZakaznikID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Nakup_PolozkaID",
                table: "Nakup",
                column: "PolozkaID");

            migrationBuilder.CreateIndex(
                name: "IX_Nakup_ZakaznikID",
                table: "Nakup",
                column: "ZakaznikID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Nakup");

            migrationBuilder.DropTable(
                name: "Polozka");

            migrationBuilder.DropTable(
                name: "Zakaznik");
        }
    }
}
