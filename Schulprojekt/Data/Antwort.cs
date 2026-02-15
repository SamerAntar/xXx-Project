using System.ComponentModel.DataAnnotations.Schema;

namespace Schulprojekt.Data
{
    public class Antwort
    {
        public int? Id { get; set; }
        public string? Name { get; set; }
        public int? Punkt { get; set; }

        [Column("FrageId")]
        public int FrageId { get; set; }

        [ForeignKey("FrageId")]
        [InverseProperty("Antworten")]
        public virtual Frage? Frage { get; set; }
    }
}
