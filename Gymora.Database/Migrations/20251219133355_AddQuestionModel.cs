using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Gymora.Database.Migrations
{
    /// <inheritdoc />
    public partial class AddQuestionModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "QuestionModels",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Body = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    CreateCoachId = table.Column<int>(type: "int", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    MovementModelId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_QuestionModels", x => x.Id);
                    table.ForeignKey(
                        name: "FK_QuestionModels_Coaches_CreateCoachId",
                        column: x => x.CreateCoachId,
                        principalTable: "Coaches",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_QuestionModels_MovementModels_MovementModelId",
                        column: x => x.MovementModelId,
                        principalTable: "MovementModels",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_QuestionModels_CreateCoachId",
                table: "QuestionModels",
                column: "CreateCoachId");

            migrationBuilder.CreateIndex(
                name: "IX_QuestionModels_MovementModelId",
                table: "QuestionModels",
                column: "MovementModelId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "QuestionModels");
        }
    }
}
