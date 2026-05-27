using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Budget1.Models
{
    public class QuickNote
    {
        public int Id { get; set; }

        [Required, StringLength(100)]
        public string Title { get; set; } = string.Empty;

        [StringLength(500)]
        public string Description { get; set; } = string.Empty;

        [DataType(DataType.Date)]
        public DateTime? Date { get; set; }

        // Categorías opcionales
        public int? CategoryId { get; set; }
        public QuickNoteCategory? Category { get; set; }

        // Checklist de items
        public ICollection<QuickNoteItem> Items { get; set; } = new List<QuickNoteItem>();

        // Para marcar como completada
        public bool IsCompleted { get; set; } = false;
    }

    public class QuickNoteCategory
    {
        public int Id { get; set; }
        [Required, StringLength(50)]
        public string Name { get; set; } = string.Empty;
        public ICollection<QuickNote> QuickNotes { get; set; } = new List<QuickNote>();
    }

    public class QuickNoteItem
    {
        public int Id { get; set; }

        [Required, StringLength(200)]
        public string Name { get; set; } = string.Empty;

        [StringLength(500)]
        public string Description { get; set; } = string.Empty;

        public bool Checked { get; set; } = false;

        public int QuickNoteId { get; set; }
        public QuickNote? QuickNote { get; set; }
    }
}
