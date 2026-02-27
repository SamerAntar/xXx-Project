namespace Schulprojekt.Services
{
    /// <summary>
    /// Verwaltet den aktuellen Zustand des Quiz, z. B. welche Fragen noch übrig sind
    /// und ob die Daten initial geladen wurden.
    /// </summary>
    public class QuizStateService
    {
        /// <summary>
        /// Enthält die IDs der Quizfragen, die noch nicht beantwortet wurden.
        /// HashSet wird genutzt, um Duplikate zu vermeiden und schnellen Zugriff zu ermöglichen.
        /// </summary>
        public HashSet<int> RemainingQuizIds { get; set; } = new();

        /// <summary>
        /// Gibt an, ob die Quizdaten bereits initialisiert wurden.
        /// Wird z. B. beim ersten Laden der Quizseite gesetzt.
        /// </summary>
        public bool IsInitialized { get; set; } = false;

        /// <summary>
        /// Setzt den gesamten Quizzustand zurück.
        /// Wird z. B. beim Neustart eines Quiz aufgerufen.
        /// </summary>
        public void Reset()
        {
            // Entfernt alle gespeicherten IDs
            RemainingQuizIds.Clear();

            // Markiert die Initialisierung als nicht erfolgt
            IsInitialized = false;
        }
    }
}