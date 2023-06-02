using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Eindwerk__Gegevensbeheer__en_C_sharp.Migrations
{
    /// <inheritdoc />
    public partial class AddedBestellingtable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Bestellingen",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Aantal = table.Column<int>(type: "INTEGER", nullable: false),
                    TotaalPrijs = table.Column<float>(type: "REAL", nullable: false),
                    KlantId = table.Column<int>(type: "INTEGER", nullable: true),
                    OnderdeelId = table.Column<int>(type: "INTEGER", nullable: true),
                    AutoId = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Bestellingen", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Bestellingen_Autos_AutoId",
                        column: x => x.AutoId,
                        principalTable: "Autos",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Bestellingen_Klanten_KlantId",
                        column: x => x.KlantId,
                        principalTable: "Klanten",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Bestellingen_Onderdelen_OnderdeelId",
                        column: x => x.OnderdeelId,
                        principalTable: "Onderdelen",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Bestellingen_AutoId",
                table: "Bestellingen",
                column: "AutoId");

            migrationBuilder.CreateIndex(
                name: "IX_Bestellingen_KlantId",
                table: "Bestellingen",
                column: "KlantId");

            migrationBuilder.CreateIndex(
                name: "IX_Bestellingen_OnderdeelId",
                table: "Bestellingen",
                column: "OnderdeelId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Bestellingen");
        }
    }
}
