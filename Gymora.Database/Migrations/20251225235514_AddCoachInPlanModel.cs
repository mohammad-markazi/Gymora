using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Gymora.Database.Migrations
{
    /// <inheritdoc />
    public partial class AddCoachInPlanModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CreateCoachId",
                table: "PlanModels",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreateCoachId",
                table: "PlanModels");
        }
    }
}
