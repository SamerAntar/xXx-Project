using Microsoft.EntityFrameworkCore;
using Schulprojekt.Data;

namespace Schulprojekt.Services
{
    public class QuestionService : IQuestionService
    {
        /// <summary>
        /// Reference to the dbContext in the ContextPage.
        /// Reference is set during construction and is readonly.
        /// </summary>
        private readonly ApplicationDbContext dbContext;

        /// <summary>
        /// Constructor of the service.
        /// Should only be instantiated in the ContextPage.
        /// </summary>
        /// <param name="dbContext">A reference to the private dbContext in the ContextPage.</param>
        public QuestionService(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        /// <summary>
        /// Default constructor required for mocking
        /// </summary>
        public QuestionService() { }

        public virtual async Task<IEnumerable<Question>> GetAllEntriesIncludingNavigationsAsync()
        {
            try
            {
                return await dbContext.Questions
                    .Include(x => x.McAnswers)
                    .Include(x => x.QuestionSet)
                    .Include(x => x.Themes)
                    .Include(x => x.GapFields)
                       .ThenInclude(g => g.GapOptions)
                    .ToListAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public virtual async Task<IEnumerable<Question>> GetAllEntriesByQuestionSetIncludingNavigationsAsync(int questionSetID)
        {
            try
            {
                return await dbContext.Questions
                    .Include(x => x.McAnswers)
                    .Include(x => x.QuestionSet)
                    .Include(x => x.Themes)
                    .Include(x => x.GapFields)
                       .ThenInclude(g => g.GapOptions)
                    .Where(x => x.QuestionSetId == questionSetID)
                    .ToListAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
