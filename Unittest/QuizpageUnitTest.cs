using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Schulprojekt.Data;
using Schulprojekt.ProjektLogic;
using Schulprojekt.Services;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Unittest
{
    [TestClass]
    public class QuizpageLogicTest
    {
        private class QuestionServiceStub : IQuestionService
        {
            public List<Question> ReturnList = new();
            public Task<IEnumerable<Question>> GetAllEntriesByQuestionSetIncludingNavigationsAsync(int id)
                => Task.FromResult<IEnumerable<Question>>(ReturnList);

            public Task<IEnumerable<Question>> GetAllEntriesIncludingNavigationsAsync()
                => Task.FromResult<IEnumerable<Question>>(ReturnList);

            public Task<Question> GetEntryByKeyIncludingNavigationsAsync(int key)
                => Task.FromResult(new Question { Id = key});
        }

        private class QuestionSetServiceStub : IQuestionSetService
        {
            public QuestionSet ReturnSet = new QuestionSet();
            public Task<QuestionSet> GetEntryByKeyIncludingNavigationsAsync(int id)
                => Task.FromResult(ReturnSet);

            public Task<IEnumerable<QuestionSet>> GetEntriesByThemaKeyIncludingNavigationsAsync(int themaId)
                => Task.FromResult<IEnumerable<QuestionSet>>(new List<QuestionSet>());

            public Task<IEnumerable<QuestionSet>> GetAllEntriesIncludingNavigationsAsync()
                => Task.FromResult<IEnumerable<QuestionSet>>(new List<QuestionSet>());
        }

        private class ProgressServiceStub : IQuestionSetProgressService
        {
            public int Calls = 0;
            public QuestionSetProgress? LastProgress;

            public Task<QuestionSetProgress> AddEntryAsync(QuestionSetProgress progress)
            {
                Calls++;
                LastProgress = progress;
                return Task.FromResult(progress);
            }

            public Task<IEnumerable<QuestionSetProgress>> GetAllProgressesWithNavigationsAsync()
            {
                return Task.FromResult<IEnumerable<QuestionSetProgress>>(new List<QuestionSetProgress>());
            }

            public Task<IEnumerable<QuestionSetProgress>> GetEntriesByPlayerId(int spielerId)
            {
                return Task.FromResult<IEnumerable<QuestionSetProgress>>(new List<QuestionSetProgress>());
            }
        }

        private QuizpageLogic CreateLogic(
            out QuestionServiceStub qService,
            out QuestionSetServiceStub qsService,
            out ProgressServiceStub pService)
        {
            qService = new QuestionServiceStub();
            qsService = new QuestionSetServiceStub();
            pService = new ProgressServiceStub();

            return new QuizpageLogic(qService, qsService, pService);
        }

        [TestMethod]
        public async Task LoadQuizAsync_LoadsQuestions_AndResetsState()
        {
            var logic = CreateLogic(out var qService, out _, out _);

            qService.ReturnList.Add(new Question { Id = 1 });
            qService.ReturnList.Add(new Question { Id = 2 });

            logic.QuestionSetId = 5;
            await logic.LoadQuizAsync();

            Assert.AreEqual(2, logic.QuestionsList.Count);
            Assert.AreEqual(0, logic.BarValue);
            Assert.AreEqual(0, logic.AnsweredQuestions.Count);
            Assert.AreEqual(0, logic.PointsPerQuestion.Count);
        }

        [TestMethod]
        public void SingleChoice_Correct_AddsPoints_AndMarksAnswered()
        {
            var logic = CreateLogic(out _, out _, out _);

            var q = new Question { Id = 10 };
            var a = new McAnswer { IsCorrect = true, Points = 3 };

            logic.EvaluateSingleChoice(q, a);

            Assert.AreEqual(3, logic.PointsPerQuestion[10]);
            Assert.IsTrue(logic.AnsweredQuestions.Contains(10));
            Assert.AreEqual(1, logic.BarValue);
        }

        [TestMethod]
        public void SingleChoice_Wrong_AddsToWrongQuestions()
        {
            var logic = CreateLogic(out _, out _, out _);

            var q = new Question { Id = 10 };
            var a = new McAnswer { IsCorrect = false };

            logic.EvaluateSingleChoice(q, a);

            Assert.IsTrue(logic.WrongQuestions.Contains(q));
        }

        [TestMethod]
        public void MultipleChoice_AllCorrect_AddsPoints()
        {
            var logic = CreateLogic(out _, out _, out _);

            var q = new Question
            {
                Id = 1,
                McAnswers = new List<McAnswer>
                {
                    new McAnswer { Id = 1, IsCorrect = true, Points = 2 },
                    new McAnswer { Id = 2, IsCorrect = true, Points = 3 }
                }
            };

            logic.EvaluateMultipleChoice(q, q.McAnswers);

            Assert.AreEqual(5, logic.PointsPerQuestion[1]);
            Assert.IsFalse(logic.WrongQuestions.Contains(q));
        }

        [TestMethod]
        public void MultipleChoice_WrongAnswer_MarksWrong()
        {
            var logic = CreateLogic(out _, out _, out _);

            var q = new Question
            {
                Id = 1,
                McAnswers = new List<McAnswer>
                {
                    new McAnswer { Id = 1, IsCorrect = true, Points = 2 },
                    new McAnswer { Id = 2, IsCorrect = false, Points = -1 }
                }
            };

            logic.EvaluateMultipleChoice(q, new[] { ((List<McAnswer>)q.McAnswers)[1] });

            Assert.IsTrue(logic.WrongQuestions.Contains(q));
        }

        [TestMethod]
        public void MultipleChoice_UnselectingCorrect_RevertsPoints()
        {
            var logic = CreateLogic(out _, out _, out _);

            var correct = new McAnswer { Id = 1, IsCorrect = true, Points = 2 };
            var q = new Question
            {
                Id = 1,
                McAnswers = new List<McAnswer> { correct }
            };

            logic.EvaluateMultipleChoice(q, new[] { correct });
            logic.EvaluateMultipleChoice(q, new McAnswer[] { });

            Assert.AreEqual(0, logic.PointsPerQuestion[1]);
        }

        [TestMethod]
        public void Gap_Correct_AddsPoint()
        {
            var logic = CreateLogic(out _, out _, out _);

            var q = new Question
            {
                Id = 1,
                GapFields = new List<GapField>
                {
                    new GapField { GapIndex = 0, CorrectText = "Test", CaseSensitive = false }
                }
            };

            logic.EvaluateGap(q, 0, "test");

            Assert.AreEqual(1, logic.PointsPerQuestion[1]);
            Assert.IsFalse(logic.WrongQuestions.Contains(q));
        }

        [TestMethod]
        public void Gap_Wrong_MarksWrong()
        {
            var logic = CreateLogic(out _, out _, out _);

            var q = new Question
            {
                Id = 1,
                GapFields = new List<GapField>
                {
                    new GapField { GapIndex = 0, CorrectText = "Test" }
                }
            };

            logic.EvaluateGap(q, 0, "wrong");

            Assert.IsTrue(logic.WrongQuestions.Contains(q));
        }

        [TestMethod]
        public void Gap_Empty_RemovesAnsweredState()
        {
            var logic = CreateLogic(out _, out _, out _);

            var q = new Question
            {
                Id = 1,
                GapFields = new List<GapField>
                {
                    new GapField { GapIndex = 0, CorrectText = "A" }
                }
            };

            logic.EvaluateGap(q, 0, "A");
            logic.EvaluateGap(q, 0, "");

            Assert.IsFalse(logic.AnsweredQuestions.Contains(1));
            Assert.AreEqual(0, logic.BarValue);
        }

        [TestMethod]
        public void AnsweredQuestions_IncrementsBarValue_OnlyOnce()
        {
            var logic = CreateLogic(out _, out _, out _);

            var q = new Question { Id = 1 };
            var a = new McAnswer { IsCorrect = true, Points = 1 };

            logic.EvaluateSingleChoice(q, a);
            logic.EvaluateSingleChoice(q, a);

            Assert.AreEqual(1, logic.BarValue);
        }

        [TestMethod]
        public void WrongQuestions_Removed_WhenCorrected()
        {
            var logic = CreateLogic(out _, out _, out _);

            var q = new Question { Id = 1 };
            var wrong = new McAnswer { IsCorrect = false };
            var correct = new McAnswer { IsCorrect = true, Points = 1 };

            logic.EvaluateSingleChoice(q, wrong);
            logic.EvaluateSingleChoice(q, correct);

            Assert.IsFalse(logic.WrongQuestions.Contains(q));
        }

        [TestMethod]
        public async Task ReloadWrongQuestions_ResetsState_AndLoadsOnlyWrong()
        {
            var logic = CreateLogic(out var qService, out _, out _);

            var q1 = new Question { Id = 1 };
            var q2 = new Question { Id = 2 };

            // Provide questions via stub
            qService.ReturnList.Add(q1);
            qService.ReturnList.Add(q2);

            logic.QuestionSetId = 1;

            // Load Quiz -> correctly filled QuestionsList
            await logic.LoadQuizAsync();

            // q2 wrong mark
            logic.ReloadWrongQuestions();
            logic.WrongQuestions.Add(q2);

            // Test method
            logic.ReloadWrongQuestions();
            logic.QuestionsList.Add(q2);

            Assert.AreEqual(1, logic.QuestionsList.Count);
            Assert.AreEqual(2, logic.QuestionsList[0].Id);
            Assert.AreEqual(0, logic.BarValue);
            Assert.AreEqual(0, logic.PointsPerQuestion.Count);
        }

        [TestMethod]
        public async Task FinishQuizAsync_SavesProgress_WhenPassed()
        {
            var logic = CreateLogic(out _, out var qsService, out var pService);

            qsService.ReturnSet = new QuestionSet
            {
                Questions = new List<Question>
                {
                    new Question
                    {
                        McAnswers = new List<McAnswer>
                        {
                            new McAnswer { IsCorrect = true, Points = 2 }
                        },
                        GapFields = new List<GapField>()
                    }
                }
            };

            logic.PointsPerQuestion[1] = 2;
            logic.PreviousQuizPoints = 0;

            var result = await logic.FinishQuizAsync();

            Assert.IsTrue(result.Passed);
            Assert.AreEqual(1, pService.Calls);
        }

        [TestMethod]
        public async Task FinishQuizAsync_NoPoints_DoesNotSaveProgress()
        {
            var logic = CreateLogic(out _, out var qsService, out var pService);

            qsService.ReturnSet = new QuestionSet
            {
                Questions = new List<Question>
                {
                    new Question
                    {
                        McAnswers = new List<McAnswer>(),
                        GapFields = new List<GapField>()
                    }
                }
            };

            var result = await logic.FinishQuizAsync();

            Assert.IsFalse(result.Passed);
            Assert.AreEqual(0, pService.Calls);
        }
    }
}

