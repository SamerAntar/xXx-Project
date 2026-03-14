using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Schulprojekt.Migrations
{
    /// <inheritdoc />
    public partial class Added_Table_Character_67 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "GamePlaceDescription",
                table: "Themen",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "GamePlaceName",
                table: "Themen",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "CharacterId",
                table: "QuestionSetProgresses",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Character",
                columns: table => new
                {
                    CharacterID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    GettingByCompletingTheme = table.Column<int>(type: "int", nullable: false),
                    Backstory = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NormalEndText = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ProfiEndText = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TopEndText = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Character", x => x.CharacterID);
                });

            migrationBuilder.CreateIndex(
                name: "IX_QuestionSetProgresses_CharacterId",
                table: "QuestionSetProgresses",
                column: "CharacterId");

            migrationBuilder.AddForeignKey(
                name: "FK_QuestionSetProgresses_Character_CharacterId",
                table: "QuestionSetProgresses",
                column: "CharacterId",
                principalTable: "Character",
                principalColumn: "CharacterID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_QuestionSetProgresses_Character_CharacterId",
                table: "QuestionSetProgresses");

            migrationBuilder.DropTable(
                name: "Character");

            migrationBuilder.DropIndex(
                name: "IX_QuestionSetProgresses_CharacterId",
                table: "QuestionSetProgresses");

            migrationBuilder.DropColumn(
                name: "GamePlaceDescription",
                table: "Themen");

            migrationBuilder.DropColumn(
                name: "GamePlaceName",
                table: "Themen");

            migrationBuilder.DropColumn(
                name: "CharacterId",
                table: "QuestionSetProgresses");
        }
    }
}
