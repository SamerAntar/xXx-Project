using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Schulprojekt.Data
{
    public class Thema
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string? Description { get; set; }

        [InverseProperty("Thema")]
        public ICollection<QuestionSet> QuestionSets { get; set; } = new List<QuestionSet>();
    }
}
