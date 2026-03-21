using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Schulprojekt.Migrations
{
    /// <inheritdoc />
    public partial class Add_GamePlaceName_And_Description : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Themen",
                keyColumn: "Id",
                keyValue: 1,
                columns: ["GamePlaceName", "GamePlaceDescription"],
                values: ["UMLand", "Mysteriöse, einsame Inseln, wo alles in Klassen, Aktivitäten oder Sequenzen modelliert werden muss."]
            );
            migrationBuilder.UpdateData(
                table: "Themen",
                keyColumn: "Id",
                keyValue: 2,
                columns: ["GamePlaceName", "GamePlaceDescription"],
                values: ["Law(less) Zone", "Verruchte Gegend, wo nur das Wissen über das Recht einen entkommen lässt."]
            );
            migrationBuilder.UpdateData(
                table: "Themen",
                keyColumn: "Id",
                keyValue: 3,
                columns: ["GamePlaceName", "GamePlaceDescription"],
                values: ["Economic New City", "Eine leuchtende Metropole, wo Wirtschaft an der Tagesordnung steht. Wer sich damit nicht auskennt, geht gnadenlos unter!"]
            );
            migrationBuilder.UpdateData(
                   table: "Themen",
                    keyColumn: "Id",
                    keyValue: 4,
                    columns: ["GamePlaceName", "GamePlaceDescription"],
                    values: ["Modell Palace of Database", "Die Gänge und Räume des prächtige Palast der Republik Database müssen so gut wie möglich genormt werden."]
            );
            migrationBuilder.UpdateData(
                    table: "Themen",
                    keyColumn: "Id",
                    keyValue: 5,
                    columns: ["GamePlaceName", "GamePlaceDescription"],
                    values: ["SQL Autonom Region", "Eine ruhige, simple rurale Gegend. Aber mit den richtigen Einsatz kann man so viel hier erreichen!"]
             );
            migrationBuilder.UpdateData(
                    table: "Themen",
                    keyColumn: "Id",
                    keyValue: 6,
                    columns: ["GamePlaceName", "GamePlaceDescription"],
                    values: ["MechLearn Arena", "Dieser Wolkenkratzer ist bekannt für seine ständige Kämpfe. Wer sich hier nichts ständig neues beibringt, verliert!"]
            );
            migrationBuilder.UpdateData(
                    table: "Themen",
                    keyColumn: "Id",
                    keyValue: 7,
                    columns: ["GamePlaceName", "GamePlaceDescription"],
                     values: ["Gate of Pseudocode", "Ein riesiges Tor, welches nur mit richtiger Logik geöffnet werden kann."]
            );


            migrationBuilder.InsertData(
            table: "Themen",
            columns: new[] { "Id", "Name", "Description", "GamePlaceName", "GamePlaceDescription" },
            values: new object[,]
            {
                { 0, "-", "Dummy-Datensatz, damit Charactere direkt freischaltbar sind.", "Dark Area", "Die Herausforderungen dieses Gebietes sind selbst für die IHKunter Organisation unbekannt." },
            }
    );
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
