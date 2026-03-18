using Schulprojekt.Data;          // Spieler, Thema, QuestionSet
using Schulprojekt.Services;      // ISpielerService, IQuestionSetService, IThemaService
using System.Collections.Generic; // List<T>
using System.Linq;                // ToList()
using System.Threading.Tasks;     // Task, async/await

namespace Schulprojekt.ProjektLogic
{
    /// <summary>
    /// Ergebnis von StartGameAsync.
    /// Damit können wir in Unit-Tests prüfen:
    /// - ob die Methode erfolgreich war
    /// - welche Fehlermeldung zurückkam
    /// - welche URL gebaut wurde
    /// </summary>
    public class StartGameResult
    {
        public bool Ok { get; set; }              // true = Erfolg, false = Fehler
        public string? Message { get; set; }      // Fehlermeldung oder null
        public string? NavigateUrl { get; set; }  // Ziel-URL oder null
    }

    /// <summary>
    /// Testbare Logik der Homepage ohne Razor-UI.
    /// Hier liegt nur die Fachlogik, damit wir sie mit Unit-Tests prüfen können.
    /// </summary>
    public class HomepageLogic
    {
        // Services werden im Test durch Stubs ersetzt.
        private readonly ISpielerService _spielerService;
        private readonly IQuestionSetService _questionSetService;
        private readonly IThemaService _themaService;

        /// <summary>
        /// Konstruktor: bekommt die Services von außen.
        /// In echten Programmen kommen sie per Dependency Injection,
        /// in Tests geben wir Stubs/Fakes hinein.
        /// </summary>
        public HomepageLogic(
            ISpielerService spielerService,
            IQuestionSetService questionSetService,
            IThemaService themaService)
        {
            _spielerService = spielerService;
            _questionSetService = questionSetService;
            _themaService = themaService;
        }

        // ===========================
        // State wie in Home.razor
        // ===========================

        public string PlayerName { get; set; } = "";
        public Thema? SelectedThema { get; set; }
        public QuestionSet? SelectedQuestionSet { get; set; }

        public List<Thema> ThemenList { get; private set; } = new();
        public List<QuestionSet> QuestionSetsList { get; private set; } = new();

        /// <summary>
        /// Lädt alle Themen wie OnInitializedAsync in der Razor Page.
        /// </summary>
        public async Task LoadThemesAsync()
        {
            ThemenList = (await _themaService.GetAllEntriesIncludingNavigationsAsync()).ToList();
        }

        /// <summary>
        /// Prüft, ob Spielername fehlt.
        /// Gleiches Verhalten wie string.IsNullOrWhiteSpace(...)
        /// </summary>
        public bool IsPlayerMissing()
            => string.IsNullOrWhiteSpace(PlayerName);

        /// <summary>
        /// Prüft, ob Quiz fehlt.
        /// WICHTIG:
        /// IsNullOrEmpty bedeutet:
        /// - null => fehlt
        /// - "" => fehlt
        /// - "   " => gilt NICHT als leer
        /// </summary>
        public bool IsQuestionSetMissing()
            => string.IsNullOrEmpty(SelectedQuestionSet?.Title);

        /// <summary>
        /// Prüft, ob Thema fehlt.
        /// WICHTIG:
        /// IsNullOrEmpty bedeutet:
        /// - null => fehlt
        /// - "" => fehlt
        /// - "   " => gilt NICHT als leer
        /// </summary>
        public bool IsThemeMissing()
            => string.IsNullOrEmpty(SelectedThema?.Name);

        /// <summary>
        /// Baut die Ziel-URL für das Quiz.
        /// </summary>
        public string BuildQuizRoute(int themaId, int spielerId, int quizId)
            => $"/Quiz/ThemaID={themaId}/SpielerID={spielerId}/QuizID={quizId}";

        /// <summary>
        /// Lädt die Quiz-Sets passend zum ausgewählten Thema.
        /// Wenn kein Thema gewählt wurde, wird die Quiz-Liste geleert.
        /// </summary>
        public async Task LoadQuestionSetAsync()
        {
            if (SelectedThema is null)
            {
                QuestionSetsList.Clear();
                return;
            }

            QuestionSetsList = (await _questionSetService
                .GetEntriesByThemaKeyIncludingNavigationsAsync(SelectedThema.Id)).ToList();
        }

        /// <summary>
        /// Startet das Spiel:
        /// - prüft Eingaben
        /// - speichert Spieler
        /// - baut Ziel-URL
        /// </summary>
        public async Task<StartGameResult> StartGameAsync()
        {
            var res = new StartGameResult();

            // Wie in der Page:
            // Wenn Spielername fehlt oder kein Quiz gewählt wurde -> Fehler
            if (string.IsNullOrWhiteSpace(PlayerName) || SelectedQuestionSet is null)
            {
                res.Ok = false;
                res.Message = "Bitte Thema, Spieler und Quiz auswählen";
                return res;
            }

            // In der URL brauchen wir Thema.Id, deshalb prüfen wir Thema zusätzlich.
            if (SelectedThema is null)
            {
                res.Ok = false;
                res.Message = "Bitte Thema auswählen";
                return res;
            }

            // Spieler speichern
            var savedPlayer = await _spielerService.AddOrUpdateAsync(new Spieler
            {
                Name = PlayerName
            });

            // Wenn Speichern fehlschlägt
            if (savedPlayer is null)
            {
                res.Ok = false;
                res.Message = "Spieler konnte nicht gespeichert werden";
                return res;
            }

            // Erfolg
            res.Ok = true;
            res.NavigateUrl = BuildQuizRoute(
                SelectedThema.Id,
                savedPlayer.Id,
                SelectedQuestionSet.Id);

            return res;
        }
    }
}