using Microsoft.VisualStudio.TestTools.UnitTesting;
using Schulprojekt.Data;
using Schulprojekt.ProjektLogic;
using Schulprojekt.Services;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Unittest
{
    [TestClass]
    public class HomeCurrentPageUnitTest
    {
        // ==========================================================
        // STUBS / FAKES
        // ==========================================================
        // Diese Klasse ersetzt den echten Spieler-Service.
        // Vorteil:
        // - keine echte Datenbank
        // - keine echte UI
        // - Tests bleiben einfach und schnell

        private class SpielerServiceStub : ISpielerService
        {
            // Liste mit Test-Spielern
            public List<Spieler> Players = new List<Spieler>();

            // Rückgabewert für AddOrUpdateAsync
            public Spieler? AddOrUpdateReturnValue;

            // Zählt, wie oft GetAllPlayers aufgerufen wurde
            public int GetAllPlayersCalls;

            // Zählt, wie oft AddOrUpdateAsync aufgerufen wurde
            public int AddOrUpdateCalls;

            // Simuliert: alle Spieler laden
            public Task<IEnumerable<Spieler>> GetAllPlayers()
            {
                GetAllPlayersCalls++;
                return Task.FromResult<IEnumerable<Spieler>>(Players);
            }

            // Simuliert: neuen Spieler speichern oder bestehenden aktualisieren
            public Task<Spieler?> AddOrUpdateAsync(Spieler spieler)
            {
                AddOrUpdateCalls++;
                return Task.FromResult(AddOrUpdateReturnValue);
            }
        }

        // ==========================================================
        // TEST CONTEXT
        // ==========================================================
        // Diese Hilfsklasse bündelt alles, was ein Test braucht:
        // - die Logik
        // - den Stub-Service

        private class TestContext
        {
            public HomeCurrentPageLogic Logic { get; set; } = null!;
            public SpielerServiceStub SpielerService { get; set; } = null!;
        }

        /// <summary>
        /// Erstellt für jeden Test eine frische Testumgebung.
        /// So beeinflussen sich Tests nicht gegenseitig.
        /// </summary>
        private TestContext CreateContext()
        {
            SpielerServiceStub spielerService = new SpielerServiceStub();
            HomeCurrentPageLogic logic = new HomeCurrentPageLogic(spielerService);

            return new TestContext
            {
                Logic = logic,
                SpielerService = spielerService
            };
        }

        // ==========================================================
        // 1) IsPlayerMissing()
        // ==========================================================

        [TestMethod]
        public void IsPlayerMissing_True_WhenEmpty()
        {
            // Fall:
            // Spielername ist leer.
            // Erwartung:
            // Die Methode liefert true.

            TestContext context = CreateContext();
            context.Logic.PlayerName = "";

            Assert.IsTrue(context.Logic.IsPlayerMissing());
        }

        [TestMethod]
        public void IsPlayerMissing_True_WhenWhitespace()
        {
            // Fall:
            // Spielername besteht nur aus Leerzeichen.
            // Erwartung:
            // Die Methode liefert true.

            TestContext context = CreateContext();
            context.Logic.PlayerName = "   ";

            Assert.IsTrue(context.Logic.IsPlayerMissing());
        }

        [TestMethod]
        public void IsPlayerMissing_False_WhenTextExists()
        {
            // Fall:
            // Spielername enthält normalen Text.
            // Erwartung:
            // Die Methode liefert false.

            TestContext context = CreateContext();
            context.Logic.PlayerName = "Gon";

            Assert.IsFalse(context.Logic.IsPlayerMissing());
        }

        // ==========================================================
        // 2) IsPlayerExistAsync()
        // ==========================================================

        [TestMethod]
        public async Task IsPlayerExistAsync_ReturnsFalse_WhenPlayerDoesNotExist()
        {
            // Fall:
            // Es gibt keinen Spieler mit diesem Namen.
            // Erwartung:
            // Die Methode liefert false.

            TestContext context = CreateContext();

            bool result = await context.Logic.IsPlayerExistAsync("Gon");

            Assert.IsFalse(result);
            Assert.AreEqual(1, context.SpielerService.GetAllPlayersCalls);
        }

        [TestMethod]
        public async Task IsPlayerExistAsync_ReturnsTrue_WhenPlayerExists()
        {
            // Fall:
            // Ein Spieler mit dem Namen existiert bereits.
            // Erwartung:
            // Die Methode liefert true.

            TestContext context = CreateContext();
            context.SpielerService.Players.Add(new Spieler
            {
                Id = 1,
                Name = "Gon"
            });

            bool result = await context.Logic.IsPlayerExistAsync("Gon");

            Assert.IsTrue(result);
        }

        [TestMethod]
        public async Task IsPlayerExistAsync_ReturnsTrue_IgnoringUpperLowerCase()
        {
            // Fall:
            // Spieler existiert, aber mit anderer Groß-/Kleinschreibung.
            // Erwartung:
            // Die Methode liefert trotzdem true.

            TestContext context = CreateContext();
            context.SpielerService.Players.Add(new Spieler
            {
                Id = 1,
                Name = "gOn"
            });

            bool result = await context.Logic.IsPlayerExistAsync("GON");

            Assert.IsTrue(result);
        }

        // ==========================================================
        // 3) BuildMenuRoute()
        // ==========================================================

        [TestMethod]
        public void BuildMenuRoute_ReturnsCorrectUrl()
        {
            // Dieser Test prüft:
            // Ob die Route zur Menüseite korrekt gebaut wird.

            TestContext context = CreateContext();

            string url = context.Logic.BuildMenuRoute(123);

            Assert.AreEqual("/MenuSeite/SpielerID=123", url);
        }

        // ==========================================================
        // 4) StartGameAsync()
        // ==========================================================

        [TestMethod]
        public async Task StartGameAsync_Fails_WhenPlayerNameMissing()
        {
            // Fall:
            // Spielername fehlt.
            // Erwartung:
            // StartGame schlägt fehl und liefert eine Fehlermeldung.

            TestContext context = CreateContext();
            context.Logic.PlayerName = "   ";

            HomeCurrentStartGameResult result = await context.Logic.StartGameAsync();

            Assert.IsFalse(result.Ok);
            Assert.IsFalse(result.Cancelled);
            Assert.AreEqual("Bitte Spieler eingeben!", result.Message);
            Assert.IsNull(result.NavigateUrl);
            Assert.AreEqual(0, context.SpielerService.AddOrUpdateCalls);
        }

        [TestMethod]
        public async Task StartGameAsync_Cancels_WhenExistingPlayerAndUserDoesNotConfirm()
        {
            // Fall:
            // Spieler existiert bereits, aber Benutzer bestätigt das Laden nicht.
            // Erwartung:
            // Vorgang wird abgebrochen.

            TestContext context = CreateContext();
            context.Logic.PlayerName = "Gon";

            context.SpielerService.Players.Add(new Spieler
            {
                Id = 10,
                Name = "Gon"
            });

            HomeCurrentStartGameResult result = await context.Logic.StartGameAsync(confirmExistingPlayer: false);

            Assert.IsFalse(result.Ok);
            Assert.IsTrue(result.Cancelled);
            Assert.IsNull(result.NavigateUrl);
        }

        [TestMethod]
        public async Task StartGameAsync_LoadsExistingPlayer_WhenUserConfirms()
        {
            // Fall:
            // Spieler existiert und Benutzer bestätigt das Laden.
            // Erwartung:
            // Vorhandener Spielstand wird geladen und Route wird gesetzt.

            TestContext context = CreateContext();
            context.Logic.PlayerName = "Gon";

            context.SpielerService.Players.Add(new Spieler
            {
                Id = 10,
                Name = "Gon"
            });

            HomeCurrentStartGameResult result = await context.Logic.StartGameAsync(confirmExistingPlayer: true);

            Assert.IsTrue(result.Ok);
            Assert.IsTrue(result.ExistingPlayerLoaded);
            Assert.IsFalse(result.NewPlayerCreated);
            Assert.AreEqual("/MenuSeite/SpielerID=10", result.NavigateUrl);
        }

        [TestMethod]
        public async Task StartGameAsync_Cancels_WhenNewPlayerAndUserDoesNotConfirm()
        {
            // Fall:
            // Spieler existiert nicht und Benutzer möchte keinen neuen Spielstand anlegen.
            // Erwartung:
            // Vorgang wird abgebrochen.

            TestContext context = CreateContext();
            context.Logic.PlayerName = "Killua";

            HomeCurrentStartGameResult result = await context.Logic.StartGameAsync(confirmCreateNewPlayer: false);

            Assert.IsFalse(result.Ok);
            Assert.IsTrue(result.Cancelled);
            Assert.IsNull(result.NavigateUrl);
        }

        [TestMethod]
        public async Task StartGameAsync_CreatesNewPlayer_WhenUserConfirms()
        {
            // Fall:
            // Spieler existiert nicht und Benutzer bestätigt das Anlegen.
            // Erwartung:
            // Neuer Spieler wird erstellt und Route wird gesetzt.

            TestContext context = CreateContext();
            context.Logic.PlayerName = "Killua";

            context.SpielerService.AddOrUpdateReturnValue = new Spieler
            {
                Id = 20,
                Name = "Killua"
            };

            HomeCurrentStartGameResult result = await context.Logic.StartGameAsync(confirmCreateNewPlayer: true);

            Assert.IsTrue(result.Ok);
            Assert.IsFalse(result.ExistingPlayerLoaded);
            Assert.IsTrue(result.NewPlayerCreated);
            Assert.AreEqual("/MenuSeite/SpielerID=20", result.NavigateUrl);
            Assert.AreEqual(1, context.SpielerService.AddOrUpdateCalls);
        }

        [TestMethod]
        public async Task StartGameAsync_Fails_WhenNewPlayerCouldNotBeSaved()
        {
            // Fall:
            // Neuer Spieler soll angelegt werden, aber das Speichern schlägt fehl.
            // Erwartung:
            // StartGame liefert eine Fehlermeldung.

            TestContext context = CreateContext();
            context.Logic.PlayerName = "Kurapika";

            context.SpielerService.AddOrUpdateReturnValue = null;

            HomeCurrentStartGameResult result = await context.Logic.StartGameAsync(confirmCreateNewPlayer: true);

            Assert.IsFalse(result.Ok);
            Assert.IsFalse(result.Cancelled);
            Assert.AreEqual("Spieler konnte nicht gespeichert werden.", result.Message);
            Assert.IsNull(result.NavigateUrl);
        }

        [TestMethod]
        public async Task StartGameAsync_Fails_WhenLoadedPlayerHasInvalidId()
        {
            // Fall:
            // Vorhandener Spieler wird geladen, hat aber eine ungültige ID (0).
            // Erwartung:
            // StartGame schlägt fehl.

            TestContext context = CreateContext();
            context.Logic.PlayerName = "Leorio";

            context.SpielerService.Players.Add(new Spieler
            {
                Id = 0,
                Name = "Leorio"
            });

            HomeCurrentStartGameResult result = await context.Logic.StartGameAsync(confirmExistingPlayer: true);

            Assert.IsFalse(result.Ok);
            Assert.AreEqual("Ungültige Spieler-ID.", result.Message);
            Assert.IsNull(result.NavigateUrl);
        }
    }
}