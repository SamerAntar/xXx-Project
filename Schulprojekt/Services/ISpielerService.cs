using Schulprojekt.Data;

namespace Schulprojekt.Services
{
    /// <summary>
    /// Service zum Zugriff auf Spieler-Daten.
    /// </summary>
    public interface ISpielerService
    {
        /// <summary>
        /// Gibt alle Spieler zurück.
        /// </summary>
        Task<List<Spieler>> GetAllPlayers();
    }
}