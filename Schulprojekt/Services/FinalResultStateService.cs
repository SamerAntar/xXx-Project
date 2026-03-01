using Schulprojekt.Data;
using static Schulprojekt.Components.Pages.FinalResults;

namespace Schulprojekt.Services
{
    public class FinalResultStateService
    {
        public List<TopicResult> TopicResults { get; set; }
        public List<QuestionSet> QuestionSets { get; set; }
        public double? Percent {  get; set; }
    }
}
