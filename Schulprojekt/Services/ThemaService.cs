using Microsoft.EntityFrameworkCore;
using Schulprojekt.Data;

namespace Schulprojekt.Services
{
    public class ThemaService : IThemaService
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
        public ThemaService(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        /// <summary>
        /// Default constructor required for mocking
        /// </summary>
        public ThemaService() { }
        public virtual async Task<IEnumerable<Thema>> GetAllEntriesIncludingNavigationsAsync()
        {
            try
            {
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
