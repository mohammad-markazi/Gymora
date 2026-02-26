using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Gymora.Database.Migrations
{
    /// <inheritdoc />
    public partial class AddTemplatePlan : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TemplateModels",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: true),
                    Description = table.Column<string>(type: "nvarchar(1500)", maxLength: 1500, nullable: true),
                    CreateCoachId = table.Column<int>(type: "int", nullable: false),
                    Status = table.Column<byte>(type: "tinyint", nullable: false),
                    CreateDateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedDateTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CoachId = table.Column<int>(type: "int", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    UsedCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TemplateModels", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TemplateModels_Coaches_CoachId",
                        column: x => x.CoachId,
                        principalTable: "Coaches",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "TemplateDetailModels",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TemplateId = table.Column<int>(type: "int", nullable: false),
                    Number = table.Column<byte>(type: "tinyint", nullable: false),
                    Complete = table.Column<bool>(type: "bit", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TemplateDetailModels", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TemplateDetailModels_TemplateModels_TemplateId",
                        column: x => x.TemplateId,
                        principalTable: "TemplateModels",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TemplateMovementModels",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TempDetailId = table.Column<int>(type: "int", nullable: false),
                    MovementId = table.Column<int>(type: "int", nullable: false),
                    Pattern = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    ParentId = table.Column<int>(type: "int", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TemplateMovementModels", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TemplateMovementModels_MovementModels_MovementId",
                        column: x => x.MovementId,
                        principalTable: "MovementModels",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TemplateMovementModels_TemplateDetailModels_TempDetailId",
                        column: x => x.TempDetailId,
                        principalTable: "TemplateDetailModels",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TemplateMovementModels_TemplateMovementModels_ParentId",
                        column: x => x.ParentId,
                        principalTable: "TemplateMovementModels",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_TemplateDetailModels_TemplateId",
                table: "TemplateDetailModels",
                column: "TemplateId");

            migrationBuilder.CreateIndex(
                name: "IX_TemplateModels_CoachId",
                table: "TemplateModels",
                column: "CoachId");

            migrationBuilder.CreateIndex(
                name: "IX_TemplateMovementModels_MovementId",
                table: "TemplateMovementModels",
                column: "MovementId");

            migrationBuilder.CreateIndex(
                name: "IX_TemplateMovementModels_ParentId",
                table: "TemplateMovementModels",
                column: "ParentId");

            migrationBuilder.CreateIndex(
                name: "IX_TemplateMovementModels_TempDetailId",
                table: "TemplateMovementModels",
                column: "TempDetailId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TemplateMovementModels");

            migrationBuilder.DropTable(
                name: "TemplateDetailModels");

            migrationBuilder.DropTable(
                name: "TemplateModels");
        }
    }
}
