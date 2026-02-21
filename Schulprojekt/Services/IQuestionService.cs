using Schulprojekt.Data;

namespace Schulprojekt.Services
{
    public interface IQuestionService
    {
        Task<IEnumerable<Question>> GetAllEntriesIncludingNavigationsAsync();
        Task<IEnumerable<Question>> GetAllEntriesByQuestionSetIncludingNavigationsAsync(int questionSetID);
    }
}
