using Schulprojekt.Data;
using Schulprojekt.Services;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Schulprojekt.ProjektLogic
{
    public class QuizpageResult
    {
        public bool Ok { get; set; }
        public string? Message { get; set; }
        public int PlayerPoints { get; set; }
        public int CollectedPoints { get; set; }
        public bool Passed { get; set; }
    }

    public class QuizpageLogic
    {
        private readonly IQuestionService _questionService;
        private readonly IQuestionSetService _questionSetService;
        private readonly IQuestionSetProgressService _progressService;

        // Parameters
        public int SpielerId { get; set; }
        public int ThemaId { get; set; }
        public int CharacterId { get; set; }
        public int QuestionSetId { get; set; }

        // State
        public List<Question> QuestionsList { get; private set; } = new();
        public List<Question> WrongQuestions { get; private set; } = new();

        public Dictionary<int, int> PointsPerQuestion { get; private set; } = new();
        public Dictionary<(int QuestionId, int GapIndex), string> FilledAnswersInGaps { get; private set; } = new();
        public Dictionary<(int QuestionId, int GapIndex), bool> GapCorrectState { get; private set; } = new();

        public HashSet<int> AnsweredQuestions { get; private set; } = new();
        public HashSet<int> PreviousSelectedMCAnswers { get; private set; } = new();

        public int BarValue { get; private set; }
        public int PreviousQuizPoints { get; set; }
        public int PlayerPoints { get; private set; }

        public QuizpageLogic(
            IQuestionService questionService,
            IQuestionSetService questionSetService,
            IQuestionSetProgressService progressService)
        {
            _questionService = questionService;
            _questionSetService = questionSetService;
            _progressService = progressService;
        }

        // ============================================================
        // Load Quiz
        // ============================================================
        public async Task LoadQuizAsync()
        {
            QuestionsList = (await _questionService
                .GetAllEntriesByQuestionSetIncludingNavigationsAsync(QuestionSetId))
                .ToList();

            ResetState();
        }

        private void ResetState()
        {
            WrongQuestions.Clear();
            PointsPerQuestion.Clear();
            FilledAnswersInGaps.Clear();
            GapCorrectState.Clear();
            PreviousSelectedMCAnswers.Clear();
            AnsweredQuestions.Clear();
            BarValue = 0;
            PlayerPoints = 0;
        }

        // ============================================================
        // Single Choice
        // ============================================================
        public void EvaluateSingleChoice(Question question, McAnswer answer)
        {
            if (answer == null)
                return;

            if (answer.IsCorrect)
            {
                AddPoints(question.Id, answer.Points);
                WrongQuestions.Remove(question);
            }
            else
            {
                if (!WrongQuestions.Contains(question))
                    WrongQuestions.Add(question);
            }

            MarkQuestionAnswered(question.Id);
        }

        // ============================================================
        // Multiple Choice
        // ============================================================
        public void EvaluateMultipleChoice(Question question, IEnumerable<McAnswer> selected)
        {
            int points = 0;
            bool hasWrong = false;

            foreach (var option in question.McAnswers)
            {
                bool isSelected = selected.Any(x => x.Id == option.Id);
                bool wasSelectedBefore = PreviousSelectedMCAnswers.Contains(option.Id);

                // Newly selected
                if (isSelected && !wasSelectedBefore)
                {
                    PreviousSelectedMCAnswers.Add(option.Id);

                    if (option.IsCorrect)
                        points += option.Points;
                    else
                    {
                        points -= option.Points;
                        hasWrong = true;
                    }
                }

                // Unselected now
                if (!isSelected && wasSelectedBefore)
                {
                    PreviousSelectedMCAnswers.Remove(option.Id);

                    if (option.IsCorrect)
                    {
                        points -= option.Points;
                        hasWrong = true;
                    }
                    else
                        points += option.Points;
                }

                // Missed correct answer
                if (!isSelected && option.IsCorrect)
                    hasWrong = true;

                // Selected wrong answer
                if (isSelected && !option.IsCorrect)
                    hasWrong = true;
            }

            AddPoints(question.Id, points);

            if (hasWrong)
                WrongQuestions.Add(question);
            else
                WrongQuestions.Remove(question);

            MarkQuestionAnswered(question.Id);
        }

        // ============================================================
        // Gap Text
        // ============================================================
        public void EvaluateGap(Question question, int gapIndex, string value)
        {
            FilledAnswersInGaps[(question.Id, gapIndex)] = value;

            bool isEmpty = string.IsNullOrWhiteSpace(value);

            if (isEmpty)
            {
                if (GapCorrectState.ContainsKey((question.Id, gapIndex)))
                {
                    if (GapCorrectState[(question.Id, gapIndex)] == true)
                        AddPoints(question.Id, -1);

                    GapCorrectState.Remove((question.Id, gapIndex));
                }

                if (AnsweredQuestions.Contains(question.Id))
                {
                    BarValue--;
                    AnsweredQuestions.Remove(question.Id);
                }

                return;
            }

            var gap = question.GapFields.FirstOrDefault(x => x.GapIndex == gapIndex);
            if (gap == null)
                return;

            bool correct =
                gap.CorrectText.Equals(value,
                    gap.CaseSensitive
                        ? System.StringComparison.Ordinal
                        : System.StringComparison.OrdinalIgnoreCase);

            if (correct)
            {
                if (!GapCorrectState.ContainsKey((question.Id, gapIndex)))
                    AddPoints(question.Id, 1);

                GapCorrectState[(question.Id, gapIndex)] = true;

                if (AllGapsCorrect(question))
                    WrongQuestions.Remove(question);
            }
            else
            {
                if (GapCorrectState.ContainsKey((question.Id, gapIndex)) &&
                    GapCorrectState[(question.Id, gapIndex)] == true)
                {
                    AddPoints(question.Id, -1);
                }

                GapCorrectState[(question.Id, gapIndex)] = false;

                if (!WrongQuestions.Contains(question))
                    WrongQuestions.Add(question);
            }

            if (!AnsweredQuestions.Contains(question.Id) &&
                AllGapsFilled(question))
            {
                MarkQuestionAnswered(question.Id);
            }
        }

        private bool AllGapsFilled(Question q)
            => q.GapFields.All(g => FilledAnswersInGaps.ContainsKey((q.Id, g.GapIndex)));

        private bool AllGapsCorrect(Question q)
            => q.GapFields.All(g => GapCorrectState.TryGetValue((q.Id, g.GapIndex), out bool ok) && ok);

        // ============================================================
        // Wrong Questions Reload
        // ============================================================
        public void ReloadWrongQuestions()
        {
            if (!WrongQuestions.Any())
                return;

            ResetState();

            QuestionsList = WrongQuestions.ToList();
            WrongQuestions.Clear();
        }

        // ============================================================
        // Finish Quiz
        // ============================================================
        public async Task<QuizpageResult> FinishQuizAsync()
        {
            var result = new QuizpageResult();

            PlayerPoints = PointsPerQuestion.Values.Where(v => v > 0).Sum();
            int collected = PlayerPoints - PreviousQuizPoints;

            result.CollectedPoints = collected;
            result.PlayerPoints = PlayerPoints;

            var questionSet = await _questionSetService
                .GetEntryByKeyIncludingNavigationsAsync(QuestionSetId);

            int max = GetMaxPoints(questionSet);
            // bool passed = collected >= (max / 2);
            bool passed = max > 0 && PlayerPoints >= (max / 2);

            result.Passed = passed;
            result.Ok = true;

            if (passed)
            {
                var progress = new QuestionSetProgress
                {
                    SpielerId = SpielerId,
                    ThemaId = ThemaId,
                    CharacterId = CharacterId,
                    QuestionSetId = QuestionSetId,
                    Topic = questionSet.Title,
                    Points = collected,
                    MaxPoints = max,
                    IsPassed = true
                };

                await _progressService.AddEntryAsync(progress);
            }

            return result;
        }

        private int GetMaxPoints(QuestionSet set)
        {
            int max = 0;

            foreach (var q in set.Questions)
            {
                max += q.McAnswers.Where(a => a.IsCorrect).Sum(a => a.Points);
                max += q.GapFields.Count();
                // max += q.GapFields.Where(a => a.Equals(a.CorrectText)).Sum(a => a.GapIndex);
            }

            return max;
        }

        private void AddPoints(int questionId, int value)
        {
            if (!PointsPerQuestion.ContainsKey(questionId))
                PointsPerQuestion[questionId] = 0;

            PointsPerQuestion[questionId] += value;
        }

        private void MarkQuestionAnswered(int questionId)
        {
            if (!AnsweredQuestions.Contains(questionId))
            {
                AnsweredQuestions.Add(questionId);
                BarValue++;
            }
        }
    }
}

