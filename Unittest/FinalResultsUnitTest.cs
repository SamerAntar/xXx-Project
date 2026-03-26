using Microsoft.VisualStudio.TestTools.UnitTesting;
using Schulprojekt.Data;
using Schulprojekt.ProjektLogic;
using Schulprojekt.Services;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Unittest
{
    [TestClass]
    public class FinalResultsLogicTest
    {
        // -------------------------------------------
        // PROGRESS SERVICE STUB
        // -------------------------------------------
        private class ProgressStub : IQuestionSetProgressService
        {
            public List<QuestionSetProgress> List = new();

            public Task<IEnumerable<QuestionSetProgress>> GetEntriesByPlayerId(int id)
                => Task.FromResult<IEnumerable<QuestionSetProgress>>(List);

            public Task<IEnumerable<QuestionSetProgress>> GetAllProgressesWithNavigationsAsync()
                => Task.FromResult<IEnumerable<QuestionSetProgress>>(new List<QuestionSetProgress>());

            public Task<QuestionSetProgress> AddEntryAsync(QuestionSetProgress p)
                => Task.FromResult(p);
        }

        // -------------------------------------------
        // QUESTION SET SERVICE STUB
        // -------------------------------------------
        private class QuestionSetStub : IQuestionSetService
        {
            public Task<IEnumerable<QuestionSet>> GetEntriesByThemaKeyIncludingNavigationsAsync(int themaId)
                => Task.FromResult<IEnumerable<QuestionSet>>(new List<QuestionSet>());

            public Task<IEnumerable<QuestionSet>> GetAllEntriesIncludingNavigationsAsync()
                => Task.FromResult<IEnumerable<QuestionSet>>(new List<QuestionSet>());

            public Task<QuestionSet> GetEntryByKeyIncludingNavigationsAsync(int id)
                => Task.FromResult(new QuestionSet());
        }

        // -------------------------------------------
        // CHARACTER SERVICE STUB
        // -------------------------------------------
        private class CharacterStub : ICharacterService
        {
            public Character Ch = new Character
            {
                TopEndText = "TOP",
                ProfiEndText = "PROFI",
                NormalEndText = "NORMAL"
            };

            public Task<Character> GetEntryByKeyAsync(int id)
                => Task.FromResult(Ch);

            // Fehlt in deinem Fehler → hier implementiert
            public Task<IEnumerable<Character>> GetAllEntriesAsync()
                => Task.FromResult<IEnumerable<Character>>(new List<Character>());
        }

        // Factory für neue Logiken
        private FinalResultsLogic CreateLogic(
            out ProgressStub p, out QuestionSetStub qs, out CharacterStub cs)
        {
            p = new ProgressStub();
            qs = new QuestionSetStub();
            cs = new CharacterStub();

            return new FinalResultsLogic(qs, p, cs)
            {
                WebRootPath = "" // wichtig für Tests ohne Filesystem
            };
        }

        // -------------------------------------------
        // TESTS
        // -------------------------------------------

        /// <author>Houman</author>
        /// <summary>
        /// Tests whether total points, max points and percentage are computed correctly.
        /// </summary>
        /// <returns>The executed test result.</returns>
        [TestMethod]
        public async Task LoadAsync_ComputesTotalsCorrectly()
        {
            var logic = CreateLogic(out var p, out _, out _);

            p.List.Add(new QuestionSetProgress { Points = 10, MaxPoints = 20 });
            p.List.Add(new QuestionSetProgress { Points = 20, MaxPoints = 20 });

            await logic.LoadAsync();

            Assert.AreEqual(30, logic.TotalPoints);
            Assert.AreEqual(40, logic.TotalMax);
            Assert.AreEqual(75, logic.Percent);
        }

        /// <author>Houman</author>
        /// <summary>
        /// Tests whether the character is retrieved correctly.
        /// </summary>
        /// <returns>The executed test result.</returns>
        [TestMethod]
        public async Task LoadAsync_LoadsCharacter()
        {
            var logic = CreateLogic(out _, out _, out var cs);

            await logic.LoadAsync();

            Assert.IsNotNull(logic.EndCharacter);
            Assert.AreEqual("TOP", cs.Ch.TopEndText);
        }

        /// <author>Houman</author>
        /// <summary>
        /// Tests whether the fallback image is used when no character image exists.
        /// </summary>
        /// <returns>The executed test result.</returns>
        [TestMethod]
        public async Task LoadEndScreen_UsesFallback_WhenImageMissing()
        {
            var logic = CreateLogic(out _, out _, out _);

            await logic.LoadAsync();

            Assert.AreEqual(
                "images/Endscreen_Normal/Endscreen_0_normal.png",
                logic.EndscreenImage);
        }

        /// <author>Houman</author>
        /// <summary>
        /// Tests whether the motivational text changes depending on the score.
        /// </summary>
        /// <returns>The executed test result.</returns>
        [TestMethod]
        public async Task MotivationText_ChangesWithPercent()
        {
            var logic = CreateLogic(out var p, out _, out _);

            p.List.Add(new QuestionSetProgress { Points = 90, MaxPoints = 100 });

            await logic.LoadAsync();

            Assert.IsTrue(logic.MotivationText.Contains("Top"));
        }
    }
}
