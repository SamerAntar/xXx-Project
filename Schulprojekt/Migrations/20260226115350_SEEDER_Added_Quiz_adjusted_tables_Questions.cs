using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Schulprojekt.Migrations
{
    /// <inheritdoc />
    public partial class SEEDER_Added_Quiz_adjusted_tables_Questions : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "McAnswers",
                keyColumn: "Id",
                keyValue: 42,
                column: "OptionText",
                value: "Ein alt-Fragment benötigt mindestens zwei Akteure"
            );

            migrationBuilder.UpdateData(
                table: "Questions",
                keyColumn: "Id",
                keyValue: 2,
                column: "StartText",
                value: "Welche Beziehung wird im UML-Klassendiagramm durch eine durchgezogene Linie mit einer ausgefüllten Raute dargestellt?"
            );

            migrationBuilder.UpdateData(
                table: "Questions",
                keyColumn: "Id",
                keyValue: 19,
                column: "QuestionType",
                value: "GAP"
            );

            migrationBuilder.UpdateData(
                table: "Questions",
                keyColumn: "Id",
                keyValue: 20,
                column: "QuestionType",
                value: "GAP"
            );

            migrationBuilder.UpdateData(
                table: "Questions",
                keyColumn: "Id",
                keyValue: 29,
                column: "QuestionType",
                value: "GAP"
            );

            migrationBuilder.UpdateData(
                table: "Questions",
                keyColumn: "Id",
                keyValue: 30,
                column: "QuestionType",
                value: "GAP"
            );

            migrationBuilder.UpdateData(
                table: "QuestionSets",
                keyColumn: "Id",
                keyValue: 1,
                column: "Title",
                value: "Klassendiagramm Quiz"
            );
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "McAnswers",
                keyColumn: "Id",
                keyValue: 42,
                column: "OptionText",
                value: "Ein alt-Fragment benötigt mindestens zwei Operanden"
            );

            migrationBuilder.UpdateData(
                table: "Questions",
                keyColumn: "Id",
                keyValue: 2,
                column: "StartText",
                value: "Welche Beziehung wird im UML-Klassendiagramm durch eine durchgezogene Linie mit einer Raute dargestellt?"
            );

            migrationBuilder.UpdateData(
                table: "Questions",
                keyColumn: "Id",
                keyValue: 19,
                column: "QuestionType",
                value: "MC"
            );

            migrationBuilder.UpdateData(
                table: "Questions",
                keyColumn: "Id",
                keyValue: 20,
                column: "QuestionType",
                value: "MC"
            );

            migrationBuilder.UpdateData(
                table: "Questions",
                keyColumn: "Id",
                keyValue: 29,
                column: "QuestionType",
                value: "MC"
            );

            migrationBuilder.UpdateData(
                table: "Questions",
                keyColumn: "Id",
                keyValue: 30,
                column: "QuestionType",
                value: "MC"
            );

            migrationBuilder.UpdateData(
                table: "QuestionSets",
                keyColumn: "Id",
                keyValue: 1,
                column: "Title",
                value: "Klassen_Diagramm Quiz"
            );
        }
    }
}
