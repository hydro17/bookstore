using Microsoft.EntityFrameworkCore.Migrations;

namespace BookStore.Migrations
{
    public partial class AddNewBooksToBookTableSeed : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Books",
                columns: new[] { "Id", "Author", "Description", "PhotoUniqueName", "Price", "Title" },
                values: new object[,]
                {
                    { 6, "Jan Parandowski", null, "mitologia.jpg", 32.99m, "Mitologia. Wierzenia i podania Greków i Rzymian" },
                    { 7, "J.R.R. Tolkien", null, "the-two-towers.jpg", 55.59m, "Władca pierścieni, Dwie wieże" },
                    { 8, "Walter Isaacson", null, "lonardo-da-vinci.jpg", 34.85m, "Leonardo da Vinci" },
                    { 9, "J.K. Rowling", null, "harry-potter-kamien-filozoficzny.jpg", 23.99m, "Harry Potter i Kamień Filozoficzny" },
                    { 10, "Childress David Hatcher", null, "olmekowie-najstarsza-tajemnica-meksyku.jpg", 20.99m, "Olmekowie Najstarsza tajemnica Meksyku" }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 10);
        }
    }
}
