using Schulprojekt.Data;

namespace Schulprojekt.Services
{
    public interface IFrageService
    {
        Task<List<Frage>> GetAllQuestions();

        // Refs #10
        Task<List<Frage>> GetLueckentextQuestions();
    }
}
