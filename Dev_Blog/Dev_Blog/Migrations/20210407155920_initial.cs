using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Dev_Blog.Migrations
{
    public partial class initial : Migration
{
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.CreateTable(
            name: "Post",
            columns: table => new
            {
                Id = table.Column<int>(type: "int", nullable: false)
                    .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                UpdateNum = table.Column<string>(type: "longtext CHARACTER SET utf8mb4", nullable: false),
                Description = table.Column<string>(type: "longtext CHARACTER SET utf8mb4", nullable: false),
                Date = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                ImgURL = table.Column<string>(type: "longtext CHARACTER SET utf8mb4", nullable: true)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_Post", x => x.Id);
            });

        migrationBuilder.CreateTable(
            name: "Comment",
            columns: table => new
            {
                Id = table.Column<int>(type: "int", nullable: false)
                    .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                PostModelId = table.Column<int>(type: "int", nullable: false),
                Content = table.Column<string>(type: "longtext CHARACTER SET utf8mb4", nullable: true),
                Date = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                UserName = table.Column<string>(type: "longtext CHARACTER SET utf8mb4", nullable: true)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_Comment", x => x.Id);
                table.ForeignKey(
                    name: "FK_Comment_Post_PostModelId",
                    column: x => x.PostModelId,
                    principalTable: "Post",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Cascade);
            });

        migrationBuilder.CreateTable(
            name: "DownVote",
            columns: table => new
            {
                PostModelId = table.Column<int>(type: "int", nullable: false),
                UserName = table.Column<string>(type: "varchar(255) CHARACTER SET utf8mb4", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_DownVote", x => new { x.PostModelId, x.UserName });
                table.ForeignKey(
                    name: "FK_DownVote_Post_PostModelId",
                    column: x => x.PostModelId,
                    principalTable: "Post",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Cascade);
            });

        migrationBuilder.CreateTable(
            name: "UpVote",
            columns: table => new
            {
                PostModelId = table.Column<int>(type: "int", nullable: false),
                UserName = table.Column<string>(type: "varchar(255) CHARACTER SET utf8mb4", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_UpVote", x => new { x.PostModelId, x.UserName });
                table.ForeignKey(
                    name: "FK_UpVote_Post_PostModelId",
                    column: x => x.PostModelId,
                    principalTable: "Post",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Cascade);
            });

        migrationBuilder.CreateIndex(
            name: "IX_Comment_PostModelId",
            table: "Comment",
            column: "PostModelId");
    }

    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropTable(
            name: "Comment");

        migrationBuilder.DropTable(
            name: "DownVote");

        migrationBuilder.DropTable(
            name: "UpVote");

        migrationBuilder.DropTable(
            name: "Post");
    }
}
}
