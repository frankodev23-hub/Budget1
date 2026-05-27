using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Budget1.Models
{
    public class SubTransaction
    {
        public int Id { get; set; }

        [Required]
        public int TransactionId { get; set; }

        [ForeignKey(nameof(TransactionId))]
        public Transaction Transaction { get; set; } = null!; // Relación obligatoria con Transaction

        [Required(ErrorMessage = "Name is required.")]
        [StringLength(100, ErrorMessage = "Name must be at most 100 characters.")]
        public string Name { get; set; } = string.Empty;

        [StringLength(250, ErrorMessage = "Description must be at most 250 characters.")]
        public string? Description { get; set; }

        [Range(0.01, double.MaxValue, ErrorMessage = "Amount must be greater than zero.")]
        public decimal Amount { get; set; }

        [NotMapped] // Para que EF ignore esta propiedad
        public string AmountString
        {
            get => Amount.ToString();
            set
            {
                if (decimal.TryParse(value, out var val))
                    Amount = val;
                else
                    Amount = 0;
            }
        }

        // Para el checkbox
        [NotMapped]
        public bool IsChecked { get; set; } = false;

        [DataType(DataType.Date)]
        [Required(ErrorMessage = "Date is required.")]
        public DateTime? Date { get; set; }
    }
}
