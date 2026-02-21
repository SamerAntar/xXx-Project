using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Schulprojekt.Migrations
{
    /// <inheritdoc />
    public partial class SEEDER_Added_SingleChoice_1to3_Quastions : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            //Team
            migrationBuilder.InsertData(
                table: "Teams",
                columns: new[] { "Id", "Name" },
                values: new object[] { 1, "ITF-231 Team M.S.H.M.J" }
            );

            //QuestionSet
            migrationBuilder.InsertData(
                table: "QuestionSets",
                columns: new[] { "Id", "Title", "TeamId" },
                values: new object[] { 1, "Klassen_Diagramm Quiz", 1 }
            );

            //Theme
            migrationBuilder.InsertData(
                table: "Themes",
                columns: new[] { "Id", "Name", "Description" },
                values: new object[] { 1, "Klassen Diagramm", null }
            );

            //Questions
            migrationBuilder.InsertData(
                table: "Questions",
                columns: new[] { "Id", "StartText", "QuestionType", "QuestionSetId", "AllowsMultiple" },
                values: new object[,]
                {
                    {
                        1,
                        "Welches UML-Diagramm wird hauptsächlich verwendet, um die statische Struktur eines Systems darzustellen?",
                        "TF",
                        1,
                        false
                    },
                    {
                        2,
                        "Welche Beziehung wird im UML-Klassendiagramm durch eine durchgezogene Linie mit einer Raute dargestellt?",
                        "TF",
                        1,
                        false
                    },
                    {
                        3,
                        "Welche Art von Beziehung beschreibt eine Vererbung zwischen zwei Klassen?",
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
                    { 1, 1 },
                    { 2, 1 },
                    { 3, 1 }
                }
            );

            //MC Answers
            migrationBuilder.InsertData(
                table: "McAnswers",
                columns: new[] { "Id", "QuestionId", "OptionText", "Points", "IsCorrect", "OptionOrder" },
                values: new object[,]
                {
                    { 1, 1, "Sequenzdiagramm", 0, false, 0 },
                    { 2, 1, "Aktivitätsdiagramm", 0, false, 1 },
                    { 3, 1, "Klassendiagramm", 1, true, 2 },
                    { 4, 1, "Zustandsdiagramm", 0, false, 3 },

                    { 5, 2, "Komposition", 1, true, 0 },
                    { 6, 2, "Aggregation", 0, false, 1 },
                    { 7, 2, "Assoziation", 0, false, 2 },
                    { 8, 2, "Generalisierung", 0, false, 3 },

                    { 9, 3, "Abhängigkeit", 0, false, 0 },
                    { 10, 3, "Assoziation", 0, false, 1 },
                    { 11, 3, "Generalisierung", 1, true, 2 },
                    { 12, 3, "Realisation", 0, false, 3 }
                }
            );
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(table: "McAnswers", keyColumn: "Id", keyValues: new object[] { 1 });
            migrationBuilder.DeleteData(table: "McAnswers", keyColumn: "Id", keyValues: new object[] { 2 });
            migrationBuilder.DeleteData(table: "McAnswers", keyColumn: "Id", keyValues: new object[] { 3 });
            migrationBuilder.DeleteData(table: "McAnswers", keyColumn: "Id", keyValues: new object[] { 4 });
            migrationBuilder.DeleteData(table: "McAnswers", keyColumn: "Id", keyValues: new object[] { 5 });
            migrationBuilder.DeleteData(table: "McAnswers", keyColumn: "Id", keyValues: new object[] { 6 });
            migrationBuilder.DeleteData(table: "McAnswers", keyColumn: "Id", keyValues: new object[] { 7 });
            migrationBuilder.DeleteData(table: "McAnswers", keyColumn: "Id", keyValues: new object[] { 8 });
            migrationBuilder.DeleteData(table: "McAnswers", keyColumn: "Id", keyValues: new object[] { 9 });
            migrationBuilder.DeleteData(table: "McAnswers", keyColumn: "Id", keyValues: new object[] { 10 });
            migrationBuilder.DeleteData(table: "McAnswers", keyColumn: "Id", keyValues: new object[] { 11 });
            migrationBuilder.DeleteData(table: "McAnswers", keyColumn: "Id", keyValues: new object[] { 12 });


            migrationBuilder.DeleteData(table: "QuestionTheme", keyColumns: new[] { "QuestionsId", "ThemesId" }, keyValues: new object[] { 1, 1 });
            migrationBuilder.DeleteData(table: "QuestionTheme", keyColumns: new[] { "QuestionsId", "ThemesId" }, keyValues: new object[] { 2, 1 });
            migrationBuilder.DeleteData(table: "QuestionTheme", keyColumns: new[] { "QuestionsId", "ThemesId" }, keyValues: new object[] { 3, 1 });


            migrationBuilder.DeleteData(table: "Questions", keyColumn: "Id", keyValue: 1);
            migrationBuilder.DeleteData(table: "Questions", keyColumn: "Id", keyValue: 2);
            migrationBuilder.DeleteData(table: "Questions", keyColumn: "Id", keyValue: 3);

            migrationBuilder.DeleteData(table: "Themes", keyColumn: "Id", keyValue: 1);
            migrationBuilder.DeleteData(table: "QuestionSets", keyColumn: "Id", keyValue: 1);
            migrationBuilder.DeleteData(table: "Teams", keyColumn: "Id", keyValue: 1);
        }
    }
}
