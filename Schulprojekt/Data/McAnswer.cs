using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Schulprojekt.Data
{
    public class McAnswer
    {
        [Key]
        public int Id { get; set; }
        public int QuestionId { get; set; }   
        public string OptionText { get; set; } = string.Empty;
        public int Points { get; set; } = 0;
        public bool IsCorrect { get; set; } = false;
        public int OptionOrder { get; set; }

        [ForeignKey(nameof(QuestionId))]
        [InverseProperty("McAnswers")]
        public Question Question { get; set; } = null!;
    }
}
