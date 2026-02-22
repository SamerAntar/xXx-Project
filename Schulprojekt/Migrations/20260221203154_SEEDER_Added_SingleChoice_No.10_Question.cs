using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Schulprojekt.Migrations
{
    /// <inheritdoc />
    public partial class SEEDER_Added_SingleChoice_No10_Question : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            //Questions
            migrationBuilder.InsertData(
                table: "Questions",
                columns: new[] { "Id", "StartText", "QuestionType", "QuestionSetId", "AllowsMultiple" },
                values: new object[,]
                {
                    {
                        10,
                        "Eine Aggregation ist stärker als eine Komposition.",
                        "TF",
                        1,
                        false
                    }
                }
            );

            //Question_Theme (Many-to-Many)
            migrationBuilder.InsertData(
                table: "QuestionTheme",
                columns: new[] { "QuestionsId", "ThemesId" },
                values: new object[,]
                {
                    { 10, 1 }
                }
            );

            //MC Answers
            migrationBuilder.InsertData(
                table: "McAnswers",
                columns: new[] { "Id", "QuestionId", "OptionText", "Points", "IsCorrect", "OptionOrder" },
                values: new object[,]
                {
                    { 25, 10, "Wahr", 0, false, 0 },
                    { 26, 10, "Falsch", 1, true, 1 },
                }
            );
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(table: "McAnswers", keyColumn: "Id", keyValues: new object[] { 25 });
            migrationBuilder.DeleteData(table: "McAnswers", keyColumn: "Id", keyValues: new object[] { 26 });

            migrationBuilder.DeleteData(table: "QuestionTheme", keyColumns: new[] { "QuestionsId", "ThemesId" }, keyValues: new object[] { 10, 1 });

            migrationBuilder.DeleteData(table: "Questions", keyColumn: "Id", keyValue: 10);
        }
    }
}
