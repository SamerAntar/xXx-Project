using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Schulprojekt.Migrations
{
    /// <inheritdoc />
    public partial class SeederQuestionSets : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
        INSERT INTO `questionsets` (`Id`, `Title`, `TeamId`, `ThemaId`)
        VALUES
        (1, 'Wirtschaft & Sozialkunde – 50', 1, 3),
        (2, 'Cursed-Crib', 2, 3),
        (3, 'Klassendiagramm Quiz', 3, 1),
        (4, 'Sequenzdiagramm Quiz', 3, 1),
        (5, 'Aktivitätsdiagramm Quiz', 3, 1),
        (6, 'Zustandsdiagramm Quiz', 3, 1),
        (7, 'Wirtschaft Fragen Set', 4, 3),
        (8, 'GBoyz Datenbank - SQL', 5, 5),
        (9, 'NORMALISIERUNG', 6, 4),
        (10, 'SELECT_ABFRAGEN', 6, 5),
        (11, 'ER_MODELLIERUNG', 6, 4),
        (12, 'JOINS_SUBQUERIES', 6, 5),
        (13, 'SQL_GRUNDLAGEN', 6, 5),
        (14, 'DDL_DML', 6, 5),
        (15, 'Objektorientierung und Algorithmik', 7, 7),
        (16, 'Level 1', 8, 7),
        (17, 'Level 2', 8, 7),
        (18, 'Level 3', 8, 7),
        (19, 'Level 4', 8, 7),
        (20, 'Level 5', 8, 7),
        (21, 'Fragen von der Gruppe Dummy2Pro (DB-Modell.)', 9, 4),
        (22, 'Hammer-Set (SQL)', 10, 5),
        (23, 'Pseudocode und Algorithmen', 11, 7),
        (24, 'Hammer-Set (DB-Modell.)', 10, 4),
        (25, 'Hammer-Set (Recht)', 10, 2),
        (26, 'Hammer-Set (Wirtscchaft)', 10, 3),
        (27, 'Hammer-Set (UML)', 10, 1),
        (28, 'Hammer-Set (Masch. Lernen)', 10, 6),
        (29, 'Hammer-Set (Pseudocode)', 10, 7),
        (30, 'Fragen von der Gruppe Dummy2Pro (SQL)', 9, 5);
    ");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DELETE FROM `questionsets` WHERE `Id` BETWEEN 1 AND 30;");
        }
    }
}
