using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Schulprojekt.Migrations
{
    /// <inheritdoc />
    public partial class Added_AktivitätsdiagrammQuiz : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            //QuestionSet
            migrationBuilder.InsertData(
                table: "QuestionSets",
                columns: new[] { "Id", "Title", "TeamId" , "ThemaId" },
                values: new object[] { 3, "Aktivitätsdiagramm Quiz", 1, 1 }
            );

            //Questions
            migrationBuilder.InsertData(
                table: "Questions",
                columns: new[] { "Id", "StartText", "QuestionType", "QuestionSetId", "AllowsMultiple" },
                values: new object[,]
                {
                    {
                        21,
                        "Ein Aktivitätsdiagramm kann sowohl Kontroll‑ als auch Objektflüsse enthalten.",
                        "TF",
                        3,
                        false
                    },
                    {
                        22,
                        "Ein Fork-Knoten darf nur zwei ausgehende Kanten besitzen.",
                        "TF",
                        3,
                        false
                    },
                    {
                        23,
                        "Swimlanes dienen der Darstellung von Verantwortlichkeiten.",
                        "TF",
                        3,
                        false
                    },
                    {
                        24,
                        "Wofür wird ein Aktivitätsdiagramm hauptsächlich verwendet?",
                        "TF",
                        3,
                        false
                    },
                    {
                        25,
                        "Welche Aussage über Objektknoten ist korrekt?",
                        "TF",
                        3,
                        false
                    },
                    {
                        26,
                        "Welches Diagramm eignet sich am besten, um den Ablauf eines Prozesses mit Verzweigungen und Schleifen darzustellen?",
                        "TF",
                        3,
                        false
                    },
                    {
                        27,
                        "Welche Elemente können in einem Aktivitätsdiagramm vorkommen?",
                        "MC",
                        3,
                        true
                    },
                    {
                        28,
                        "Welche Aussagen über Partitionen (Swimlanes) sind korrekt?",
                        "MC",
                        3,
                        true
                    },
                    {
                        29,
                        "Ein Fork-Knoten teilt einen Ablauf in mehrere __________ Kontrollflüsse auf, während ein Join-Knoten diese wieder zu einem __________ Kontrollfluss zusammenführt.",
                        "MC",
                        3,
                        true
                    },
                    {
                        30,
                        "Ein Entscheidungsknoten teilt den Ablauf basierend auf __________ auf, während ein Zusammenführungsknoten mehrere __________ wieder zusammenführt.",
                        "MC",
                        3,
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
                    { 53, 21, "Wahr", 1, true, 0 },
                    { 54, 21, "Falsch", 0, false, 1 },

                    { 55, 22, "Wahr", 0, false, 0 },
                    { 56, 22, "Falsch", 1, true, 1 },

                    { 57, 23, "Wahr", 1, true, 0 },
                    { 58, 23, "Falsch", 0, false, 1 },

                    { 59, 24, "Darstellung der Klassenhierarchie", 0, false, 0 },
                    { 60, 24, "Modellierung von Abläufen und Workflows", 1, true, 1 },
                    { 61, 24, "Beschreibung der Systemarchitektur", 0, false, 2 },
                    { 62, 24, "Darstellung von Objektinteraktionen über Zeit", 0, false, 3 },

                    { 63, 25, "Sie dürfen nur in Sequenzdiagrammen verwendet werden", 0, false, 0 },
                    { 64, 25, "Sie können Datenflüsse innerhalb eines Aktivitätsdiagramms darstellen", 1, true, 1 },
                    { 65, 25, "Sie ersetzen Kontrollflüsse vollständig", 0, false, 2 },
                    { 66, 25, "Sie dürfen keine Typen besitzen", 0, false, 3 },

                    { 67, 26, "Klassendiagramm", 0, false, 0 },
                    { 68, 26, "Aktivitätsdiagramm", 1, true, 1 },
                    { 69, 26, "Sequenzdiagramm", 0, false, 2 },
                    { 70, 26, "Zustandsdiagramm", 0, false, 3 },

                    { 71, 27, "Aktionen", 1, true, 0 },
                    { 72, 27, "Entscheidungsknoten", 1, true, 1 },
                    { 73, 27, "Lebenslinien", 0, false, 2 },
                    { 74, 27, "Start- und Endknoten", 1, true, 3 },

                    { 75, 28, "Sie ordnen Aktivitäten Verantwortlichkeiten zu", 1, true, 0 },
                    { 76, 28, "Sie dürfen nur horizontal dargestellt werden", 0, false, 1 },
                    { 77, 28, "Sie können sowohl Personen als auch Systeme repräsentieren", 1, true, 2 },
                    { 78, 28, "Sie beeinflussen die Ausführung der Aktivitäten", 0, false, 3 }
                }
            );

            // GapFields
            migrationBuilder.InsertData(
                table: "GapFields",
                columns: new[] { "GapId", "QuestionId", "GapIndex", "InputType", "CorrectText", "CaseSensitive" },
                values: new object[,]
                {
                    { 11, 29, 0, "FREE_TEXT", "parallele", false },
                    { 12, 29, 1, "FREE_TEXT", "einzigen", false },

                    { 13, 30, 0, "FREE_TEXT", "Bedingungen", false },
                    { 14, 30, 1, "FREE_TEXT", "Kontrollflüsse", false },
                }
            );
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
