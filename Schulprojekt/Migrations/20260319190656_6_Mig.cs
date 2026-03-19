using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Schulprojekt.Migrations
{
    /// <inheritdoc />
    public partial class _6_Mig : Migration
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
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
