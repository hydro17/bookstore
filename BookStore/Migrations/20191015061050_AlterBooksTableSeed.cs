using Microsoft.EntityFrameworkCore.Migrations;

namespace BookStore.Migrations
{
    public partial class AlterBooksTableSeed : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Books",
                columns: new[] { "Id", "Author", "Description", "PhotoUniqueName", "Price", "Title" },
                values: new object[,]
                {
                    { 2, "Stanisław Lem", null, "lem-kongres-futurologiczny.jpg", 18.89m, "Kongres futurologiczny" },
                    { 3, "Carl Gustav Jung", null, "czerwona-ksiega.jpg", 87.99m, "Czerwona księga" },
                    { 4, "Richard King", null, "dywizjon-303.jpg", 25.69m, "Dywizjon 303. Walka i codzienność" },
                    { 5, "Wojciech Drewniak", null, "historia-bez-cenzury.jpg", 28.99m, "Historia bez cenzury 4" }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 5);
        }
    }
}
