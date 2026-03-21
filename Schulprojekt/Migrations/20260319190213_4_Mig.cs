using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Schulprojekt.Migrations
{
    /// <inheritdoc />
    public partial class _4_Mig : Migration
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

        }
    }
}
