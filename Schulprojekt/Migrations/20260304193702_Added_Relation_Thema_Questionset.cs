using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Schulprojekt.Migrations
{
    /// <inheritdoc />
    public partial class Added_Relation_Thema_Questionset : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ThemaId",
                table: "QuestionSets",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_QuestionSets_ThemaId",
                table: "QuestionSets",
                column: "ThemaId");

            migrationBuilder.AddForeignKey(
                name: "FK_QuestionSets_Themen_ThemaId",
                table: "QuestionSets",
                column: "ThemaId",
                principalTable: "Themen",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_QuestionSets_Themen_ThemaId",
                table: "QuestionSets");

            migrationBuilder.DropIndex(
                name: "IX_QuestionSets_ThemaId",
                table: "QuestionSets");

            migrationBuilder.DropColumn(
                name: "ThemaId",
                table: "QuestionSets");
        }
    }
}
