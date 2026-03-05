using Schulprojekt.Data;

namespace Schulprojekt.Services
{
    public interface IQuestionSetService
    {
        Task<IEnumerable<QuestionSet>> GetAllEntriesIncludingNavigationsAsync();
        Task<QuestionSet> GetEntryByKeyIncludingNavigationsAsync(int key);
        Task<IEnumerable<QuestionSet>> GetEntriesByThemaKeyIncludingNavigationsAsync(int themaId);
    }
}
