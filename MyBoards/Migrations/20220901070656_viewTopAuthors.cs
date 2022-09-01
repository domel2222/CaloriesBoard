using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MyCaloriesBoards.Migrations
{
    public partial class viewTopAuthors : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
            CREATE VIEW View_TopAuthors AS
            SELECT top 5 u.FullName, Count(*)  AS Meal
            FROM Users u 
            JOIN Meals m ON u.Id = m.AuthorId
            GROUP BY u.Id, u.FullName
            ORDER BY  Meal DESC
            ");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
            DROP VIEW View_TopAuthors 
            ");
        }
    }
}
