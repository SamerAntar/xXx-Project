using Schulprojekt.Data;

namespace Schulprojekt.Services
{
    public interface ISpielerService
    {
        Task<List<Spieler>> GetAllPlayers();
    }
}
