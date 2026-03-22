using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Schulprojekt.Migrations
{
    /// <inheritdoc />
    public partial class SeederTeams : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
        INSERT INTO `teams` (`Id`, `Name`)
        VALUES
        (1, 'KhitoGlebLih'),
        (2, 'Cursed-Crib'),
        (3, 'xXx'),
        (4, 'SUD_LJ3Q3_Projektarbeit_2026'),
        (5, 'GBoyz'),
        (6, 'Bazinga'),
        (7, 'Feenstaub'),
        (8, 'Hard Workers'),
        (9, 'Dummy2Pro'),
        (10, 'Hammer'),
        (11, 'RandomMap');
    ");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DELETE FROM `teams` WHERE `Id` BETWEEN 1 AND 11;");
        }
    }
}
