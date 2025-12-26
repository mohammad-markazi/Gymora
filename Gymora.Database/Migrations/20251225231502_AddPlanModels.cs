using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Gymora.Database.Migrations
{
    /// <inheritdoc />
    public partial class AddPlanModels : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "PlanModels",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PhoneNumber = table.Column<string>(type: "nvarchar(11)", maxLength: 11, nullable: true),
                    FullName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Files = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: true),
                    Weight = table.Column<byte>(type: "tinyint", nullable: false),
                    Number = table.Column<byte>(type: "tinyint", nullable: false),
                    WeakMuscle_Value = table.Column<short>(type: "smallint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PlanModels", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PlanDetailModels",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PlanId = table.Column<int>(type: "int", nullable: false),
                    Number = table.Column<byte>(type: "tinyint", nullable: false),
                    Complete = table.Column<bool>(type: "bit", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PlanDetailModels", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PlanDetailModels_PlanModels_PlanId",
                        column: x => x.PlanId,
                        principalTable: "PlanModels",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PlanQuestionModels",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PlanId = table.Column<int>(type: "int", nullable: false),
                    Answer = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    QuestionId = table.Column<int>(type: "int", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PlanQuestionModels", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PlanQuestionModels_PlanModels_PlanId",
                        column: x => x.PlanId,
                        principalTable: "PlanModels",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PlanQuestionModels_QuestionModels_QuestionId",
                        column: x => x.QuestionId,
                        principalTable: "QuestionModels",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PlanMovementModels",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PlanDetailId = table.Column<int>(type: "int", nullable: false),
                    MovementId = table.Column<int>(type: "int", nullable: false),
                    Set = table.Column<byte>(type: "tinyint", nullable: false),
                    Count = table.Column<byte>(type: "tinyint", nullable: false),
                    Code = table.Column<int>(type: "int", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PlanMovementModels", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PlanMovementModels_MovementModels_MovementId",
                        column: x => x.MovementId,
                        principalTable: "MovementModels",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PlanMovementModels_PlanDetailModels_PlanDetailId",
                        column: x => x.PlanDetailId,
                        principalTable: "PlanDetailModels",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PlanDetailModels_PlanId",
                table: "PlanDetailModels",
                column: "PlanId");

            migrationBuilder.CreateIndex(
                name: "IX_PlanMovementModels_MovementId",
                table: "PlanMovementModels",
                column: "MovementId");

            migrationBuilder.CreateIndex(
                name: "IX_PlanMovementModels_PlanDetailId",
                table: "PlanMovementModels",
                column: "PlanDetailId");

            migrationBuilder.CreateIndex(
                name: "IX_PlanQuestionModels_PlanId",
                table: "PlanQuestionModels",
                column: "PlanId");

            migrationBuilder.CreateIndex(
                name: "IX_PlanQuestionModels_QuestionId",
                table: "PlanQuestionModels",
                column: "QuestionId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PlanMovementModels");

            migrationBuilder.DropTable(
                name: "PlanQuestionModels");

            migrationBuilder.DropTable(
                name: "PlanDetailModels");

            migrationBuilder.DropTable(
                name: "PlanModels");
        }
    }
}
