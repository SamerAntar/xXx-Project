using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Schulprojekt.Migrations
{
    /// <inheritdoc />
    public partial class make_Thema_not_Nullable_In_QuestionSet : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_QuestionSets_Themen_ThemaId",
                table: "QuestionSets");

            migrationBuilder.AlterColumn<int>(
                name: "ThemaId",
                table: "QuestionSets",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_QuestionSets_Themen_ThemaId",
                table: "QuestionSets",
                column: "ThemaId",
                principalTable: "Themen",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_QuestionSets_Themen_ThemaId",
                table: "QuestionSets");

            migrationBuilder.AlterColumn<int>(
                name: "ThemaId",
                table: "QuestionSets",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_QuestionSets_Themen_ThemaId",
                table: "QuestionSets",
                column: "ThemaId",
                principalTable: "Themen",
                principalColumn: "Id");
        }
    }
}
