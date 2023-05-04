using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace devblog.Migrations
{
    /// <inheritdoc />
    public partial class removePostUpdateNumber : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UpdateNum",
                table: "Post");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "UpdateNum",
                table: "Post",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");
        }
    }
}
