using Schulprojekt.Data;

namespace Schulprojekt.Services
{
    public class SpielerService : ISpielerService
    {
        // Gibt eine Liste aller Spieler zurück (aktuell statisch definiert)
        public async Task<List<Spieler>> GetAllPlayers()
        {
            List<Spieler> players = new List<Spieler>()
            {
                new Spieler() { Id = 1, Name = "Hisoka" },
                new Spieler() { Id = 2, Name = "Gone" },
                new Spieler() { Id = 3, Name = "Kilwa" }
            };

            return players.ToList(); // Kopie der Liste zurückgeben
        }
    }
}