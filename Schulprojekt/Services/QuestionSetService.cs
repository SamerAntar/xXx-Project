using Microsoft.EntityFrameworkCore;
using Schulprojekt.Data;

namespace Schulprojekt.Services
{
    /// <summary>
    /// Service zur Datenabfrage von QuestionSets über den DbContext.
    /// </summary>
    public class QuestionSetService : IQuestionSetService
    {
        /// <summary>
        /// Referenz auf den ApplicationDbContext.
        /// </summary>
        private readonly ApplicationDbContext dbContext;

        /// <summary>
        /// Konstruktor mit Übergabe des DbContext (Dependency Injection).
        /// </summary>
        public QuestionSetService(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        /// <summary>
        /// Parameterloser Konstruktor für Tests/Mocking.
        /// </summary>
        public QuestionSetService() { }

        /// <summary>
        /// Gibt alle QuestionSets inklusive Questions und Team zurück.
        /// </summary>
        public virtual async Task<IEnumerable<QuestionSet?>> GetAllEntriesIncludingNavigationsAsync()
        {
            return await dbContext.QuestionSets
                .Include(x => x.Questions)
                .Include(x => x.Team)
                .ToListAsync();
        }

        /// <summary>
        /// Gibt ein QuestionSet anhand der Id inklusive Questions und Team zurück.
        /// </summary>
        public virtual async Task<QuestionSet?> GetEntryByKeyIncludingNavigationsAsync(int key)
        {
            return await dbContext.QuestionSets
                .Include(x => x.Questions)
                .Include(x => x.Team)
                .FirstOrDefaultAsync(x => x.Id == key);
        }
    }
}