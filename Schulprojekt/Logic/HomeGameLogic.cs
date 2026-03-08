using Schulprojekt.Data;

namespace Schulprojekt.Logic
{
    public static class HomeGameLogic
    {
        public static bool IsPlayerMissing(string? playerName)
            => string.IsNullOrWhiteSpace(playerName);

        public static bool IsQuestionSetMissing(QuestionSet? selectedQuestionSet)
            => selectedQuestionSet == null || string.IsNullOrWhiteSpace(selectedQuestionSet.Title);

        public static bool IsThemeMissing(Thema? selectedThema)
            => selectedThema == null || string.IsNullOrWhiteSpace(selectedThema.Name);


        // @page "/Quiz/{SpielerId:int}/{QuestionSetId:int}"
        public static string BuildQuizRoute(int spielerId, int questionSetId)
            => $"/Quiz/{spielerId}/{questionSetId}";
    }
}
