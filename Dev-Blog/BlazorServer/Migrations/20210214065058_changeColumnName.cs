using Microsoft.EntityFrameworkCore.Migrations;

namespace BlazorServer.Migrations
{
    public partial class changeColumnName : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "Vote",
                newName: "UserName");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "UserName",
                table: "Vote",
                newName: "UserId");
        }
    }
}
