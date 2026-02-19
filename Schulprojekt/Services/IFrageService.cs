using Schulprojekt.Data;

namespace Schulprojekt.Services
{
    public interface IFrageService
    {
        Task<List<Frage>> GetAllQuestions();

        Task<List<Frage>> GetLueckentextQuestions();
    }
}
