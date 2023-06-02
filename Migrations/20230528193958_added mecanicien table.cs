using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Eindwerk__Gegevensbeheer__en_C_sharp.Migrations
{
    /// <inheritdoc />
    public partial class addedmecanicientable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Bestellingen_Klanten_KlantId",
                table: "Bestellingen");

            migrationBuilder.AlterColumn<int>(
                name: "KlantId",
                table: "Bestellingen",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "INTEGER",
                oldNullable: true);

            migrationBuilder.CreateTable(
                name: "Mecaniciens",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Voornaam = table.Column<string>(type: "TEXT", nullable: false),
                    Achternaam = table.Column<string>(type: "TEXT", nullable: false),
                    TelefoonNummer = table.Column<string>(type: "TEXT", nullable: false),
                    Email = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Mecaniciens", x => x.Id);
                });

            migrationBuilder.AddForeignKey(
                name: "FK_Bestellingen_Klanten_KlantId",
                table: "Bestellingen",
                column: "KlantId",
                principalTable: "Klanten",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Bestellingen_Klanten_KlantId",
                table: "Bestellingen");

            migrationBuilder.DropTable(
                name: "Mecaniciens");

            migrationBuilder.AlterColumn<int>(
                name: "KlantId",
                table: "Bestellingen",
                type: "INTEGER",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "INTEGER");

            migrationBuilder.AddForeignKey(
                name: "FK_Bestellingen_Klanten_KlantId",
                table: "Bestellingen",
                column: "KlantId",
                principalTable: "Klanten",
                principalColumn: "Id");
        }
    }
}
