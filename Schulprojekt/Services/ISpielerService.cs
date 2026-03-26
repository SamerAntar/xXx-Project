using Schulprojekt.Data;

namespace Schulprojekt.Services
{
    public interface ISpielerService
    {
        Task<IEnumerable<Spieler>> GetAllPlayers();
        Task <Spieler?> GetPlayerByIdAsync(int id);
        Task<Spieler?> AddOrUpdateAsync(Spieler item);
    }
}
