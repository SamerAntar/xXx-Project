namespace Schulprojekt.Services
{
    public class QuizStateService
    {
        public HashSet<int> RemainingQuizIds { get; set; } = new();
        public bool IsInitialized { get; set; } = false;
        public int? QuizPoints { get; set; } = 0;

        public void Reset()
        {
            RemainingQuizIds.Clear();
            IsInitialized = false;
            QuizPoints = 0;
        }
    }
}
