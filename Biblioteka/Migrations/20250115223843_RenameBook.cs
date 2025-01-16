using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Biblioteka.Migrations
{
    /// <inheritdoc />
    public partial class RenameBook : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Tytuł",
                table: "Books",
                newName: "Tytul");

            migrationBuilder.RenameColumn(
                name: "DostępneKopie",
                table: "Books",
                newName: "DostepneKopie");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Tytul",
                table: "Books",
                newName: "Tytuł");

            migrationBuilder.RenameColumn(
                name: "DostepneKopie",
                table: "Books",
                newName: "DostępneKopie");
        }
    }
}
