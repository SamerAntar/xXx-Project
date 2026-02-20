using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Schulprojekt.Migrations
{
    /// <inheritdoc />
    public partial class CreateQuestionRelatedEntities : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "GapFields",
                columns: table => new
                {
                    GapId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    QuestionId = table.Column<int>(type: "int", nullable: false),
                    GapIndex = table.Column<int>(type: "int", nullable: false),
                    InputType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CorrectText = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CaseSensitive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GapFields", x => x.GapId);
                    table.ForeignKey(
                        name: "FK_GapFields_Questions_QuestionId",
                        column: x => x.QuestionId,
                        principalTable: "Questions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "McAnswers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    QuestionId = table.Column<int>(type: "int", nullable: false),
                    OptionText = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Points = table.Column<int>(type: "int", nullable: false),
                    IsCorrect = table.Column<bool>(type: "bit", nullable: false),
                    OptionOrder = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_McAnswers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_McAnswers_Questions_QuestionId",
                        column: x => x.QuestionId,
                        principalTable: "Questions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "themes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_themes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "GapOptions",
                columns: table => new
                {
                    GapOptionId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    GapId = table.Column<int>(type: "int", nullable: false),
                    OptionText = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsCorrect = table.Column<bool>(type: "bit", nullable: false),
                    OptionOrder = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GapOptions", x => x.GapOptionId);
                    table.ForeignKey(
                        name: "FK_GapOptions_GapFields_GapId",
                        column: x => x.GapId,
                        principalTable: "GapFields",
                        principalColumn: "GapId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "QuestionTheme",
                columns: table => new
                {
                    QuestionsId = table.Column<int>(type: "int", nullable: false),
                    ThemesId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_QuestionTheme", x => new { x.QuestionsId, x.ThemesId });
                    table.ForeignKey(
                        name: "FK_QuestionTheme_Questions_QuestionsId",
                        column: x => x.QuestionsId,
                        principalTable: "Questions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_QuestionTheme_themes_ThemesId",
                        column: x => x.ThemesId,
                        principalTable: "themes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_GapFields_QuestionId_GapIndex",
                table: "GapFields",
                columns: new[] { "QuestionId", "GapIndex" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_GapOptions_GapId_OptionOrder",
                table: "GapOptions",
                columns: new[] { "GapId", "OptionOrder" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_McAnswers_QuestionId",
                table: "McAnswers",
                column: "QuestionId");

            migrationBuilder.CreateIndex(
                name: "IX_QuestionTheme_ThemesId",
                table: "QuestionTheme",
                column: "ThemesId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "GapOptions");

            migrationBuilder.DropTable(
                name: "McAnswers");

            migrationBuilder.DropTable(
                name: "QuestionTheme");

            migrationBuilder.DropTable(
                name: "GapFields");

            migrationBuilder.DropTable(
                name: "themes");
        }
    }
}
