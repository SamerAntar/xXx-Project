using Microsoft.EntityFrameworkCore;
using Schulprojekt.Data;

namespace Schulprojekt.Services
{
    public class QuestionSetProgressService : IQuestionSetProgressService
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
        public QuestionSetProgressService(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        /// <summary>
        /// Default constructor required for mocking
        /// </summary>
        public QuestionSetProgressService() { }

        public async Task<IEnumerable<QuestionSetProgress>> GetAllProgressesWithNavigationsAsync()
        {
            try
            {
                return await dbContext.QuestionSetProgresses
                                        .ToListAsync();
            }
            catch(Exception)
            {
                throw;
            }
        }

        public virtual async Task<QuestionSetProgress> AddEntryAsync(QuestionSetProgress item)
        {
            try
            {
                var Entity = await dbContext.QuestionSetProgresses.AddAsync(item);
                dbContext.SaveChanges();

                return Entity.Entity;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
