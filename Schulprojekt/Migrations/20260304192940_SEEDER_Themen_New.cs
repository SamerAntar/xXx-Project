using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Schulprojekt.Migrations
{
    /// <inheritdoc />
    public partial class SEEDER_Themen_New : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            //Theme
            migrationBuilder.InsertData(
                table: "Themen",
                columns: new[] { "Id", "Name", "Description" },
                values: new object[,]
                {
                    { 1, "UML", "Klassendiagramm, Sequenzdiagramm, Zustandsdiagramm, Use-Case Diagramm, Aktivitätsdiagramm" },
                    { 2, "Recht", "Alles was mit Gesetzesgrundlagen zu tun hat" },
                    { 3, "Wirtschaft", "Alles, was NICHT mit Recht, aber mit Wirtschaft zu tun hat (Kalkulation, Prozesse,...)" },
                    { 4, "Datenbanken Modellierung", "ERD, relationales Tabellenmodell" },
                    { 5, "Datenbank - SQL", null },
                    { 6, "Maschinelles Lernen", null },
                    { 7, "Programmierung Pseudocode", null },
                }
            );
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
