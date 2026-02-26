using Microsoft.EntityFrameworkCore;
using Schulprojekt.Data;

namespace Schulprojekt.Services
{
    // Service zur Verwaltung und Abfrage von Fragen aus der Datenbank
    public class QuestionService : IQuestionService
    {
        // Referenz auf den Datenbankkontext für den Zugriff auf die Datenbank
        private readonly ApplicationDbContext dbContext;

        // Konstruktor mit Übergabe des DbContext (Dependency Injection)
        public QuestionService(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        // Parameterloser Konstruktor (z.B. für Tests oder Mocking)
        public QuestionService() { }

        // Lädt alle Fragen inklusive ihrer Navigationseigenschaften (verknüpfte Daten)
        public virtual async Task<IEnumerable<Question>> GetAllEntriesIncludingNavigationsAsync()
        {
            try
            {
                return await dbContext.Questions
                    .Include(x => x.McAnswers)     // Multiple-Choice-Antworten
                    .Include(x => x.QuestionSet)   // Zugehöriges QuestionSet
                    .Include(x => x.Themes)        // Zugeordnete Themen
                    .Include(x => x.GapFields)     // Lückentext-Felder
                    .ToListAsync();
            }
            catch (Exception)
            {
                throw; // Gibt die Exception unverändert weiter
            }
        }

        // Lädt alle Fragen eines bestimmten QuestionSets inklusive Navigationseigenschaften
        public virtual async Task<IEnumerable<Question>> GetAllEntriesByQuestionSetIncludingNavigationsAsync(int questionSetID)
        {
            try
            {
                return await dbContext.Questions
                    .Include(x => x.McAnswers)
                    .Include(x => x.QuestionSet)
                    .Include(x => x.Themes)
                    .Include(x => x.GapFields)
                    .Where(x => x.QuestionSet.Id == questionSetID) // Filter nach QuestionSet-ID
                    .ToListAsync();
            }
            catch (Exception)
            {
                throw; // Gibt die Exception unverändert weiter
            }
        }
    }
}