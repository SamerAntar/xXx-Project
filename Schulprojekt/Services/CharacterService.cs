using Microsoft.EntityFrameworkCore;
using Schulprojekt.Data;

namespace Schulprojekt.Services
{
    public class CharacterService : ICharacterService
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
        public CharacterService(IDbContextFactory<ApplicationDbContext> contextFactory)
        {
            _contextFactory = contextFactory;
        }

        /// <summary>
        /// Default constructor required for mocking
        /// </summary>
        public CharacterService() { }
        public virtual async Task<IEnumerable<Character>> GetAllEntriesAsync()
        {
            try
            {
                using var dbContext = await _contextFactory.CreateDbContextAsync();

                return await dbContext.Character.ToListAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public virtual async Task<Character?> GetEntryByKeyAsync(int key)
        {
            try
            {
                using var dbContext = await _contextFactory.CreateDbContextAsync();

                return await dbContext.Character.FirstOrDefaultAsync(q => q.CharacterID == key);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
