using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Schulprojekt.Data
{
    public class QuestionSetProgress
    {
        [Key]
        public int Id { get; set; }
        public string? Topic { get; set; }
        public int SpielerId { get; set; }
        public int QuestionSetId { get; set; }
        public int ThemaId { get; set; }
        public int? CharacterId { get; set; }
        public int? Points { get; set; }
        public double? MaxPoints { get; set; }
        public bool IsPassed { get; set; }
        public DateTime CompletedAt { get; set; } = DateTime.Now;
        
        [ForeignKey(nameof(CharacterId))]
        [InverseProperty("QuestionSetProgresses")]
        public virtual Character? Character { get; set; }
    }
}
