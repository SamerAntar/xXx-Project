using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Schulprojekt.Data
{
    public class Question
    {
        [Key]
        public int Id { get; set; }
        public string? StartText { get; set; }
        public string? ImageUrl { get; set; }
        public string? EndText { get; set; }
        public bool AllowsMultiple { get; set; }
        public QuestionType QuestionType { get; set; }
        public int QuestionSetId { get; set; }

        [ForeignKey(nameof(QuestionSetId))]
        [InverseProperty("Questions")]
        public virtual QuestionSet? QuestionSet { get; set; }

        [InverseProperty("Question")]
        public ICollection<McAnswer> McAnswers { get; set; } = new List<McAnswer>();
       
        [InverseProperty("Question")]
        public ICollection<GapField> GapFields { get; set; } = new List<GapField>();

        [NotMapped]
        public McAnswer? selectedAnswer { get; set; }

        // GAP: Mohammed
        // GAP: User-Eingaben pro GapId (mehrere Lücken pro Frage)
        [NotMapped]
        public Dictionary<int, string> GapUserInputs { get; set; } = new();

        // GAP: Bewertung pro Frage (damit Punkte nicht mehrfach addiert werden)
        [NotMapped]
        public bool GapEvaluated { get; set; } = false;

        [NotMapped]
        public bool GapIsCorrect { get; set; } = false;
    }
}
