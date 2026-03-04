using Schulprojekt.Data;

namespace Schulprojekt.Services
{
    public interface IThemaService
    {
        Task<IEnumerable<Thema>> GetAllEntriesIncludingNavigationsAsync();
    }
}
