using Microsoft.EntityFrameworkCore;
using Schulprojekt.Data;

namespace Schulprojekt.Services
{
    /// <summary>
    /// Service zur Datenabfrage von Questions über den DbContext.
    /// </summary>
    public class QuestionService : IQuestionService
    {
        /// <summary>
        /// Referenz auf den ApplicationDbContext.
        /// </summary>
        private readonly ApplicationDbContext dbContext;

        /// <summary>
        /// Konstruktor mit Übergabe des DbContext (Dependency Injection).
        /// </summary>
        public QuestionService(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        /// <summary>
        /// Parameterloser Konstruktor für Tests/Mocking.
        /// </summary>
        public QuestionService() { }

        /// <summary>
        /// Gibt alle Questions inklusive zugehöriger Navigationsdaten zurück.
        /// </summary>
        public virtual async Task<IEnumerable<Question>> GetAllEntriesIncludingNavigationsAsync()
        {
            return await dbContext.Questions
                .Include(x => x.McAnswers)
                .Include(x => x.QuestionSet)
                .Include(x => x.Themes)
                .Include(x => x.GapFields)
                .ToListAsync();
        }

        /// <summary>
        /// Gibt alle Questions eines bestimmten QuestionSets inklusive Navigationsdaten zurück.
        /// </summary>
        public virtual async Task<IEnumerable<Question>> GetAllEntriesByQuestionSetIncludingNavigationsAsync(int questionSetID)
        {
            return await dbContext.Questions
                .Include(x => x.McAnswers)
                .Include(x => x.QuestionSet)
                .Include(x => x.Themes)
                .Include(x => x.GapFields)
                .Where(x => x.QuestionSet.Id == questionSetID)
                .ToListAsync();
        }
    }
}