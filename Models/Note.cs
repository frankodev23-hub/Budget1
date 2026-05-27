
using System.ComponentModel.DataAnnotations.Schema;

namespace Budget1.Models
{
    public class Note
    {
        public int Id { get; set; }
        [Column(TypeName = "nvarchar(max)")]
        public string? Content { get; set; }  // HTML del editor
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime? UpdatedAt { get; set; }
    }

}
