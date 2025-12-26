using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Gymora.Database.Migrations
{
    /// <inheritdoc />
    public partial class AddFieldStatusAndCreateDateAndmodifiedInPlan : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "CreateDateTime",
                table: "PlanModels",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "ModifiedDateTime",
                table: "PlanModels",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<byte>(
                name: "Status",
                table: "PlanModels",
                type: "tinyint",
                nullable: false,
                defaultValue: (byte)0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreateDateTime",
                table: "PlanModels");

            migrationBuilder.DropColumn(
                name: "ModifiedDateTime",
                table: "PlanModels");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "PlanModels");
        }
    }
}
