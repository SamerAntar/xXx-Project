using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Schulprojekt.Migrations
{
    /// <inheritdoc />
    public partial class Added_SequenzdiagrammQuiz : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            //QuestionSet
            migrationBuilder.InsertData(
                table: "QuestionSets",
                columns: new[] { "Id", "Title", "TeamId" , "ThemaId"},
                values: new object[] { 2, "Sequenzdiagramm Quiz", 1, 1 }
            );

            //Questions
            migrationBuilder.InsertData(
                table: "Questions",
                columns: new[] { "Id", "StartText", "QuestionType", "QuestionSetId", "AllowsMultiple" },
                values: new object[,]
                {
                    {
                        11,
                        "Eine synchrone Nachricht blockiert den Sender, bis eine Antwort erfolgt.",
                        "TF",
                        2,
                        false
                    },
                    {
                        12,
                        "Ein opt-Fragment ist ein Spezialfall eines alt‑Fragments.",
                        "TF",
                        2,
                        false
                    },
                    {
                        13,
                        "Eine Aktivierung zeigt an, dass ein Objekt gerade eine Operation ausführt.",
                        "TF",
                        2,
                        false
                    },
                    {
                        14,
                        "Welches UML-Diagramm zeigt die Interaktion zwischen Objekten in zeitlicher Reihenfolge?",
                        "TF",
                        2,
                        false
                    },
                    {
                        15,
                        "Welche Aussage über Sequenzdiagramme trifft zu?",
                        "TF",
                        2,
                        false
                    },
                    {
                        16,
                        "Welche Aussage über kombinierte Fragmente ist korrekt?",
                        "TF",
                        2,
                        false
                    },
                    {
                        17,
                        "Welche Aussagen über Lebenslinien sind korrekt?",
                        "MC",
                        2,
                        true
                    },
                    {
                        18,
                        "Welche Elemente können in einem Sequenzdiagramm vorkommen?",
                        "MC",
                        2,
                        true
                    },
                    {
                        19,
                        "Eine synchrone Nachricht blockiert den Sender, bis eine __________ erfolgt, während eine asynchrone Nachricht __________ ausgeführt wird.",
                        "MC",
                        2,
                        true
                    },
                    {
                        20,
                        "Ein kombiniertes Fragment mit dem Operator __________ beschreibt alternative Abläufe, während der Operator __________ Wiederholungen modelliert.",
                        "MC",
                        2,
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
                    { 27, 11, "Wahr", 1, true, 0 },
                    { 28, 11, "Falsch", 0, false, 1 },

                    { 29, 12, "Wahr", 1, true, 0 },
                    { 30, 12, "Falsch", 0, false, 1 },

                    { 31, 13, "Wahr", 1, true, 0 },
                    { 32, 13, "Falsch", 0, false, 1 },

                    { 33, 14, "Komponentendiagramm", 0, false, 0 },
                    { 34, 14, "Use-Case-Diagramm", 0, false, 1 },
                    { 35, 14, "Sequenzdiagramm", 1, true, 2 },
                    { 36, 14, "Paketdiagramm", 0, false, 3 },

                    { 37, 15, "Nachrichten dürfen keine Rückgabewerte enthalten", 0, false, 0 },
                    { 38, 15, "Parallele Abläufe werden durch kombinierte Fragmente dargestellt", 1, true, 1 },
                    { 39, 15, "Ein Objekt kann nur eine Lebenslinie besitzen", 0, false, 2 },
                    { 40, 15, "Ein Sequenzdiagramm zeigt ausschließlich synchrone Kommunikation", 0, false, 3 },

                    { 41, 16, "Ein loop-Fragment darf keine Bedingungen enthalten", 0, false, 0 },
                    { 42, 16, "Ein alt-Fragment benötigt mindestens zwei Operanden", 0, false, 1 },
                    { 43, 16, "Ein par-Fragment erzwingt synchrone Ausführung", 0, false, 2 },
                    { 44, 16, "Ein opt-Fragment ist ein Spezialfall von alt", 1, true, 3 },

                    { 45, 17, "Eine Lebenslinie kann mehrere Aktivierungen besitzen", 1, true, 0 },
                    { 46, 17, "Eine Lebenslinie endet immer im Endzustand", 0, false, 1 },
                    { 47, 17, "Eine Lebenslinie kann durch ein X beendet werden", 1, true, 2 },
                    { 48, 17, "Eine Lebenslinie darf keine Nachrichten senden", 0, false, 3 },

                    { 49, 18, "Nachrichten", 1, true, 0 },
                    { 50, 18, "Zustände", 0, false, 1 },
                    { 51, 18, "Aktivierungen (Execution Specifications)", 1, true, 2 },
                    { 52, 18, "Lebenslinien", 1, true, 3 }
                }
            );

            // GapFields
            migrationBuilder.InsertData(
                table: "GapFields",
                columns: new[] { "GapId", "QuestionId", "GapIndex", "InputType", "CorrectText", "CaseSensitive" },
                values: new object[,]
                {
                    { 7, 19, 0, "FREE_TEXT", "Rückgabe", false },
                    { 8, 19, 1, "FREE_TEXT", "nicht blockierend", false },

                    { 9, 20, 0, "FREE_TEXT", "alt", false },
                    { 10, 20, 1, "FREE_TEXT", "loop", false },
                }
            );
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
