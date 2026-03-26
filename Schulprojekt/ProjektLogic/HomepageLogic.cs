using Schulprojekt.Data;          // Spieler, Thema, QuestionSet, Character
using Schulprojekt.Services;      // ISpielerService, IQuestionSetService, IThemaService, ICharacterService
using System;                     // StringComparison
using System.Collections.Generic; // List<T>
using System.Linq;                // ToList(), Where(), FirstOrDefault()
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

    // ==========================================================
    // ZUSÄTZLICHE LOGIK FÜR DIE AKTUELLE Home.razor
    // Alte HomepageLogic oben bleibt unverändert bestehen.
    // ==========================================================

    /// <summary>
    /// Ergebnis für StartGame der aktuellen Home.razor.
    /// </summary>
    public class HomeCurrentStartGameResult
    {
        public bool Ok { get; set; }
        public bool Cancelled { get; set; }
        public bool ExistingPlayerLoaded { get; set; }
        public bool NewPlayerCreated { get; set; }
        public string? Message { get; set; }
        public string? NavigateUrl { get; set; }
        public Spieler? Player { get; set; }
    }

    /// <summary>
    /// Zusätzliche testbare Logik für die aktuelle Home.razor.
    /// Alte HomepageLogic bleibt unverändert bestehen.
    /// </summary>
    public class HomeCurrentPageLogic
    {
        private readonly ISpielerService _spielerService;

        public HomeCurrentPageLogic(ISpielerService spielerService)
        {
            _spielerService = spielerService;
        }

        public string PlayerName { get; set; } = "";

        /// <summary>
        /// Prüft, ob Spielername fehlt.
        /// </summary>
        public bool IsPlayerMissing()
            => string.IsNullOrWhiteSpace(PlayerName);

        /// <summary>
        /// Prüft, ob ein Spieler mit dem Namen bereits existiert.
        /// Vergleich ist case-insensitive.
        /// </summary>
        public async Task<bool> IsPlayerExistAsync(string playerName)
        {
            Spieler? player = (await _spielerService.GetAllPlayers())
                .FirstOrDefault(x => string.Equals(x.Name, playerName, StringComparison.OrdinalIgnoreCase));

            return player is not null;
        }

        /// <summary>
        /// Baut die Ziel-URL für die Menüseite.
        /// </summary>
        public string BuildMenuRoute(int spielerId)
            => $"/MenuSeite/SpielerID={spielerId}";

        /// <summary>
        /// confirmExistingPlayer:
        /// true  = vorhandenen Spielstand laden
        /// false = abbrechen
        ///
        /// confirmCreateNewPlayer:
        /// true  = neuen Spielstand anlegen
        /// false = abbrechen
        /// </summary>
        public async Task<HomeCurrentStartGameResult> StartGameAsync(
            bool? confirmExistingPlayer = null,
            bool? confirmCreateNewPlayer = null)
        {
            var result = new HomeCurrentStartGameResult();

            if (string.IsNullOrWhiteSpace(PlayerName))
            {
                result.Ok = false;
                result.Message = "Bitte Spieler eingeben!";
                return result;
            }

            Spieler? player = null;
            bool exists = await IsPlayerExistAsync(PlayerName);

            if (exists)
            {
                if (confirmExistingPlayer != true)
                {
                    result.Ok = false;
                    result.Cancelled = true;
                    return result;
                }

                player = (await _spielerService.GetAllPlayers())
                    .FirstOrDefault(x => string.Equals(x.Name, PlayerName, StringComparison.OrdinalIgnoreCase));

                if (player is null)
                {
                    result.Ok = false;
                    result.Message = "Spielstand konnte nicht geladen werden.";
                    return result;
                }

                result.ExistingPlayerLoaded = true;
            }
            else
            {
                if (confirmCreateNewPlayer != true)
                {
                    result.Ok = false;
                    result.Cancelled = true;
                    return result;
                }

                player = await _spielerService.AddOrUpdateAsync(new Spieler
                {
                    Name = PlayerName
                });

                if (player is null)
                {
                    result.Ok = false;
                    result.Message = "Spieler konnte nicht gespeichert werden.";
                    return result;
                }

                result.NewPlayerCreated = true;
            }

            if (player.Id > 0)
            {
                result.Ok = true;
                result.Player = player;
                result.NavigateUrl = BuildMenuRoute(player.Id);
                return result;
            }

            result.Ok = false;
            result.Message = "Ungültige Spieler-ID.";
            return result;
        }
    }

    // ==========================================================
    // ZUSÄTZLICHE LOGIK FÜR MenuSeite.razor
    // Alte HomepageLogic oben bleibt unverändert bestehen.
    // ==========================================================

    /// <summary>
    /// Ergebnis von StartGame auf der MenuSeite.
    /// </summary>
    public class MenuSeiteStartGameResult
    {
        public bool Ok { get; set; }
        public string? Message { get; set; }
        public string? NavigateUrl { get; set; }
    }

    /// <summary>
    /// Zusätzliche testbare Logik für MenuSeite.razor.
    /// </summary>
    public class MenuSeiteLogic
    {
        private readonly IQuestionSetService _questionSetService;
        private readonly ICharacterService _characterService;
        private readonly IThemaService _themaService;

        public MenuSeiteLogic(
            IQuestionSetService questionSetService,
            ICharacterService characterService,
            IThemaService themaService)
        {
            _questionSetService = questionSetService;
            _characterService = characterService;
            _themaService = themaService;
        }

        public int SpielerId { get; set; }

        public List<QuestionSet> QuestionSetsList { get; private set; } = new();
        public List<Character> CharacterList { get; private set; } = new();
        public List<Thema> ThemenList { get; private set; } = new();

        public QuestionSet? SelectedQuestionSet { get; set; }
        public Character? SelectedCharacter { get; set; }
        public Thema? SelectedThema { get; set; }

        public string CharacterProfileImagePath { get; private set; } = "/images/Profil_Images/Profil_Image_0.png";
        public string CharacterProfilBackstory { get; private set; } = "Dieser Character ist so mysteriös, dass er keine Backstory hat!";
        public string ThemeInfoImage { get; private set; } = "";
        public string ThemeInfoDescription { get; private set; } = "";

        /// <summary>
        /// Lädt Characters und Themen.
        /// Thema mit Id 0 wird herausgefiltert.
        /// </summary>
        public async Task InitializeAsync()
        {
            CharacterList = (await _characterService.GetAllEntriesAsync()).ToList();
            ThemenList = (await _themaService.GetAllEntriesIncludingNavigationsAsync())
                .Where(x => x.Id != 0)
                .ToList();
        }

        public bool IsCharacterMissing()
            => string.IsNullOrEmpty(SelectedCharacter?.Name);

        public bool IsQuestionSetMissing()
            => string.IsNullOrEmpty(SelectedQuestionSet?.Title);

        public bool IsThemeMissing()
            => string.IsNullOrEmpty(SelectedThema?.Name);

        public string BuildQuizRoute(int themaId, int characterId, int spielerId, int quizId)
            => $"/Quiz/ThemaID={themaId}/CharacterId={characterId}/SpielerID={spielerId}/QuizID={quizId}";

        public MenuSeiteStartGameResult StartGame()
        {
            var result = new MenuSeiteStartGameResult();

            if (SelectedCharacter is null || SelectedThema is null || SelectedQuestionSet is null)
            {
                result.Ok = false;
                result.Message = "Bitte Character, Themengebiet und Prüfung auswählen";
                return result;
            }

            result.Ok = true;
            result.NavigateUrl = BuildQuizRoute(
                SelectedThema.Id,
                SelectedCharacter.CharacterID,
                SpielerId,
                SelectedQuestionSet.Id);

            return result;
        }

        /// <summary>
        /// imageExists simuliert File.Exists(...)
        /// </summary>
        public void LoadCharacterProfile(bool imageExists)
        {
            if (SelectedCharacter is null)
                return;

            if (imageExists)
            {
                CharacterProfileImagePath = $"/images/Profil_Images/Profil_Image_{SelectedCharacter.CharacterID}.png";
                CharacterProfilBackstory = $"{SelectedCharacter.Backstory}";
            }
            else
            {
                CharacterProfileImagePath = "/images/Profil_Images/Profil_Image_0.png";
                CharacterProfilBackstory = "Dieser Character ist so mysteriös, dass nur der beste Detektiv seine Identität kennt!";
            }
        }

        /// <summary>
        /// imageExists simuliert File.Exists(...)
        /// </summary>
        public async Task LoadThemeInfoAsync(bool imageExists)
        {
            if (SelectedThema is null)
                return;

            if (imageExists)
            {
                ThemeInfoImage = $"/images/Themen/Theme_Image_{SelectedThema.Id}.png";
                ThemeInfoDescription = $"{SelectedThema.GamePlaceDescription} \n\n(Thema: {SelectedThema.Name})";
            }
            else
            {
                ThemeInfoImage = "/images/Themen/Theme_Image_0.png";
                ThemeInfoDescription = "Die Herausforderungen dieses Gebietes sind selbst für die IHKunter Organisation unbekannt";
            }

            await LoadQuestionSetAsync();
        }

        public async Task LoadQuestionSetAsync()
        {
            SelectedQuestionSet = null;

            if (SelectedThema is not null)
            {
                QuestionSetsList = (await _questionSetService
                    .GetEntriesByThemaKeyIncludingNavigationsAsync(SelectedThema.Id))
                    .ToList();
            }
            else
            {
                QuestionSetsList.Clear();
            }
        }
    }
}