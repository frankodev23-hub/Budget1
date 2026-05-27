using System.ComponentModel.DataAnnotations;

namespace Budget1.Models
{
    public class BudgetForecast
    {
        public int Id { get; set; }

        [Required]
        public int WalletId { get; set; }

        [Required]
        public int Year { get; set; }

        [Required]
        public int Month { get; set; }

        [Required]
        [StringLength(100)]
        public string TransactionName { get; set; } = string.Empty;

        [Range(0.01, double.MaxValue)]
        public decimal BaseAmount { get; set; }

        [Required]
        public bool IsIncome { get; set; }

        // Propiedades calculadas (no mapeadas)
        [System.ComponentModel.DataAnnotations.Schema.NotMapped]
        public decimal Daily => BaseAmount / 30m;

        [System.ComponentModel.DataAnnotations.Schema.NotMapped]
        public decimal Weekly => BaseAmount / 4m;

        [System.ComponentModel.DataAnnotations.Schema.NotMapped]
        public decimal FourWeeks => Daily * 28m;

        [System.ComponentModel.DataAnnotations.Schema.NotMapped]
        public decimal Monthly => BaseAmount;

        [System.ComponentModel.DataAnnotations.Schema.NotMapped]
        public decimal Quarterly => BaseAmount * 3m;

        [System.ComponentModel.DataAnnotations.Schema.NotMapped]
        public decimal Semester => BaseAmount * 6m;

        [System.ComponentModel.DataAnnotations.Schema.NotMapped]
        public decimal Yearly => BaseAmount * 12m;
    }
}
