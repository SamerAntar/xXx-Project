using System.ComponentModel.DataAnnotations;

namespace Schulprojekt.Data
{
    public class QuestionSetProgress
    {
        [Key]
        public int Id { get; set; }
        public int SpielerId { get; set; }
        public int QuestionSetId { get; set; }
        public int Points { get; set; }
        public int MaxPoints { get; set; }
        public bool IsPassed { get; set; }
        public DateTime CompletedAt { get; set; } = DateTime.Now;
    }
}
