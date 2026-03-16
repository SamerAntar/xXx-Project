using Microsoft.VisualStudio.TestTools.UnitTesting;
using Schulprojekt.Data;
using Schulprojekt.ProjektLogic;
using Schulprojekt.Services;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Unittest
{
    [TestClass]
    public class HomepageLogicTest
    {
        // ==========================================================
        // STUBS / FAKES
        // ==========================================================
        // Diese ersetzen echte Services.
        // Vorteil:
        // - keine echte DB
        // - keine echte UI
        // - Tests bleiben einfach und schnell

        private class SpielerServiceStub : ISpielerService
        {
            public Spieler? ReturnValue;
            public int Calls;

            public Task<Spieler?> AddOrUpdateAsync(Spieler spieler)
            {
                Calls++;
                return Task.FromResult(ReturnValue);
            }

            public Task<IEnumerable<Spieler>> GetAllPlayers()
            {
                return Task.FromResult<IEnumerable<Spieler>>(new List<Spieler>());
            }
        }

        private class QuestionSetServiceStub : IQuestionSetService
        {
            public List<QuestionSet> ReturnList = new List<QuestionSet>();
            public int Calls;
            public int LastThemaId;

            public Task<IEnumerable<QuestionSet>> GetEntriesByThemaKeyIncludingNavigationsAsync(int themaId)
            {
                Calls++;
                LastThemaId = themaId;
                return Task.FromResult<IEnumerable<QuestionSet>>(ReturnList);
            }

            public Task<IEnumerable<QuestionSet>> GetAllEntriesIncludingNavigationsAsync()
            {
                return Task.FromResult<IEnumerable<QuestionSet>>(new List<QuestionSet>());
            }

            public Task<QuestionSet> GetEntryByKeyIncludingNavigationsAsync(int key)
            {
                return Task.FromResult(new QuestionSet
                {
                    Id = key,
                    Title = "Dummy"
                });
            }
        }

        private class ThemaServiceStub : IThemaService
        {
            public List<Thema> ReturnList = new List<Thema>();
            public int Calls;

            public Task<IEnumerable<Thema>> GetAllEntriesIncludingNavigationsAsync()
            {
                Calls++;
                return Task.FromResult<IEnumerable<Thema>>(ReturnList);
            }
        }

        // ==========================================================
        // TEST CONTEXT
        // ==========================================================
        // verwenden wir eine kleine Hilfsklasse.
        // So ist der Zugriff verständlicher:
        // context.Logic
        // context.SpielerService
        // context.QuestionSetService
        // context.ThemaService

        private class TestContext
        {
            public HomepageLogic Logic { get; set; }
            public SpielerServiceStub SpielerService { get; set; }
            public QuestionSetServiceStub QuestionSetService { get; set; }
            public ThemaServiceStub ThemaService { get; set; }
        }

        /// <summary>
        /// Erstellt pro Test eine frische Testumgebung.
        /// So beeinflussen sich die Tests nicht gegenseitig.
        /// </summary>
        private TestContext CreateContext()
        {
            SpielerServiceStub spielerService = new SpielerServiceStub();
            QuestionSetServiceStub questionSetService = new QuestionSetServiceStub();
            ThemaServiceStub themaService = new ThemaServiceStub();

            HomepageLogic logic = new HomepageLogic(
                spielerService,
                questionSetService,
                themaService);

            TestContext context = new TestContext();
            context.Logic = logic;
            context.SpielerService = spielerService;
            context.QuestionSetService = questionSetService;
            context.ThemaService = themaService;

            return context;
        }

        // ==========================================================
        // 1) IsPlayerMissing() -> 6 Tests
        // ==========================================================

        [TestMethod]
        public void IsPlayerMissing_True_WhenEmpty()
        {
            TestContext context = CreateContext();
            context.Logic.PlayerName = "";

            Assert.IsTrue(context.Logic.IsPlayerMissing());
        }

        [TestMethod]
        public void IsPlayerMissing_True_WhenWhitespaceSpaces()
        {
            TestContext context = CreateContext();
            context.Logic.PlayerName = "   ";

            Assert.IsTrue(context.Logic.IsPlayerMissing());
        }

        [TestMethod]
        public void IsPlayerMissing_True_WhenTab()
        {
            TestContext context = CreateContext();
            context.Logic.PlayerName = "\t";

            Assert.IsTrue(context.Logic.IsPlayerMissing());
        }

        [TestMethod]
        public void IsPlayerMissing_True_WhenNewLine()
        {
            TestContext context = CreateContext();
            context.Logic.PlayerName = "\n";

            Assert.IsTrue(context.Logic.IsPlayerMissing());
        }

        [TestMethod]
        public void IsPlayerMissing_False_WhenText()
        {
            TestContext context = CreateContext();
            context.Logic.PlayerName = "Gon";

            Assert.IsFalse(context.Logic.IsPlayerMissing());
        }

        [TestMethod]
        public void IsPlayerMissing_False_WhenTextHasSpacesAround()
        {
            TestContext context = CreateContext();
            context.Logic.PlayerName = "  Gon  ";

            Assert.IsFalse(context.Logic.IsPlayerMissing());
        }

        // ==========================================================
        // 2) IsQuestionSetMissing() -> 5 Tests
        // ==========================================================

        [TestMethod]
        public void IsQuestionSetMissing_True_WhenNull()
        {
            TestContext context = CreateContext();
            context.Logic.SelectedQuestionSet = null;

            Assert.IsTrue(context.Logic.IsQuestionSetMissing());
        }

        [TestMethod]
        public void IsQuestionSetMissing_True_WhenTitleNull()
        {
            TestContext context = CreateContext();
            context.Logic.SelectedQuestionSet = new QuestionSet
            {
                Title = null
            };

            Assert.IsTrue(context.Logic.IsQuestionSetMissing());
        }

        [TestMethod]
        public void IsQuestionSetMissing_True_WhenTitleEmpty()
        {
            TestContext context = CreateContext();
            context.Logic.SelectedQuestionSet = new QuestionSet
            {
                Title = ""
            };

            Assert.IsTrue(context.Logic.IsQuestionSetMissing());
        }

        [TestMethod]
        public void IsQuestionSetMissing_False_WhenTitleWhitespace()
        {
            // Wichtig:
            // string.IsNullOrEmpty("   ") = false
            TestContext context = CreateContext();
            context.Logic.SelectedQuestionSet = new QuestionSet
            {
                Title = "   "
            };

            Assert.IsFalse(context.Logic.IsQuestionSetMissing());
        }

        [TestMethod]
        public void IsQuestionSetMissing_False_WhenTitleHasText()
        {
            TestContext context = CreateContext();
            context.Logic.SelectedQuestionSet = new QuestionSet
            {
                Title = "Quiz 1"
            };

            Assert.IsFalse(context.Logic.IsQuestionSetMissing());
        }

        // ==========================================================
        // 3) IsThemeMissing() -> 5 Tests
        // ==========================================================

        [TestMethod]
        public void IsThemeMissing_True_WhenNull()
        {
            TestContext context = CreateContext();
            context.Logic.SelectedThema = null;

            Assert.IsTrue(context.Logic.IsThemeMissing());
        }

        [TestMethod]
        public void IsThemeMissing_True_WhenNameNull()
        {
            TestContext context = CreateContext();
            context.Logic.SelectedThema = new Thema
            {
                Name = null
            };

            Assert.IsTrue(context.Logic.IsThemeMissing());
        }

        [TestMethod]
        public void IsThemeMissing_True_WhenNameEmpty()
        {
            TestContext context = CreateContext();
            context.Logic.SelectedThema = new Thema
            {
                Name = ""
            };

            Assert.IsTrue(context.Logic.IsThemeMissing());
        }

        [TestMethod]
        public void IsThemeMissing_False_WhenNameWhitespace()
        {
            // Wichtig:
            // string.IsNullOrEmpty("   ") = false
            TestContext context = CreateContext();
            context.Logic.SelectedThema = new Thema
            {
                Name = "   "
            };

            Assert.IsFalse(context.Logic.IsThemeMissing());
        }

        [TestMethod]
        public void IsThemeMissing_False_WhenNameHasText()
        {
            TestContext context = CreateContext();
            context.Logic.SelectedThema = new Thema
            {
                Name = "UML"
            };

            Assert.IsFalse(context.Logic.IsThemeMissing());
        }

        // ==========================================================
        // 4) BuildQuizRoute() -> 3 Tests
        // ==========================================================

        [TestMethod]
        public void BuildQuizRoute_ReturnsCorrectUrl()
        {
            TestContext context = CreateContext();

            string url = context.Logic.BuildQuizRoute(7, 123, 99);

            Assert.AreEqual("/Quiz/ThemaID=7/SpielerID=123/QuizID=99", url);
        }

        [TestMethod]
        public void BuildQuizRoute_WorksWithZero()
        {
            TestContext context = CreateContext();

            string url = context.Logic.BuildQuizRoute(0, 0, 0);

            Assert.AreEqual("/Quiz/ThemaID=0/SpielerID=0/QuizID=0", url);
        }

        [TestMethod]
        public void BuildQuizRoute_WorksWithMaxValues()
        {
            TestContext context = CreateContext();

            int t = int.MaxValue;
            int s = int.MaxValue;
            int q = int.MaxValue;

            string url = context.Logic.BuildQuizRoute(t, s, q);

            Assert.AreEqual($"/Quiz/ThemaID={t}/SpielerID={s}/QuizID={q}", url);
        }

        // ==========================================================
        // 5) LoadQuestionSetAsync() -> 4 Tests
        // ==========================================================

        [TestMethod]
        public async Task LoadQuestionSetAsync_ThemaNull_ClearsList_AndNoServiceCall()
        {
            TestContext context = CreateContext();
            context.Logic.SelectedThema = null;

            await context.Logic.LoadQuestionSetAsync();

            Assert.AreEqual(0, context.Logic.QuestionSetsList.Count);
            Assert.AreEqual(0, context.QuestionSetService.Calls);
        }

        [TestMethod]
        public async Task LoadQuestionSetAsync_ThemaSet_LoadsList()
        {
            TestContext context = CreateContext();

            context.Logic.SelectedThema = new Thema
            {
                Id = 7,
                Name = "UML"
            };

            context.QuestionSetService.ReturnList.Add(new QuestionSet
            {
                Id = 10,
                Title = "Klassendiagramm Quiz"
            });

            context.QuestionSetService.ReturnList.Add(new QuestionSet
            {
                Id = 11,
                Title = "Sequenzdiagramm Quiz"
            });

            await context.Logic.LoadQuestionSetAsync();

            Assert.AreEqual(2, context.Logic.QuestionSetsList.Count);
            Assert.AreEqual(1, context.QuestionSetService.Calls);
            Assert.AreEqual(7, context.QuestionSetService.LastThemaId);
        }

        [TestMethod]
        public async Task LoadQuestionSetAsync_ThemaSet_ServiceReturnsEmptyList_ResultEmpty()
        {
            TestContext context = CreateContext();

            context.Logic.SelectedThema = new Thema
            {
                Id = 7,
                Name = "UML"
            };

            context.QuestionSetService.ReturnList.Clear();

            await context.Logic.LoadQuestionSetAsync();

            Assert.AreEqual(0, context.Logic.QuestionSetsList.Count);
            Assert.AreEqual(1, context.QuestionSetService.Calls);
            Assert.AreEqual(7, context.QuestionSetService.LastThemaId);
        }

        [TestMethod]
        public async Task LoadQuestionSetAsync_ThemaSet_OverwritesOldList()
        {
            TestContext context = CreateContext();

            context.Logic.SelectedThema = new Thema
            {
                Id = 3,
                Name = "Netzwerk"
            };

            context.QuestionSetService.ReturnList.Add(new QuestionSet
            {
                Id = 21,
                Title = "TCP"
            });

            await context.Logic.LoadQuestionSetAsync();

            Assert.AreEqual(1, context.Logic.QuestionSetsList.Count);
            Assert.AreEqual("TCP", context.Logic.QuestionSetsList[0].Title);
            Assert.AreEqual(1, context.QuestionSetService.Calls);
        }

        // ==========================================================
        // 6) StartGameAsync() -> 6 Tests
        // ==========================================================

        [TestMethod]
        public async Task StartGameAsync_Fails_WhenPlayerMissing()
        {
            TestContext context = CreateContext();

            context.Logic.PlayerName = "   ";
            context.Logic.SelectedQuestionSet = new QuestionSet
            {
                Id = 10,
                Title = "Quiz"
            };
            context.Logic.SelectedThema = new Thema
            {
                Id = 7,
                Name = "UML"
            };

            StartGameResult res = await context.Logic.StartGameAsync();

            Assert.IsFalse(res.Ok);
            Assert.AreEqual("Bitte Thema, Spieler und Quiz auswählen", res.Message);
            Assert.IsNull(res.NavigateUrl);
            Assert.AreEqual(0, context.SpielerService.Calls);
        }

        [TestMethod]
        public async Task StartGameAsync_Fails_WhenQuestionSetNull()
        {
            TestContext context = CreateContext();

            context.Logic.PlayerName = "Gon";
            context.Logic.SelectedQuestionSet = null;
            context.Logic.SelectedThema = new Thema
            {
                Id = 7,
                Name = "UML"
            };

            StartGameResult res = await context.Logic.StartGameAsync();

            Assert.IsFalse(res.Ok);
            Assert.AreEqual("Bitte Thema, Spieler und Quiz auswählen", res.Message);
            Assert.IsNull(res.NavigateUrl);
            Assert.AreEqual(0, context.SpielerService.Calls);
        }

        [TestMethod]
        public async Task StartGameAsync_Fails_WhenThemaMissing()
        {
            TestContext context = CreateContext();

            context.Logic.PlayerName = "Gon";
            context.Logic.SelectedQuestionSet = new QuestionSet
            {
                Id = 99,
                Title = "Quiz"
            };
            context.Logic.SelectedThema = null;

            StartGameResult res = await context.Logic.StartGameAsync();

            Assert.IsFalse(res.Ok);
            Assert.AreEqual("Bitte Thema auswählen", res.Message);
            Assert.IsNull(res.NavigateUrl);
            Assert.AreEqual(0, context.SpielerService.Calls);
        }

        [TestMethod]
        public async Task StartGameAsync_Fails_WhenPlayerCouldNotBeSaved()
        {
            TestContext context = CreateContext();

            context.Logic.PlayerName = "Gon";
            context.Logic.SelectedQuestionSet = new QuestionSet
            {
                Id = 99,
                Title = "Quiz"
            };
            context.Logic.SelectedThema = new Thema
            {
                Id = 7,
                Name = "UML"
            };

            context.SpielerService.ReturnValue = null;

            StartGameResult res = await context.Logic.StartGameAsync();

            Assert.IsFalse(res.Ok);
            Assert.AreEqual("Spieler konnte nicht gespeichert werden", res.Message);
            Assert.IsNull(res.NavigateUrl);
            Assert.AreEqual(1, context.SpielerService.Calls);
        }

        [TestMethod]
        public async Task StartGameAsync_Succeeds_WhenServiceReturnsPlayer()
        {
            TestContext context = CreateContext();

            context.Logic.PlayerName = "Gon";
            context.Logic.SelectedQuestionSet = new QuestionSet
            {
                Id = 99,
                Title = "Quiz"
            };
            context.Logic.SelectedThema = new Thema
            {
                Id = 7,
                Name = "UML"
            };

            context.SpielerService.ReturnValue = new Spieler
            {
                Id = 123,
                Name = "Gon"
            };

            StartGameResult res = await context.Logic.StartGameAsync();

            Assert.IsTrue(res.Ok);
            Assert.IsNull(res.Message);
            Assert.AreEqual("/Quiz/ThemaID=7/SpielerID=123/QuizID=99", res.NavigateUrl);
            Assert.AreEqual(1, context.SpielerService.Calls);
        }

        [TestMethod]
        public async Task StartGameAsync_Succeeds_WithPlayerNameContainingSpacesAround()
        {
            TestContext context = CreateContext();

            context.Logic.PlayerName = "  Gon  ";
            context.Logic.SelectedQuestionSet = new QuestionSet
            {
                Id = 8,
                Title = "Quiz A"
            };
            context.Logic.SelectedThema = new Thema
            {
                Id = 5,
                Name = "Anime"
            };

            context.SpielerService.ReturnValue = new Spieler
            {
                Id = 50,
                Name = "  Gon  "
            };

            StartGameResult res = await context.Logic.StartGameAsync();

            Assert.IsTrue(res.Ok);
            Assert.AreEqual("/Quiz/ThemaID=5/SpielerID=50/QuizID=8", res.NavigateUrl);
            Assert.AreEqual(1, context.SpielerService.Calls);
        }

        // ==========================================================
        // 7) LoadThemesAsync() -> 3 Tests
        // ==========================================================

        [TestMethod]
        public async Task LoadThemesAsync_Loads2Themes()
        {
            TestContext context = CreateContext();

            context.ThemaService.ReturnList.Add(new Thema
            {
                Id = 1,
                Name = "UML"
            });

            context.ThemaService.ReturnList.Add(new Thema
            {
                Id = 2,
                Name = "Wirtschaft"
            });

            await context.Logic.LoadThemesAsync();

            Assert.AreEqual(2, context.Logic.ThemenList.Count);
            Assert.AreEqual(1, context.ThemaService.Calls);
        }

        [TestMethod]
        public async Task LoadThemesAsync_LoadsEmpty_WhenServiceReturnsEmpty()
        {
            TestContext context = CreateContext();

            context.ThemaService.ReturnList.Clear();

            await context.Logic.LoadThemesAsync();

            Assert.AreEqual(0, context.Logic.ThemenList.Count);
            Assert.AreEqual(1, context.ThemaService.Calls);
        }

        [TestMethod]
        public async Task LoadThemesAsync_StoresCorrectValues()
        {
            TestContext context = CreateContext();

            context.ThemaService.ReturnList.Add(new Thema
            {
                Id = 10,
                Name = "UML"
            });

            await context.Logic.LoadThemesAsync();

            Assert.AreEqual(1, context.Logic.ThemenList.Count);
            Assert.AreEqual(10, context.Logic.ThemenList[0].Id);
            Assert.AreEqual("UML", context.Logic.ThemenList[0].Name);
        }
    }
}