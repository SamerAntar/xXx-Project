using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using Schulprojekt.Data;

namespace Schulprojekt.Services
{
    public class QuestionSetService : IQuestionSetService
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
        public QuestionSetService(IDbContextFactory<ApplicationDbContext> contextFactory)
        {
            _contextFactory = contextFactory;
        }

        /// <summary>
        /// Default constructor required for mocking
        /// </summary>
        public QuestionSetService() { }

        public virtual async Task<IEnumerable<QuestionSet>> GetAllEntriesIncludingNavigationsAsync()
        {
            try
            {
                using var dbContext = await _contextFactory.CreateDbContextAsync();

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
                using var dbContext = await _contextFactory.CreateDbContextAsync();

                return await dbContext.QuestionSets
                    .Include(q => q.Questions)
                        .ThenInclude(q => q.McAnswers)
                    .Include(q => q.Questions)
                        .ThenInclude(q => q.GapFields)
                            .ThenInclude(g => g.GapOptions)
                    .Include(q => q.Team)
                    .Include(q => q.Thema)
                    .FirstOrDefaultAsync(q => q.Id == key);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public virtual async Task<IEnumerable<QuestionSet>> GetEntriesByThemaKeyIncludingNavigationsAsync(int themaId)
        {
            try
            {
                using var dbContext = await _contextFactory.CreateDbContextAsync();

                return await dbContext.QuestionSets
                    .Include(qs => qs.Questions)
                        .ThenInclude(q => q.McAnswers)
                    .Include(qs => qs.Questions)
                        .ThenInclude(q => q.GapFields)
                            .ThenInclude(gf => gf.GapOptions)
                        .Include(x => x.Team)
                    .Include(x => x.Thema)
                    .Where(x => x.ThemaId == themaId)
                    .ToListAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
