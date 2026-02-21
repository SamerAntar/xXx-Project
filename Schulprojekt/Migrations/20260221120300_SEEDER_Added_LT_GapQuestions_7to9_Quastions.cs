using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Schulprojekt.Migrations
{
    /// <inheritdoc />
    public partial class SEEDER_Added_LT_GapQuestions_7to9_Quastions : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // Questions
            migrationBuilder.InsertData(
                table: "Questions",
                columns: new[] { "Id", "StartText", "QuestionType", "QuestionSetId", "AllowsMultiple" },
                values: new object[,]
                {
                    {
                        7,
                        "Ein UML-Klassendiagramm beschreibt die __________ Struktur eines Systems und zeigt unter anderem Klassen, Attribute und __________.",
                        "GAP",
                        1,
                        true
                    },
                    {
                        8,
                        "Die Komposition wird im UML‑Klassendiagramm durch eine __________ Raute dargestellt und beschreibt eine besonders starke __________-Teil-Beziehung.",
                        "GAP",
                        1,
                        true
                    },
                    {
                        9,
                        "In einem Klassendiagramm steht das Minuszeichen (–) für __________ Sichtbarkeit, während das Pluszeichen (+) für __________ Sichtbarkeit steht.",
                        "GAP",
                        1,
                        true
                    }
                }
            );

            // QuestionTheme (Many-to-Many)
            migrationBuilder.InsertData(
                table: "QuestionTheme",
                columns: new[] { "QuestionsId", "ThemesId" },
                values: new object[,]
                {
                { 7, 1 },
                { 8, 1 },
                { 9, 1 }
                }
            );

            // GapFields
            migrationBuilder.InsertData(
                table: "GapFields",
                columns: new[] { "GapId", "QuestionId", "GapIndex", "InputType", "CorrectText", "CaseSensitive" },
                values: new object[,]
                {
                    { 1, 7, 0, "FREE_TEXT", "statische", false },
                    { 2, 7, 1, "FREE_TEXT", "Methoden", false },

                    { 3, 8, 0, "FREE_TEXT", "gefüllte", false },
                    { 4, 8, 1, "FREE_TEXT", "Ganzes", false },

                    { 5, 9, 0, "FREE_TEXT", "private", false },
                    { 6, 9, 1, "FREE_TEXT", "public", false },
                }
            );

            // GapOptions
            // Bei FREE_TEXT-Fragen gibt es normalerweise keine Optionen → leer lassen
            // Wenn du Choice-Fragen hättest, würde man hier die Optionen eintragen
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            // GapFields löschen
            migrationBuilder.DeleteData(table: "GapFields", keyColumn: "GapId", keyValue: 1);
            migrationBuilder.DeleteData(table: "GapFields", keyColumn: "GapId", keyValue: 2);
            migrationBuilder.DeleteData(table: "GapFields", keyColumn: "GapId", keyValue: 3);
            migrationBuilder.DeleteData(table: "GapFields", keyColumn: "GapId", keyValue: 4);
            migrationBuilder.DeleteData(table: "GapFields", keyColumn: "GapId", keyValue: 5);
            migrationBuilder.DeleteData(table: "GapFields", keyColumn: "GapId", keyValue: 6);


            // QuestionTheme löschen
            migrationBuilder.DeleteData(table: "QuestionTheme", keyColumns: new[] { "QuestionsId", "ThemesId" }, keyValues: new object[] { 7, 1 });
            migrationBuilder.DeleteData(table: "QuestionTheme", keyColumns: new[] { "QuestionsId", "ThemesId" }, keyValues: new object[] { 8, 1 });
            migrationBuilder.DeleteData(table: "QuestionTheme", keyColumns: new[] { "QuestionsId", "ThemesId" }, keyValues: new object[] { 9, 1 });

            // Questions löschen
            migrationBuilder.DeleteData(table: "Questions", keyColumn: "Id", keyValue: 7);
            migrationBuilder.DeleteData(table: "Questions", keyColumn: "Id", keyValue: 8);
            migrationBuilder.DeleteData(table: "Questions", keyColumn: "Id", keyValue: 9);
        }
    }
}
