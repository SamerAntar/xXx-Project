using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Schulprojekt.Data
{
    public class QuestionSet
    {
        [Key]
        public int Id { get; set; }
        
        [MaxLength(255)]
        public string Title { get; set; } = string.Empty;
        public int TeamId { get; set; }

        [ForeignKey(nameof(TeamId))]
        [InverseProperty("QuestionSets")]
        public Team? Team { get; set; }

        [InverseProperty("QuestionSet")]
        public ICollection<Question> Questions { get; set; } = new List<Question>();

    }
}
