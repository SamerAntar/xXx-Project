using Microsoft.EntityFrameworkCore;
using Schulprojekt.Data;

namespace Schulprojekt.Services
{
    public class QuestionSetService : IQuestionSetService
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
        public QuestionSetService(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        /// <summary>
        /// Default constructor required for mocking
        /// </summary>
        public QuestionSetService() { }

        public virtual async Task<IEnumerable<QuestionSet?>> GetAllEntriesIncludingNavigationsAsync()
        {
            try
            {
                return await dbContext.QuestionSets
                    .Include(x => x.Questions)
                    .Include(x => x.Team)
                    .ToListAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public virtual async Task<QuestionSet?> GetEntryByKeyIncludingNavigationsAsync(int key)
        {
            try
            {
                return await dbContext.QuestionSets
                    .Include(x => x.Questions)
                    .Include(x => x.Team)
                .FirstOrDefaultAsync(x => x.Id == key);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public virtual async Task<IEnumerable<QuestionSet>> GetEntriesByThemaKeyIncludingNavigationsAsync(int themaId)
        {
            try
            {
                return await dbContext.QuestionSets
                    .Include(x => x.Questions)
                    .Include(x => x.Team)
                    .Include(x => x.Thema)
                    .Where(x => x.TeamId == themaId)
                    .ToListAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
