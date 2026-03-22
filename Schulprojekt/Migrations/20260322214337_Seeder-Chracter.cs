using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Schulprojekt.Migrations
{
    /// <inheritdoc />
    public partial class SeederChracter : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
            INSERT INTO `character`
            (`CharacterID`, `Name`, `GettingByCompletingTheme`, `Backstory`, `NormalEndText`, `ProfiEndText`, `TopEndText`)
            VALUES
            (1,
            'Gon Disk',
            0,
            'Gon hat einen ganz großen Traum: Er will die IHKunter Lizenz erwerben, um seinen Vater, einen legendären IHKunter und Programmierer, zu finden! Hilf Gon dabei, seine IHKunter Lizenz zu bekommen und dein Wissen unter Beweis zu stellen!',
            'Dank dir hat es Gon geschafft, seine eigene IHKunter Lizenz zu erwerben! Doch während der Prüfung hat er gemerkt, dass er noch viel zum Lernen hat, bevor er auf die Suche nach seinem Vater aufbricht. Vielleicht solltest du das auch tun…',
            'Mit euren guten Kenntnissen habt ihr es geschafft, sogar eine Profi-IHKunter Lizenz zu erwerben! Nun fühlt sich Gon bereit, sein Vater zu suchen, auch wenn vorher etwas Training vonnöten ist. Da geht doch bestimmt noch mehr…',
            'Wow, eine Top-IHKunter-Lizenz! Gon hätte es nie erwartet, so weit zu bringen. Nun fühlt er sich nicht nur bereit, seinen Vater zu suchen, er weiß sogar, wo er bei der Suche anfangen soll. Und das alles dank deines perfekten Wissens!');
        ");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DELETE FROM `character` WHERE `CharacterID` = 1;");
        }
    }
}
