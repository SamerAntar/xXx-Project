using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Schulprojekt.Migrations
{
    /// <inheritdoc />
    public partial class SeederThemen : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
        INSERT IGNORE  INTO `themen`
        (`Id`, `Name`, `Description`, `GamePlaceName`, `GamePlaceDescription`)
        VALUES
        (0, '-', 'Dummy-Datensatz, damit Charactere direkt freischaltbar sind.', 'Dark Area', 'Die Herausforderungen dieses Gebietes sind selbst für die IHKunter Organisation unbekannt.'),
        (1, 'UML', NULL, 'UMLand', 'Mysteriöse, einsame Inseln, wo alles in Klassen, Aktivitäten oder Sequenzen modelliert werden muss.'),
        (2, 'Recht', 'Alles was mit Gesetzesgrundlagen zu tun hat', 'Law(less) Zone', 'Verruchte Gegend, wo nur das Wissen über das Recht einen entkommen lässt.'),
        (3, 'Wirtschaft', 'Alles, was NICHT mit Recht, aber mit Wirtschaft zu tun hat (Kalkulation, Prozesse,...)', 'Economic New City', 'Eine leuchtende Metropole, wo Wirtschaft an der Tagesordnung steht. Wer sich damit nicht auskennt, geht gnadenlos unter!'),
        (4, 'Datenbanken Modellierung', 'ERD, relationales Tabellenmodell', 'Modell Palace of Database', 'Die Gänge und Räume des prächtigen Palastes der Republik Database müssen so gut wie möglich genormt werden.'),
        (5, 'Datenbank - SQL', NULL, 'SQL Autonom Region', 'Eine ruhige, simple rurale Gegend. Aber mit den richtigen Einsatz kann man so viel hier erreichen!'),
        (6, 'Maschinelles Lernen', NULL, 'MechLearn Arena', 'Dieser Wolkenkratzer ist bekannt für seine ständige Kämpfe. Wer sich hier nichts ständig neues beibringt, verliert!'),
        (7, 'Programmierung Pseudocode', NULL, 'Gate of Pseudocode', 'Ein riesiges Tor, welches nur mit richtiger Logik geöffnet werden kann.');
    ");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DELETE FROM `themen` WHERE `Id` BETWEEN 0 AND 7;");
        }
    }
}
