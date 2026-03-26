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
    public class MenuSeiteUnitTest
    {
        // ==========================================================
        // STUBS / FAKES
        // ==========================================================
        // Diese Klassen ersetzen die echten Services.
        // Vorteil:
        // - keine echte Datenbank
        // - keine echte Razor-UI
        // - Tests bleiben schnell und einfach

        private class QuestionSetServiceStub : IQuestionSetService
        {
            // Diese Liste wird vom Stub als Ergebnis zurückgegeben.
            public List<QuestionSet> ReturnList = new List<QuestionSet>();

            // Zählt, wie oft der Service aufgerufen wurde.
            public int Calls;

            // Speichert die letzte Thema-ID, mit der geladen wurde.
            public int LastThemaId;

            // Simuliert: Prüfungen zu einem Thema laden
            public Task<IEnumerable<QuestionSet>> GetEntriesByThemaKeyIncludingNavigationsAsync(int themaId)
            {
                Calls++;
                LastThemaId = themaId;
                return Task.FromResult<IEnumerable<QuestionSet>>(ReturnList);
            }

            // Nur vorhanden, weil das Interface diese Methode verlangt.
            public Task<IEnumerable<QuestionSet>> GetAllEntriesIncludingNavigationsAsync()
            {
                return Task.FromResult<IEnumerable<QuestionSet>>(new List<QuestionSet>());
            }

            // Nur vorhanden, weil das Interface diese Methode verlangt.
            public Task<QuestionSet> GetEntryByKeyIncludingNavigationsAsync(int key)
            {
                return Task.FromResult(new QuestionSet
                {
                    Id = key,
                    Title = "Test Quiz"
                });
            }
        }

        private class CharacterServiceStub : ICharacterService
        {
            // Liste mit Test-Characters
            public List<Character> ReturnList = new List<Character>();

            // Zählt die Aufrufe
            public int Calls;

            // Simuliert: alle Characters laden
            public Task<IEnumerable<Character>> GetAllEntriesAsync()
            {
                Calls++;
                return Task.FromResult<IEnumerable<Character>>(ReturnList);
            }

            // Simuliert: einzelnen Character per ID laden
            public Task<Character> GetEntryByKeyAsync(int key)
            {
                Character? character = ReturnList.FirstOrDefault(x => x.CharacterID == key);

                // Wenn nichts gefunden wird, liefern wir einen Test-Character zurück,
                // damit der Stub trotzdem einen gültigen Wert liefert.
                if (character is null)
                {
                    character = new Character
                    {
                        CharacterID = key,
                        Name = "TestCharacter"
                    };
                }

                return Task.FromResult(character);
            }
        }

        private class ThemaServiceStub : IThemaService
        {
            // Liste mit Test-Themen
            public List<Thema> ReturnList = new List<Thema>();

            // Zählt die Aufrufe
            public int Calls;

            // Simuliert: alle Themen laden
            public Task<IEnumerable<Thema>> GetAllEntriesIncludingNavigationsAsync()
            {
                Calls++;
                return Task.FromResult<IEnumerable<Thema>>(ReturnList);
            }
        }

        // ==========================================================
        // TEST CONTEXT
        // ==========================================================
        // Diese Hilfsklasse bündelt alles, was ein Test braucht:
        // - die eigentliche Logik
        // - die Stubs

        private class TestContext
        {
            public MenuSeiteLogic Logic { get; set; } = null!;
            public QuestionSetServiceStub QuestionSetService { get; set; } = null!;
            public CharacterServiceStub CharacterService { get; set; } = null!;
            public ThemaServiceStub ThemaService { get; set; } = null!;
        }

        /// <summary>
        /// Erstellt für jeden Test eine frische Testumgebung.
        /// So beeinflussen sich Tests nicht gegenseitig.
        /// </summary>
        private TestContext CreateContext()
        {
            QuestionSetServiceStub questionSetService = new QuestionSetServiceStub();
            CharacterServiceStub characterService = new CharacterServiceStub();
            ThemaServiceStub themaService = new ThemaServiceStub();

            MenuSeiteLogic logic = new MenuSeiteLogic(
                questionSetService,
                characterService,
                themaService);

            return new TestContext
            {
                Logic = logic,
                QuestionSetService = questionSetService,
                CharacterService = characterService,
                ThemaService = themaService
            };
        }

        // ==========================================================
        // 1) InitializeAsync()
        // ==========================================================

        [TestMethod]
        public async Task InitializeAsync_LoadsCharacters_AndFiltersThemeIdZero()
        {
            // Dieser Test prüft:
            // - Characters werden geladen
            // - Themen werden geladen
            // - Thema mit Id = 0 wird herausgefiltert

            TestContext context = CreateContext();

            context.CharacterService.ReturnList.Add(new Character
            {
                CharacterID = 1,
                Name = "Gon"
            });

            context.CharacterService.ReturnList.Add(new Character
            {
                CharacterID = 2,
                Name = "Killua"
            });

            context.ThemaService.ReturnList.Add(new Thema
            {
                Id = 0,
                Name = "Nicht anzeigen"
            });

            context.ThemaService.ReturnList.Add(new Thema
            {
                Id = 1,
                Name = "UML",
                GamePlaceName = "UML"
            });

            context.ThemaService.ReturnList.Add(new Thema
            {
                Id = 2,
                Name = "Wirtschaft",
                GamePlaceName = "Wirtschaft"
            });

            await context.Logic.InitializeAsync();

            Assert.AreEqual(2, context.Logic.CharacterList.Count);
            Assert.AreEqual(2, context.Logic.ThemenList.Count);
            Assert.AreEqual(1, context.CharacterService.Calls);
            Assert.AreEqual(1, context.ThemaService.Calls);
        }

        // ==========================================================
        // 2) IsCharacterMissing()
        // ==========================================================

        [TestMethod]
        public void IsCharacterMissing_True_WhenNull()
        {
            // Fall:
            // Es wurde kein Character ausgewählt.
            // Erwartung:
            // Die Methode liefert true.

            TestContext context = CreateContext();
            context.Logic.SelectedCharacter = null;

            Assert.IsTrue(context.Logic.IsCharacterMissing());
        }

        [TestMethod]
        public void IsCharacterMissing_False_WhenCharacterExists()
        {
            // Fall:
            // Ein Character wurde ausgewählt.
            // Erwartung:
            // Die Methode liefert false.

            TestContext context = CreateContext();
            context.Logic.SelectedCharacter = new Character
            {
                CharacterID = 1,
                Name = "Gon"
            };

            Assert.IsFalse(context.Logic.IsCharacterMissing());
        }

        // ==========================================================
        // 3) IsThemeMissing()
        // ==========================================================

        [TestMethod]
        public void IsThemeMissing_True_WhenNull()
        {
            // Fall:
            // Kein Thema ausgewählt.
            // Erwartung:
            // true

            TestContext context = CreateContext();
            context.Logic.SelectedThema = null;

            Assert.IsTrue(context.Logic.IsThemeMissing());
        }

        [TestMethod]
        public void IsThemeMissing_False_WhenThemeExists()
        {
            // Fall:
            // Thema wurde ausgewählt.
            // Erwartung:
            // false

            TestContext context = CreateContext();
            context.Logic.SelectedThema = new Thema
            {
                Id = 1,
                Name = "UML"
            };

            Assert.IsFalse(context.Logic.IsThemeMissing());
        }

        // ==========================================================
        // 4) IsQuestionSetMissing()
        // ==========================================================

        [TestMethod]
        public void IsQuestionSetMissing_True_WhenNull()
        {
            // Fall:
            // Keine Prüfung ausgewählt.
            // Erwartung:
            // true

            TestContext context = CreateContext();
            context.Logic.SelectedQuestionSet = null;

            Assert.IsTrue(context.Logic.IsQuestionSetMissing());
        }

        [TestMethod]
        public void IsQuestionSetMissing_False_WhenQuestionSetExists()
        {
            // Fall:
            // Eine Prüfung wurde ausgewählt.
            // Erwartung:
            // false

            TestContext context = CreateContext();
            context.Logic.SelectedQuestionSet = new QuestionSet
            {
                Id = 10,
                Title = "Klassendiagramm Quiz"
            };

            Assert.IsFalse(context.Logic.IsQuestionSetMissing());
        }

        // ==========================================================
        // 5) BuildQuizRoute()
        // ==========================================================

        [TestMethod]
        public void BuildQuizRoute_ReturnsCorrectUrl()
        {
            // Dieser Test prüft:
            // Ob die Quiz-Route korrekt zusammengebaut wird.

            TestContext context = CreateContext();

            string url = context.Logic.BuildQuizRoute(7, 2, 123, 99);

            Assert.AreEqual("/Quiz/ThemaID=7/CharacterId=2/SpielerID=123/QuizID=99", url);
        }

        // ==========================================================
        // 6) StartGame()
        // ==========================================================

        [TestMethod]
        public void StartGame_Fails_WhenCharacterMissing()
        {
            // Fall:
            // Character fehlt, Thema und Prüfung sind gesetzt.
            // Erwartung:
            // StartGame schlägt fehl.

            TestContext context = CreateContext();
            context.Logic.SpielerId = 50;
            context.Logic.SelectedCharacter = null;
            context.Logic.SelectedThema = new Thema
            {
                Id = 1,
                Name = "UML"
            };
            context.Logic.SelectedQuestionSet = new QuestionSet
            {
                Id = 10,
                Title = "Klassendiagramm Quiz"
            };

            MenuSeiteStartGameResult result = context.Logic.StartGame();

            Assert.IsFalse(result.Ok);
            Assert.AreEqual("Bitte Character, Themengebiet und Prüfung auswählen", result.Message);
            Assert.IsNull(result.NavigateUrl);
        }

        [TestMethod]
        public void StartGame_Fails_WhenThemeMissing()
        {
            // Fall:
            // Thema fehlt, Character und Prüfung sind gesetzt.
            // Erwartung:
            // StartGame schlägt fehl.

            TestContext context = CreateContext();
            context.Logic.SpielerId = 50;
            context.Logic.SelectedCharacter = new Character
            {
                CharacterID = 2,
                Name = "Gon"
            };
            context.Logic.SelectedThema = null;
            context.Logic.SelectedQuestionSet = new QuestionSet
            {
                Id = 10,
                Title = "Klassendiagramm Quiz"
            };

            MenuSeiteStartGameResult result = context.Logic.StartGame();

            Assert.IsFalse(result.Ok);
            Assert.AreEqual("Bitte Character, Themengebiet und Prüfung auswählen", result.Message);
            Assert.IsNull(result.NavigateUrl);
        }

        [TestMethod]
        public void StartGame_Fails_WhenQuestionSetMissing()
        {
            // Fall:
            // Prüfung fehlt, Character und Thema sind gesetzt.
            // Erwartung:
            // StartGame schlägt fehl.

            TestContext context = CreateContext();
            context.Logic.SpielerId = 50;
            context.Logic.SelectedCharacter = new Character
            {
                CharacterID = 2,
                Name = "Gon"
            };
            context.Logic.SelectedThema = new Thema
            {
                Id = 1,
                Name = "UML"
            };
            context.Logic.SelectedQuestionSet = null;

            MenuSeiteStartGameResult result = context.Logic.StartGame();

            Assert.IsFalse(result.Ok);
            Assert.AreEqual("Bitte Character, Themengebiet und Prüfung auswählen", result.Message);
            Assert.IsNull(result.NavigateUrl);
        }

        [TestMethod]
        public void StartGame_Succeeds_WhenAllSelectionsExist()
        {
            // Fall:
            // Character, Thema und Prüfung sind alle gesetzt.
            // Erwartung:
            // StartGame liefert eine korrekte Quiz-Route.

            TestContext context = CreateContext();
            context.Logic.SpielerId = 50;
            context.Logic.SelectedCharacter = new Character
            {
                CharacterID = 2,
                Name = "Gon"
            };
            context.Logic.SelectedThema = new Thema
            {
                Id = 1,
                Name = "UML"
            };
            context.Logic.SelectedQuestionSet = new QuestionSet
            {
                Id = 10,
                Title = "Klassendiagramm Quiz"
            };

            MenuSeiteStartGameResult result = context.Logic.StartGame();

            Assert.IsTrue(result.Ok);
            Assert.AreEqual("/Quiz/ThemaID=1/CharacterId=2/SpielerID=50/QuizID=10", result.NavigateUrl);
        }

        // ==========================================================
        // 7) LoadCharacterProfile()
        // ==========================================================

        [TestMethod]
        public void LoadCharacterProfile_DoesNothing_WhenCharacterIsNull()
        {
            // Fall:
            // Kein Character gesetzt.
            // Erwartung:
            // Standardwerte bleiben unverändert.

            TestContext context = CreateContext();
            context.Logic.SelectedCharacter = null;

            context.Logic.LoadCharacterProfile(true);

            Assert.AreEqual("/images/Profil_Images/Profil_Image_0.png", context.Logic.CharacterProfileImagePath);
            Assert.AreEqual("Dieser Character ist so mysteriös, dass er keine Backstory hat!", context.Logic.CharacterProfilBackstory);
        }

        [TestMethod]
        public void LoadCharacterProfile_SetsRealImageAndBackstory_WhenImageExists()
        {
            // Fall:
            // Character gesetzt und Bild existiert.
            // Erwartung:
            // Echtes Bild + echte Backstory werden übernommen.

            TestContext context = CreateContext();
            context.Logic.SelectedCharacter = new Character
            {
                CharacterID = 3,
                Name = "Killua",
                Backstory = "Schnell und talentiert"
            };

            context.Logic.LoadCharacterProfile(true);

            Assert.AreEqual("/images/Profil_Images/Profil_Image_3.png", context.Logic.CharacterProfileImagePath);
            Assert.AreEqual("Schnell und talentiert", context.Logic.CharacterProfilBackstory);
        }

        [TestMethod]
        public void LoadCharacterProfile_SetsDefaultValues_WhenImageDoesNotExist()
        {
            // Fall:
            // Character gesetzt, aber Bild existiert nicht.
            // Erwartung:
            // Standardbild + Standardtext werden gesetzt.

            TestContext context = CreateContext();
            context.Logic.SelectedCharacter = new Character
            {
                CharacterID = 3,
                Name = "Killua",
                Backstory = "Schnell und talentiert"
            };

            context.Logic.LoadCharacterProfile(false);

            Assert.AreEqual("/images/Profil_Images/Profil_Image_0.png", context.Logic.CharacterProfileImagePath);
            Assert.AreEqual("Dieser Character ist so mysteriös, dass nur der beste Detektiv seine Identität kennt!", context.Logic.CharacterProfilBackstory);
        }

        // ==========================================================
        // 8) LoadThemeInfoAsync()
        // ==========================================================

        [TestMethod]
        public async Task LoadThemeInfoAsync_DoesNothing_WhenThemeIsNull()
        {
            // Fall:
            // Kein Thema gesetzt.
            // Erwartung:
            // Bild, Beschreibung und Service-Aufruf bleiben unverändert.

            TestContext context = CreateContext();
            context.Logic.SelectedThema = null;

            await context.Logic.LoadThemeInfoAsync(true);

            Assert.AreEqual("", context.Logic.ThemeInfoImage);
            Assert.AreEqual("", context.Logic.ThemeInfoDescription);
            Assert.AreEqual(0, context.QuestionSetService.Calls);
        }

        [TestMethod]
        public async Task LoadThemeInfoAsync_SetsRealImageDescription_AndLoadsQuestionSets_WhenImageExists()
        {
            // Fall:
            // Thema gesetzt und Bild existiert.
            // Erwartung:
            // Echtes Themenbild, echte Beschreibung und Fragebogen werden geladen.

            TestContext context = CreateContext();

            context.Logic.SelectedThema = new Thema
            {
                Id = 7,
                Name = "Netzwerk",
                GamePlaceDescription = "TCP, UDP und Modelle"
            };

            context.QuestionSetService.ReturnList.Add(new QuestionSet
            {
                Id = 21,
                Title = "TCP Quiz"
            });

            context.QuestionSetService.ReturnList.Add(new QuestionSet
            {
                Id = 22,
                Title = "UDP Quiz"
            });

            await context.Logic.LoadThemeInfoAsync(true);

            Assert.AreEqual("/images/Themen/Theme_Image_7.png", context.Logic.ThemeInfoImage);
            Assert.AreEqual("TCP, UDP und Modelle \n\n(Thema: Netzwerk)", context.Logic.ThemeInfoDescription);
            Assert.AreEqual(2, context.Logic.QuestionSetsList.Count);
            Assert.AreEqual(1, context.QuestionSetService.Calls);
            Assert.AreEqual(7, context.QuestionSetService.LastThemaId);
        }

        [TestMethod]
        public async Task LoadThemeInfoAsync_SetsDefaultImageDescription_WhenImageDoesNotExist()
        {
            // Fall:
            // Thema gesetzt, aber Themenbild fehlt.
            // Erwartung:
            // Standardbild und Standardbeschreibung werden gesetzt.

            TestContext context = CreateContext();

            context.Logic.SelectedThema = new Thema
            {
                Id = 7,
                Name = "Netzwerk",
                GamePlaceDescription = "TCP, UDP und Modelle"
            };

            await context.Logic.LoadThemeInfoAsync(false);

            Assert.AreEqual("/images/Themen/Theme_Image_0.png", context.Logic.ThemeInfoImage);
            Assert.AreEqual("Die Herausforderungen dieses Gebietes sind selbst für die IHKunter Organisation unbekannt", context.Logic.ThemeInfoDescription);
        }

        // ==========================================================
        // 9) LoadQuestionSetAsync()
        // ==========================================================

        [TestMethod]
        public async Task LoadQuestionSetAsync_LoadsQuestionSets_WhenThemeExists()
        {
            // Fall:
            // Thema gesetzt, alte Prüfung gesetzt.
            // Erwartung:
            // Neue Prüfungen werden geladen und alte Auswahl wird zurückgesetzt.

            TestContext context = CreateContext();

            context.Logic.SelectedThema = new Thema
            {
                Id = 1,
                Name = "UML"
            };

            context.Logic.SelectedQuestionSet = new QuestionSet
            {
                Id = 99,
                Title = "Alt"
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

            Assert.IsNull(context.Logic.SelectedQuestionSet);
            Assert.AreEqual(2, context.Logic.QuestionSetsList.Count);
            Assert.AreEqual(1, context.QuestionSetService.Calls);
            Assert.AreEqual(1, context.QuestionSetService.LastThemaId);
        }

        [TestMethod]
        public async Task LoadQuestionSetAsync_ClearsList_WhenThemeIsNull()
        {
            // Fall:
            // Kein Thema gesetzt.
            // Erwartung:
            // Liste bleibt leer und Service wird nicht aufgerufen.

            TestContext context = CreateContext();

            context.Logic.SelectedThema = null;

            await context.Logic.LoadQuestionSetAsync();

            Assert.AreEqual(0, context.Logic.QuestionSetsList.Count);
            Assert.AreEqual(0, context.QuestionSetService.Calls);
        }

        [TestMethod]
        public async Task LoadQuestionSetAsync_ReturnsEmpty_WhenServiceReturnsEmpty()
        {
            // Fall:
            // Thema gesetzt, aber der Service liefert keine Prüfungen.
            // Erwartung:
            // Liste bleibt leer, Auswahl wird zurückgesetzt.

            TestContext context = CreateContext();

            context.Logic.SelectedThema = new Thema
            {
                Id = 2,
                Name = "Wirtschaft"
            };

            context.QuestionSetService.ReturnList.Clear();

            await context.Logic.LoadQuestionSetAsync();

            Assert.IsNull(context.Logic.SelectedQuestionSet);
            Assert.AreEqual(0, context.Logic.QuestionSetsList.Count);
            Assert.AreEqual(1, context.QuestionSetService.Calls);
            Assert.AreEqual(2, context.QuestionSetService.LastThemaId);
        }
    }
}