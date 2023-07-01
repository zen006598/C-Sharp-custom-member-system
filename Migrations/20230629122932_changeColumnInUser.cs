using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TimePunchClock.Migrations
{
    /// <inheritdoc />
    public partial class changeColumnInUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "HashPassword",
                table: "User");

            migrationBuilder.RenameColumn(
                name: "salt",
                table: "User",
                newName: "Salt");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Salt",
                table: "User",
                newName: "salt");

            migrationBuilder.AddColumn<string>(
                name: "HashPassword",
                table: "User",
                type: "text",
                nullable: false,
                defaultValue: "");
        }
    }
}
