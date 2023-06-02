using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Eindwerk__Gegevensbeheer__en_C_sharp.Migrations
{
    /// <inheritdoc />
    public partial class addedencryptedKlantentable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Iv",
                table: "Klanten",
                newName: "IVTelefoonNummer");

            migrationBuilder.AddColumn<byte[]>(
                name: "IVEmail",
                table: "Klanten",
                type: "BLOB",
                nullable: false,
                defaultValue: new byte[0]);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IVEmail",
                table: "Klanten");

            migrationBuilder.RenameColumn(
                name: "IVTelefoonNummer",
                table: "Klanten",
                newName: "Iv");
        }
    }
}
