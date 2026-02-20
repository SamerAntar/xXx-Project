using System.ComponentModel.DataAnnotations;

namespace Schulprojekt.Data
{
    public class Theme
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string? Description { get; set; }
        public ICollection<Question> Questions { get; set; } = new List<Question>();
    }
}
