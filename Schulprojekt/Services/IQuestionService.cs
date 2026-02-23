using Schulprojekt.Data;

namespace Schulprojekt.Services
{
    // Interface für das Lesen von Questions inkl. Navigationseigenschaften
    public interface IQuestionService
    {
        // Lädt alle Questions inkl. verknüpfter Daten (z. B. Answers)
        Task<IEnumerable<Question>> GetAllEntriesIncludingNavigationsAsync();

        // Lädt alle Questions eines bestimmten QuestionSets inkl. Navigationen
        Task<IEnumerable<Question>> GetAllEntriesByQuestionSetIncludingNavigationsAsync(int questionSetID);
    }
}