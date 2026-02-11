using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Gymora.Database.Migrations
{
    /// <inheritdoc />
    public partial class ChangeFieldMovementPlan : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Code",
                table: "PlanMovementModels");

            migrationBuilder.DropColumn(
                name: "Count",
                table: "PlanMovementModels");

            migrationBuilder.DropColumn(
                name: "Set",
                table: "PlanMovementModels");

            migrationBuilder.AddColumn<int>(
                name: "ParentId",
                table: "PlanMovementModels",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Pattern",
                table: "PlanMovementModels",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_PlanMovementModels_ParentId",
                table: "PlanMovementModels",
                column: "ParentId");

            migrationBuilder.AddForeignKey(
                name: "FK_PlanMovementModels_PlanMovementModels_ParentId",
                table: "PlanMovementModels",
                column: "ParentId",
                principalTable: "PlanMovementModels",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PlanMovementModels_PlanMovementModels_ParentId",
                table: "PlanMovementModels");

            migrationBuilder.DropIndex(
                name: "IX_PlanMovementModels_ParentId",
                table: "PlanMovementModels");

            migrationBuilder.DropColumn(
                name: "ParentId",
                table: "PlanMovementModels");

            migrationBuilder.DropColumn(
                name: "Pattern",
                table: "PlanMovementModels");

            migrationBuilder.AddColumn<int>(
                name: "Code",
                table: "PlanMovementModels",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<byte>(
                name: "Count",
                table: "PlanMovementModels",
                type: "tinyint",
                nullable: false,
                defaultValue: (byte)0);

            migrationBuilder.AddColumn<byte>(
                name: "Set",
                table: "PlanMovementModels",
                type: "tinyint",
                nullable: false,
                defaultValue: (byte)0);
        }
    }
}
