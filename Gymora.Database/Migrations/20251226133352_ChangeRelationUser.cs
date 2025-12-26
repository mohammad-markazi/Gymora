using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Gymora.Database.Migrations
{
    /// <inheritdoc />
    public partial class ChangeRelationUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CoachId",
                table: "Users");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CoachId",
                table: "Users",
                type: "int",
                nullable: true);
        }
    }
}
