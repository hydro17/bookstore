using Microsoft.EntityFrameworkCore.Migrations;

namespace BookStore.Migrations
{
    public partial class SeedBooksTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Books",
                columns: new[] { "Id", "Author", "Description", "PhotoUniqueName", "Price", "Title" },
                values: new object[] { 1, "Andrzej Sapkowski", null, "3d4cdb83-8fb8-42aa-a05c-fb2f7d5d1e6d_wiedzmin-miecz-przeznaczenia.jpg", 28.99m, "Wiedźmin Miecz przeznaczenia" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 1);
        }
    }
}
