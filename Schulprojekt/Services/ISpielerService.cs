using Schulprojekt.Data;

namespace Schulprojekt.Services
{
    // Interface definiert die Funktionen zur Verwaltung von Spielern
    public interface ISpielerService
    {
        // Lädt alle Spieler asynchron und gibt sie als Liste zurück
        Task<List<Spieler>> GetAllPlayers();
    }
}