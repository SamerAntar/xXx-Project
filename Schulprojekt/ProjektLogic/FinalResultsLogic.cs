using Schulprojekt.Data;
using Schulprojekt.Services;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Schulprojekt.ProjektLogic
{
    public class FinalResultsLogic
    {
        private readonly IQuestionSetService _questionSetService;
        private readonly IQuestionSetProgressService _progressService;
        private readonly ICharacterService _characterService;

        public int SpielerId { get; set; }
        public int QuestionSetId { get; set; }
        public int CharacterId { get; set; }
        public string WebRootPath { get; set; } = "";

        public List<QuestionSetProgress> ProgressList { get; private set; } = new();
        public Character? EndCharacter { get; private set; }

        public double? TotalPoints { get; private set; }
        public double? TotalMax { get; private set; }
        public double? Percent { get; private set; }

        public string EndscreenImage { get; private set; } = "images/Endscreen_Normal/Endscreen_0_normal.png";
        public string EndscreenText { get; private set; } = "Du hast es geschafft, wir sind stolz auf dich!";

        public FinalResultsLogic(
            IQuestionSetService questionSetService,
            IQuestionSetProgressService progressService,
            ICharacterService characterService)
        {
            _questionSetService = questionSetService;
            _progressService = progressService;
            _characterService = characterService;
        }

        /// <author>Houman</author>
        /// <summary>
        /// Loads all final result data including progress, character info and total point calculation.
        /// </summary>
        /// <returns>A task representing the asynchronous load operation.</returns>
        public async Task LoadAsync()
        {
            ProgressList = (await _progressService.GetEntriesByPlayerId(SpielerId)).ToList();

            EndCharacter = await _characterService.GetEntryByKeyAsync(CharacterId);

            TotalPoints = ProgressList.Sum(x => x.Points);
            TotalMax = ProgressList.Sum(x => x.MaxPoints);

            Percent = TotalMax == 0 ? 0 : (TotalPoints / TotalMax) * 100;

            LoadEndScreen();
        }

        public string LicenseImagePath =>
            Percent switch
            {
                >= 90 => "images/Top_IHKunter_Lizenz.png",
                >= 75 => "images/Profi_IHKunter_Lizenz.png",
                >= 50 => "images/Normale_IHKunter_Lizenz.png",
                _ => "images/Normale_IHKunter_Lizenz.png"
            };

        public string MotivationText =>
            Percent switch
            {
                >= 90 => $"Hervorragende Leistung, Spieler! Sie haben ihre Top‑IHKunter‑Lizenz mehr als verdient.",
                >= 75 => $"Gute Arbeit! Damit haben Sie sogar ihre Profi‑IHKunter‑Lizenz erworben.",
                >= 50 => $"Glückwunsch zu Ihrer normalen IHKunter‑Lizenz.",
                _ => $"Glückwunsch zur Lizenz, aber übe weiter!"
            };

        private string EndKind =>
            Percent switch
            {
                >= 90 => "Top",
                >= 75 => "Profi",
                >= 50 => "Normal",
                _ => "Normal"
            };


        /// <author>Houman</author>
        /// <summary>
        /// Determines the correct end screen image and text depending on performance and available files.
        /// </summary>
        /// <returns>Returns nothing.</returns>
        public void LoadEndScreen()
        {
            string folder = $"Endscreen_{EndKind}";
            string file = $"Endscreen_{CharacterId}_{EndKind.ToLower()}.png";

            string path = Path.Combine(WebRootPath, "images", folder, file);

            if (File.Exists(path) && EndCharacter != null)
            {
                EndscreenImage = $"images/{folder}/{file}";

                EndscreenText = EndKind switch
                {
                    "Top" => EndCharacter?.TopEndText ?? "Kein Top-Endtext gefunden.",
                    "Profi" => EndCharacter?.ProfiEndText ?? "Kein Profi-Endtext gefunden.",
                    _ => EndCharacter?.NormalEndText ?? "Kein Normal-Endtext gefunden."
                };
            }
            else
            {
                EndscreenImage = "images/Endscreen_Normal/Endscreen_0_normal.png";
                EndscreenText = "Du hast es geschafft, wir sind stolz auf dich!";
            }
        }
    }
}