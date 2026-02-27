using Microsoft.EntityFrameworkCore;
using Schulprojekt.Data;

namespace Schulprojekt.Services
{
    // Service zur Verwaltung und Abfrage von QuestionSets aus der Datenbank
    public class QuestionSetService : IQuestionSetService
    {
        // Referenz auf den Datenbankkontext für den Zugriff auf die QuestionSets
        private readonly ApplicationDbContext dbContext;

        // Konstruktor mit Übergabe des DbContext (Dependency Injection)
        public QuestionSetService(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        // Parameterloser Konstruktor (z.B. für Tests oder Mocking)
        public QuestionSetService() { }

        // Lädt alle QuestionSets inklusive ihrer verknüpften Fragen und Teams
        public virtual async Task<IEnumerable<QuestionSet?>> GetAllEntriesIncludingNavigationsAsync()
        {
            try
            {
                return await dbContext.QuestionSets
                    .Include(x => x.Questions) // Zugehörige Fragen
                    .Include(x => x.Team)      // Zugehöriges Team
                    .ToListAsync();
            }
            catch (Exception)
            {
                throw; // Gibt die Exception unverändert weiter
            }
        }

        // Lädt ein bestimmtes QuestionSet anhand seiner ID inklusive Navigationseigenschaften
        public virtual async Task<QuestionSet?> GetEntryByKeyIncludingNavigationsAsync(int key)
        {
            try
            {
                return await dbContext.QuestionSets
                    .Include(x => x.Questions)
                    .Include(x => x.Team)
                    .FirstOrDefaultAsync(x => x.Id == key); // Sucht nach passender ID
            }
            catch (Exception)
            {
                throw; // Gibt die Exception unverändert weiter
            }
        }
    }
}