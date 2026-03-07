using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using Schulprojekt.Data;

namespace Schulprojekt.Services
{
    public class QuestionService : IQuestionService
    {
        /// <summary>
        /// Reference to the dbContext in the ContextPage.
        /// Reference is set during construction and is readonly.
        /// </summary>
        private readonly IDbContextFactory<ApplicationDbContext> _contextFactory;

        /// <summary>
        /// Constructor of the service.
        /// Should only be instantiated in the ContextPage.
        /// </summary>
        /// <param name="dbContext">A reference to the private dbContext in the ContextPage.</param>
        public QuestionService(IDbContextFactory<ApplicationDbContext> contextFactory)
        {
            _contextFactory = contextFactory;
        }

        /// <summary>
        /// Default constructor required for mocking
        /// </summary>
        public QuestionService() { }

        public virtual async Task<IEnumerable<Question>> GetAllEntriesIncludingNavigationsAsync()
        {
            try
            {
                using var dbContext = await _contextFactory.CreateDbContextAsync();

                return await dbContext.Questions
                    .Include(x => x.McAnswers)
                    .Include(x => x.QuestionSet)
                    .Include(x => x.GapFields)
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
                using var dbContext = await _contextFactory.CreateDbContextAsync();

                return await dbContext.Questions
                    .Include(x => x.McAnswers)
                    .Include(x => x.QuestionSet)
                    .Include(x => x.GapFields)
                    .Where(x => x.QuestionSet.Id == questionSetID)
                    .ToListAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
