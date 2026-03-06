using Schulprojekt.Data;

namespace Schulprojekt.Services
{
    public interface IQuestionSetProgressService
    {
        Task<IEnumerable<QuestionSetProgress>> GetAllProgressesWithNavigationsAsync();
        Task<QuestionSetProgress> AddEntryAsync(QuestionSetProgress entry);
        Task<IEnumerable<QuestionSetProgress>> GetEntriesByPlayerId(int playerId);
    }
}




