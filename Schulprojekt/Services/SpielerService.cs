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
        private readonly IDbContextFactory<ApplicationDbContext> _contextFactory;
        
        /// <summary>
        /// Constructor of the service.
        /// Should only be instantiated in the ContextPage.
        /// </summary>
        /// <param name="dbContext">A reference to the private dbContext in the ContextPage.</param>
        public SpielerService(IDbContextFactory<ApplicationDbContext> contextFactory)
        {
            _contextFactory = contextFactory;
        }

        /// <summary>
        /// Default constructor required for mocking
        /// </summary>
        public SpielerService() { }

        public virtual async Task<IEnumerable<Spieler>> GetAllPlayers()
        {
            try
            {
                using var dbContext = await _contextFactory.CreateDbContextAsync();

                return await dbContext.Players
                    .ToListAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public virtual async Task<Spieler?> AddOrUpdateAsync(Spieler item)
        {
            try
            {
                using var dbContext = await _contextFactory.CreateDbContextAsync();

                // Prüfen, ob der Spielername schon existiert
                var existing = await dbContext.Players
                                              .FirstOrDefaultAsync(s => s.Name == item.Name);

                if (existing != null)
                {
                    // Existierender Spieler → Daten ersetzen / updaten
                    existing.Name = item.Name; // ggf. weitere Felder updaten
                    dbContext.Players.Update(existing);
                    await dbContext.SaveChangesAsync();
                    return existing;
                }
                else
                {
                    // Neuer Spieler → hinzufügen
                    var addedEntry = await dbContext.Players.AddAsync(item);
                    await dbContext.SaveChangesAsync();
                    return addedEntry.Entity;
                }
            }
            catch
            {
                throw;
            }
        }
    }
}
