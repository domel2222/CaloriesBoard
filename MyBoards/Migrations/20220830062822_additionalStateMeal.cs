using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MyCaloriesBoards.Migrations
{
    public partial class additionalStateMeal : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                "StateMeals",
                "Value",
                "On Hold"
                );

            migrationBuilder.InsertData(
                "StateMeals",
                "Value",
                "Vomited"
                );
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                "StateMeals",
                "Value",
                "On Hold"
                );

            migrationBuilder.DeleteData(
                "StateMeals",
                "Value",
                "Vomited"
                );
        }
    }
}
