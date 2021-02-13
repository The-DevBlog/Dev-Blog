using Microsoft.EntityFrameworkCore.Migrations;

namespace BlazorServer.Migrations
{
    public partial class changeColumnNames : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Comment_Post_PostModelId",
                table: "Comment");

            migrationBuilder.DropForeignKey(
                name: "FK_Vote_Post_PostId",
                table: "Vote");

            migrationBuilder.DropColumn(
                name: "PostId",
                table: "Comment");

            migrationBuilder.RenameColumn(
                name: "PostId",
                table: "Vote",
                newName: "PostModelId");

            migrationBuilder.AlterColumn<int>(
                name: "PostModelId",
                table: "Comment",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Comment_Post_PostModelId",
                table: "Comment",
                column: "PostModelId",
                principalTable: "Post",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Vote_Post_PostModelId",
                table: "Vote",
                column: "PostModelId",
                principalTable: "Post",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Comment_Post_PostModelId",
                table: "Comment");

            migrationBuilder.DropForeignKey(
                name: "FK_Vote_Post_PostModelId",
                table: "Vote");

            migrationBuilder.RenameColumn(
                name: "PostModelId",
                table: "Vote",
                newName: "PostId");

            migrationBuilder.AlterColumn<int>(
                name: "PostModelId",
                table: "Comment",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<int>(
                name: "PostId",
                table: "Comment",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddForeignKey(
                name: "FK_Comment_Post_PostModelId",
                table: "Comment",
                column: "PostModelId",
                principalTable: "Post",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Vote_Post_PostId",
                table: "Vote",
                column: "PostId",
                principalTable: "Post",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
