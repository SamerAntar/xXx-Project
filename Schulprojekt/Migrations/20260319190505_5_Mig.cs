using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Schulprojekt.Migrations
{
    /// <inheritdoc />
    public partial class _5_Mig : Migration
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

        }
    }
}
