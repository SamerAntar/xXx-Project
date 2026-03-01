using Microsoft.AspNetCore.Builder.Extensions;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Schulprojekt.Data
{
    public class GapField
    {
        [Key]
        public int GapId { get; set; }
        public int QuestionId { get; set; }
        public int GapIndex { get; set; }
        public GapInputType InputType { get; set; } = GapInputType.FREE_TEXT;
        public string? CorrectText { get; set; }
        public bool CaseSensitive { get; set; } = false;
        //public int Points { get; set; } = 1;  // NEU für Lückentext

        [ForeignKey(nameof(QuestionId))]
        [InverseProperty("GapFields")]
        public Question Question { get; set; } = null!;
        
        [InverseProperty("GapField")]
        public ICollection<GapOption> GapOptions { get; set; } = new List<GapOption>();
    }
}
