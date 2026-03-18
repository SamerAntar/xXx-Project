using Schulprojekt.Data;

namespace Schulprojekt.Services
{
    public interface ICharacterService
    {
            Task<IEnumerable<Character>> GetAllEntriesAsync();
    }
}
