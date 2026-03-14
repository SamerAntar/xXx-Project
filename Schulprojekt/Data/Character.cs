using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Schulprojekt.Data
{
    public class Character
    {
        [Key]
        public int CharacterID { get; set; }
        public string? Name { get; set; }
        
        [DefaultValue(0)]
        public int GettingByCompletingTheme { get; set; }
        public string? Backstory { get; set; }
        public string? NormalEndText { get; set; }
        public string? ProfiEndText { get; set; }
        public string? TopEndText { get; set; }

        [InverseProperty("Character")]
        public ICollection<QuestionSetProgress> QuestionSetProgresses { get; set; } = new List<QuestionSetProgress>();
    }
}
