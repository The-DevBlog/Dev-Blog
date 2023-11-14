using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace devblog.Migrations
{
    /// <inheritdoc />
    public partial class changenotificationtablecasing : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Username",
                table: "Notification",
                newName: "UserName");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "UserName",
                table: "Notification",
                newName: "Username");
        }
    }
}
