using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Biblioteka.Migrations
{
    /// <inheritdoc />
    public partial class RenameCopiesAvailableToDostępneKopie : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Title",
                table: "Books",
                newName: "Tytuł");

            migrationBuilder.RenameColumn(
                name: "CopiesAvailable",
                table: "Books",
                newName: "DostępneKopie");

            migrationBuilder.RenameColumn(
                name: "Author",
                table: "Books",
                newName: "Autor");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Tytuł",
                table: "Books",
                newName: "Title");

            migrationBuilder.RenameColumn(
                name: "DostępneKopie",
                table: "Books",
                newName: "CopiesAvailable");

            migrationBuilder.RenameColumn(
                name: "Autor",
                table: "Books",
                newName: "Author");
        }
    }
}
