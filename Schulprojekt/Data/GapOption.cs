using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Schulprojekt.Data
{
    public class GapOption
    {
        [Key]
        public int GapOptionId { get; set; }
        public int GapId { get; set; }
        public string OptionText { get; set; } = string.Empty;
        public bool IsCorrect { get; set; } = false;
        public int OptionOrder { get; set; }
        //public int Points { get; set; } = 0;  // NEU Lückentext

        [ForeignKey(nameof(GapId))]
        [InverseProperty("GapOptions")]
        public GapField GapField { get; set; } = null!;
    }
}
