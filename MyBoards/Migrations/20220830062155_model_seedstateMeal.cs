using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MyCaloriesBoards.Migrations
{
    public partial class model_seedstateMeal : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "StateMeals",
                columns: new[] { "Id", "Value" },
                values: new object[] { 1, "To Do" });

            migrationBuilder.InsertData(
                table: "StateMeals",
                columns: new[] { "Id", "Value" },
                values: new object[] { 2, "Eating" });

            migrationBuilder.InsertData(
                table: "StateMeals",
                columns: new[] { "Id", "Value" },
                values: new object[] { 3, "Done" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "StateMeals",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "StateMeals",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "StateMeals",
                keyColumn: "Id",
                keyValue: 3);
        }
    }
}
