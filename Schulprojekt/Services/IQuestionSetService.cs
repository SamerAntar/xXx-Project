using Schulprojekt.Data;

namespace Schulprojekt.Services
{
    /// <summary>
    /// Service zum Zugriff auf QuestionSet-Daten.
    /// </summary>
    public interface IQuestionSetService
    {
        /// <summary>
        /// Gibt alle QuestionSets inklusive ihrer Navigationsdaten zurück.
        /// </summary>
        Task<IEnumerable<QuestionSet>> GetAllEntriesIncludingNavigationsAsync();

        /// <summary>
        /// Gibt ein QuestionSet anhand seines Schlüssels inklusive Navigationsdaten zurück.
        /// </summary>
        Task<QuestionSet> GetEntryByKeyIncludingNavigationsAsync(int key);
    }
}