using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Schulprojekt.Migrations
{
    /// <inheritdoc />
    public partial class SEEDER_Added_MultipleChoice_4to6_Quastions : Migration
    {
            /// <inheritdoc />
            protected override void Up(MigrationBuilder migrationBuilder)
            {
                //Questions Klassen Diagramm
                migrationBuilder.InsertData(
                    table: "Questions",
                    columns: new[] { "Id", "StartText", "QuestionType", "QuestionSetId", "AllowsMultiple" },
                    values: new object[,]
                    {
                    {
                        4,
                        "Welche Elemente können in einem UML-Klassendiagramm vorkommen?",
                        "MC",
                        1,
                        true
                    },
                    {
                        5,
                        "Welche UML-Diagramme zählen zu den Strukturdiagrammen?",
                        "MC",
                        1,
                        true
                    },
                    {
                        6,
                        "Welche Beziehungen können im Klassendiagramm dargestellt werden?",
                        "MC",
                        1,
                        true
                    }
                    }
                );

                //Question_Theme (Many-to-Many)
                migrationBuilder.InsertData(
                    table: "QuestionTheme",
                    columns: new[] { "QuestionsId", "ThemesId" },
                    values: new object[,]
                    {
                    { 4, 1 },
                    { 5, 1 },
                    { 6, 1 }
                    }
                );

                //MC Answers
                migrationBuilder.InsertData(
                    table: "McAnswers",
                    columns: new[] { "Id", "QuestionId", "OptionText", "Points", "IsCorrect", "OptionOrder" },
                    values: new object[,]
                    {
                    { 13, 4, "Attribute", 1, true, 0 },
                    { 14, 4, "Operationen", 1, true, 1 },
                    { 15, 4, "Lebenslinien", 0, false, 2 },
                    { 16, 4, "Pakete", 1, true, 3 },

                    { 17, 5, "Klassendiagramm", 1, true, 0 },
                    { 18, 5, "Sequenzdiagramm", 0, false, 1 },
                    { 19, 5, "Komponentendiagramm", 1, true, 2 },
                    { 20, 5, "Paketdiagramm", 1, true, 3 },

                    { 21, 6, "Aggregation", 1, true, 0 },
                    { 22, 6, "Komposition", 1, true, 1 },
                    { 23, 6, "Vererbung (Generalisierung)", 1, true, 2 },
                    { 24, 6, "Aktivitätsfluss", 0, false, 3 }
                    }
                );
            }

            /// <inheritdoc />
            protected override void Down(MigrationBuilder migrationBuilder)
            {
                migrationBuilder.DeleteData(table: "McAnswers", keyColumn: "Id", keyValues: new object[] { 13 });
                migrationBuilder.DeleteData(table: "McAnswers", keyColumn: "Id", keyValues: new object[] { 14 });
                migrationBuilder.DeleteData(table: "McAnswers", keyColumn: "Id", keyValues: new object[] { 15 });
                migrationBuilder.DeleteData(table: "McAnswers", keyColumn: "Id", keyValues: new object[] { 16 });
                migrationBuilder.DeleteData(table: "McAnswers", keyColumn: "Id", keyValues: new object[] { 17 });
                migrationBuilder.DeleteData(table: "McAnswers", keyColumn: "Id", keyValues: new object[] { 18 });
                migrationBuilder.DeleteData(table: "McAnswers", keyColumn: "Id", keyValues: new object[] { 19 });
                migrationBuilder.DeleteData(table: "McAnswers", keyColumn: "Id", keyValues: new object[] { 20 });
                migrationBuilder.DeleteData(table: "McAnswers", keyColumn: "Id", keyValues: new object[] { 21 });
                migrationBuilder.DeleteData(table: "McAnswers", keyColumn: "Id", keyValues: new object[] { 22 });
                migrationBuilder.DeleteData(table: "McAnswers", keyColumn: "Id", keyValues: new object[] { 23 });
                migrationBuilder.DeleteData(table: "McAnswers", keyColumn: "Id", keyValues: new object[] { 24 });


                migrationBuilder.DeleteData(table: "QuestionTheme", keyColumns: new[] { "QuestionsId", "ThemesId" }, keyValues: new object[] { 4, 1 });
                migrationBuilder.DeleteData(table: "QuestionTheme", keyColumns: new[] { "QuestionsId", "ThemesId" }, keyValues: new object[] { 5, 1 });
                migrationBuilder.DeleteData(table: "QuestionTheme", keyColumns: new[] { "QuestionsId", "ThemesId" }, keyValues: new object[] { 6, 1 });


                migrationBuilder.DeleteData(table: "Questions", keyColumn: "Id", keyValue: 4);
                migrationBuilder.DeleteData(table: "Questions", keyColumn: "Id", keyValue: 5);
                migrationBuilder.DeleteData(table: "Questions", keyColumn: "Id", keyValue: 6);
            }
        }
    }
