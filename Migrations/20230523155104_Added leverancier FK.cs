using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Eindwerk__Gegevensbeheer__en_C_sharp.Migrations
{
    /// <inheritdoc />
    public partial class AddedleverancierFK : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "LeverancierId",
                table: "Bestellingen",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Bestellingen_LeverancierId",
                table: "Bestellingen",
                column: "LeverancierId");

            migrationBuilder.AddForeignKey(
                name: "FK_Bestellingen_Leveranciers_LeverancierId",
                table: "Bestellingen",
                column: "LeverancierId",
                principalTable: "Leveranciers",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Bestellingen_Leveranciers_LeverancierId",
                table: "Bestellingen");

            migrationBuilder.DropIndex(
                name: "IX_Bestellingen_LeverancierId",
                table: "Bestellingen");

            migrationBuilder.DropColumn(
                name: "LeverancierId",
                table: "Bestellingen");
        }
    }
}
