using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using Schulprojekt.Data;

namespace Schulprojekt.Services
{
    public class QuestionSetProgressService : IQuestionSetProgressService
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
        public QuestionSetProgressService(IDbContextFactory<ApplicationDbContext> contextFactory)
        {
            _contextFactory = contextFactory;
        }

        /// <summary>
        /// Default constructor required for mocking
        /// </summary>
        public QuestionSetProgressService() { }

        public async Task<IEnumerable<QuestionSetProgress>> GetAllProgressesWithNavigationsAsync()
        {
            try
            {
                using var dbContext = await _contextFactory.CreateDbContextAsync();

                return await dbContext.QuestionSetProgresses
                                        .ToListAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<QuestionSetProgress> AddEntryAsync(QuestionSetProgress item)
        {
            using var dbContext = await _contextFactory.CreateDbContextAsync();

            var entity = await dbContext.QuestionSetProgresses.AddAsync(item);
            await dbContext.SaveChangesAsync();

            return entity.Entity;
        }

        public virtual async Task<IEnumerable<QuestionSetProgress>> GetEntriesByPlayerId(int playerId)
        {
            try
            {
                using var dbContext = await _contextFactory.CreateDbContextAsync();

                return await dbContext.QuestionSetProgresses
                                        .Where(x => x.SpielerId == playerId)
                                        .Include(x => x.Character)
                                        .ToListAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
