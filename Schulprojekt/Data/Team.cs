using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Schulprojekt.Data
{
    public class Team
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;

        [InverseProperty("Team")]
        public ICollection<QuestionSet> QuestionSets { get; set; } = new List<QuestionSet>();

    }
}
