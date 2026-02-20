using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Schulprojekt.Migrations
{
    /// <inheritdoc />
    public partial class Adjusted_Themes_Name : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_QuestionTheme_themes_ThemesId",
                table: "QuestionTheme");

            migrationBuilder.DropPrimaryKey(
                name: "PK_themes",
                table: "themes");

            migrationBuilder.RenameTable(
                name: "themes",
                newName: "Themes");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Themes",
                table: "Themes",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_QuestionTheme_Themes_ThemesId",
                table: "QuestionTheme",
                column: "ThemesId",
                principalTable: "Themes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_QuestionTheme_Themes_ThemesId",
                table: "QuestionTheme");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Themes",
                table: "Themes");

            migrationBuilder.RenameTable(
                name: "Themes",
                newName: "themes");

            migrationBuilder.AddPrimaryKey(
                name: "PK_themes",
                table: "themes",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_QuestionTheme_themes_ThemesId",
                table: "QuestionTheme",
                column: "ThemesId",
                principalTable: "themes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
