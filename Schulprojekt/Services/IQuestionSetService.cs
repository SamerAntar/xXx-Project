using Schulprojekt.Data;

namespace Schulprojekt.Services
{
    // Interface zur Bereitstellung von Methoden für QuestionSets
    public interface IQuestionSetService
    {
        // Gibt alle QuestionSets inklusive ihrer verknüpften Navigationseigenschaften zurück
        Task<IEnumerable<QuestionSet>> GetAllEntriesIncludingNavigationsAsync();

        // Gibt ein einzelnes QuestionSet anhand seines Schlüssels zurück,
        // inklusive aller zugehörigen Navigationseigenschaften
        Task<QuestionSet> GetEntryByKeyIncludingNavigationsAsync(int key);
    }
}