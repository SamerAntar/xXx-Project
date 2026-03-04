using Microsoft.EntityFrameworkCore;
using Schulprojekt.Data;

namespace Schulprojekt.Services
{
    public class SpielerService : ISpielerService
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
        public SpielerService(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        /// <summary>
        /// Default constructor required for mocking
        /// </summary>
        public SpielerService() { }

        public virtual async Task<IEnumerable<Spieler>> GetAllPlayers()
        {
            try
            {
                return await dbContext.Players
                    .ToListAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
