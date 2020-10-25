using Microsoft.EntityFrameworkCore.Migrations;

namespace Dev_Blog.Migrations.AppDb
{
    public partial class newColumn : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddForeignKey(
                name: "FK_Vote_Post_PostId",
                table: "Vote",
                column: "PostId",
                principalTable: "Post",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Vote_Post_PostId",
                table: "Vote");
        }
    }
}
