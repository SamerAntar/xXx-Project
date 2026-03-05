using System.ComponentModel.DataAnnotations;

namespace Schulprojekt.Data
{
    public class Spieler
    {
        [Key]
        public int Id { get; set; }
        public string? Name { get; set; }
    }
}
