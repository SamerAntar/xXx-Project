using System.ComponentModel.DataAnnotations.Schema;

namespace Schulprojekt.Data
{
    public class Frage
    {
        public int? Id { get; set; }
        public string? Name { get; set; }
        public string? EndText { get; set; }

        [InverseProperty("Frage")]
        public virtual ICollection<Antwort> Antworten { get; set; } = new List<Antwort>();
    }
}
