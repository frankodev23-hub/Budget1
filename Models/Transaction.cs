using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Budget1.Models
{
    public class Transaction
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Wallet is required.")]
        public int WalletId { get; set; }

        public Wallet? Wallet { get; set; }

        [Required(ErrorMessage = "Name is required.")]
        [StringLength(100, ErrorMessage = "Name must be at most 100 characters.")]
        public string Name { get; set; }

        [StringLength(250, ErrorMessage = "Description must be at most 250 characters.")]
        public string? Description { get; set; }

        [Range(0.01, double.MaxValue, ErrorMessage = "Amount must be greater than zero.")]
        public decimal Amount { get; set; }

        [DataType(DataType.Date)]
        [Required(ErrorMessage = "Date is required.")]
        public DateTime Date { get; set; }

        [Column("is_income")]
        public bool IsIncome { get; set; }

        public bool IsUnexpected { get; set; }

        [Required(ErrorMessage = "Category is required.")]
        public int CategoryId { get; set; }

        public Category? Category { get; set; }

        public bool IsRecurring { get; set; }

        public int? RecurrenceIntervalId { get; set; }

        [ForeignKey(nameof(RecurrenceIntervalId))]
        public RecurrenceInterval? RecurrenceInterval { get; set; }

        public DateTime? RecurrenceEndDate { get; set; }

        public RecurrenceEndType? RecurrenceEndType { get; set; }

        public int? RecurrenceCount { get; set; }

        public int? OriginalTransactionId { get; set; }

        [ForeignKey(nameof(OriginalTransactionId))]
        public Transaction? OriginalTransaction { get; set; }

        public ICollection<Tithe> Tithes { get; set; } = new List<Tithe>();

        public bool Tithe { get; set; }

        // Relación con subtransacciones
        public List<SubTransaction> SubTransactions { get; set; } = new();

        // Propiedad calculada (no mapeada en DB) para validar consistencia
        [NotMapped]
        public decimal SubTransactionsTotal => SubTransactions?.Sum(st => st.Amount) ?? 0;
    }

}
