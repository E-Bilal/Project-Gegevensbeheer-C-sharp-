using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Eindwerk__Gegevensbeheer__en_C_sharp.Migrations
{
    /// <inheritdoc />
    public partial class addedreparatietable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Reparaties",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Raming = table.Column<int>(type: "INTEGER", nullable: false),
                    Soort = table.Column<string>(type: "TEXT", nullable: false),
                    Datum = table.Column<DateTime>(type: "TEXT", nullable: false),
                    MecanicienId = table.Column<int>(type: "INTEGER", nullable: true),
                    KlantId = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reparaties", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Reparaties_Klanten_KlantId",
                        column: x => x.KlantId,
                        principalTable: "Klanten",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Reparaties_Mecaniciens_MecanicienId",
                        column: x => x.MecanicienId,
                        principalTable: "Mecaniciens",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Reparaties_KlantId",
                table: "Reparaties",
                column: "KlantId");

            migrationBuilder.CreateIndex(
                name: "IX_Reparaties_MecanicienId",
                table: "Reparaties",
                column: "MecanicienId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Reparaties");
        }
    }
}
