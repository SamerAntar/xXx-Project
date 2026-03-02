using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Schulprojekt.Migrations
{
    /// <inheritdoc />
    public partial class SEEDER_Added_Quiz_ZustandsDiagramm_Questions : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            //QuestionSet
            migrationBuilder.InsertData(
                table: "QuestionSets",
                columns: new[] { "Id", "Title", "TeamId" },
                values: new object[] { 4, "Zustandsdiagramm Quiz", 1 }
            );

            //Questions
            migrationBuilder.InsertData(
                table: "Questions",
                columns: new[] { "Id", "StartText", "QuestionType", "QuestionSetId", "AllowsMultiple" },
                values: new object[,]
                {
                    {
                        31,
                        "Ein Übergang darf keine Aktion besitzen, wenn er aus einem Endzustand kommt.",
                        "TF",
                        4,
                        false
                    },
                    {
                        32,
                        "Ein Übergang kann mehrere Ereignisse gleichzeitig als Trigger besitzen.",
                        "TF",
                        4,
                        false
                    },
                    {
                        33,
                        "Ein Endzustand darf keine ausgehenden Übergänge besitzen.",
                        "TF",
                        4,
                        false
                    },
                    {
                        34,
                        "Welche Aussage über Zustandsdiagramme ist korrekt?",
                        "TF",
                        4,
                        false
                    },
                    {
                        35,
                        "Welche Aussage über Subzustände ist korrekt?",
                        "TF",
                        4,
                        false
                    },
                    {
                        36,
                        "Welche Darstellung gehört typischerweise zu einem Zustandsdiagramm?",
                        "TF",
                        4,
                        false
                    },
                    {
                        37,
                        "Welche Symbole können in einem Zustandsdiagramm vorkommen?",
                        "MC",
                        4,
                        true
                    },
                    {
                        38,
                        "Welche Aussagen über Übergänge sind korrekt?",
                        "MC",
                        4,
                        true
                    },
                    {
                        39,
                        "Ein Zustand kann interne Aktivitäten besitzen, die als __________, __________ oder __________ bezeichnet werden.",
                        "GAP",
                        4,
                        true
                    },
                    {
                        40,
                        "Ein Zustandsdiagramm beschreibt, wie ein Objekt auf __________ reagiert und dabei von einem Zustand in den __________ übergeht.",
                        "GAP",
                        4,
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
                    { 31, 1 },
                    { 32, 1 },
                    { 33, 1 },
                    { 34, 1 },
                    { 35, 1 },
                    { 36, 1 },
                    { 37, 1 },
                    { 38, 1 },
                    { 39, 1 },
                    { 40, 1 }
                }
            );

            //MC Answers
            migrationBuilder.InsertData(
                table: "McAnswers",
                columns: new[] { "Id", "QuestionId", "OptionText", "Points", "IsCorrect", "OptionOrder" },
                values: new object[,]
                {
                    { 79, 31, "Wahr", 1, true, 0 },
                    { 80, 31, "Falsch", 0, false, 1 },

                    { 81, 32, "Wahr", 1, true, 0 },
                    { 82, 32, "Falsch", 0, false, 1 },

                    { 83, 33, "Wahr", 1, true, 0 },
                    { 84, 33, "Falsch", 0, false, 1 },

                    { 85, 34, "Ein Zustand darf niemals interne Aktivitäten enthalten", 0, false, 0 },
                    { 86, 34, "Ein Übergang kann eine Bedingung und eine Aktion besitzen", 1, true, 1 },
                    { 87, 34, "Ein Zustand kann nur genau einen eingehenden Übergang haben", 0, false, 2 },
                    { 88, 34, "Ein Endzustand darf weitere Übergänge besitzen", 0, false, 3 },

                    { 89, 35, "Ein Zustand darf nur genau einen Subzustand besitzen", 0, false, 0 },
                    { 90, 35, "Subzustände sind nur in parallelen Regionen erlaubt", 0, false, 1 },
                    { 91, 35, "Subzustände ermöglichen hierarchische Zustandsmaschinen", 1, true, 2 },
                    { 92, 35, "Subzustände dürfen keine Übergänge besitzen", 0, false, 3 },

                    { 93, 36, "Lebenslinien", 0, false, 0 },
                    { 94, 36, "Zustände und Übergänge", 1, true, 1 },
                    { 95, 36, "Komponenten und Schnittstellen", 0, false, 2 },
                    { 96, 36, "Pakete und Abhängigkeiten", 0, false, 3 },

                    { 97, 37, "Zustände", 1, true, 0 },
                    { 98, 37, "Übergänge", 1, true, 1 },
                    { 99, 37, "Swimlanes", 0, false, 2 },
                    { 100, 37, "Ereignisse", 1, true, 3 },

                    { 101, 38, "Ein Übergang kann mehrere Aktionen enthalten", 1, true, 0 },
                    { 102, 38, "Ein Übergang kann eine Bedingung besitzen", 1, true, 1 },
                    { 103, 38, "Ein Übergang darf keinen Zielzustand haben", 0, false, 2 },
                    { 104, 38, "Ein Übergang kann ein Ereignis auslösen", 0, false, 3 }
                }
            );

            // GapFields
            migrationBuilder.InsertData(
                table: "GapFields",
                columns: new[] { "GapId", "QuestionId", "GapIndex", "InputType", "CorrectText", "CaseSensitive" },
                values: new object[,]
                {
                    { 15, 39, 0, "FREE_TEXT", "entry", false },
                    { 16, 39, 1, "FREE_TEXT", "do", false },
                    { 17, 39, 2, "FREE_TEXT", "exit", false },

                    { 18, 40, 0, "FREE_TEXT", "Ereignisse", false },
                    { 19, 40, 1, "FREE_TEXT", "nächsten Zustand", false },
                }
            );
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            // MC Answers löschen
            migrationBuilder.DeleteData(
                table: "McAnswers",
                keyColumn: "Id",
                keyValues: new object[] { 79, 80, 81, 82, 83, 84, 85, 86, 87, 88, 89, 90, 91, 92, 93, 94, 95, 96, 97, 98, 99, 100, 101, 102, 103, 104 }
            );

            // GapFields löschen
            migrationBuilder.DeleteData(
                table: "GapFields",
                keyColumn: "GapId",
                keyValues: new object[] { 15, 16, 17, 18, 19 }
            );

            // QuestionTheme (Many-to-Many) löschen
            migrationBuilder.DeleteData(
                table: "QuestionTheme",
                keyColumns: new[] { "QuestionsId", "ThemesId" },
                keyValues: new object[,]
                {
                    { 31, 1 },
                    { 32, 1 },
                    { 33, 1 },
                    { 34, 1 },
                    { 35, 1 },
                    { 36, 1 },
                    { 37, 1 },
                    { 38, 1 },
                    { 39, 1 },
                    { 40, 1 }
                }
            );

            // Questions löschen
            migrationBuilder.DeleteData(
                table: "Questions",
                keyColumn: "Id",
                keyValues: new object[] { 31, 32, 33, 34, 35, 36, 37, 38, 39, 40 }
            );

            // QuestionSet löschen
            migrationBuilder.DeleteData(
                table: "QuestionSets",
                keyColumn: "Id",
                keyValue: 4
            );
        }
    }
}
