using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using Schulprojekt.Data;

namespace Schulprojekt.Services
{
    public class ThemaService : IThemaService
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
        public ThemaService(IDbContextFactory<ApplicationDbContext> contextFactory)
        {
            _contextFactory = contextFactory;
        }

        /// <summary>
        /// Default constructor required for mocking
        /// </summary>
        public ThemaService() { }
        public virtual async Task<IEnumerable<Thema>> GetAllEntriesIncludingNavigationsAsync()
        {
            try
            {
                using var dbContext = await _contextFactory.CreateDbContextAsync();

                return await dbContext.Themen
                    .Include(x => x.QuestionSets)
                    .ToListAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
