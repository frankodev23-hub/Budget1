namespace Budget1.Models
{
    public class MonthlyBudget
    {
        public int Id { get; set; }
        public int Year { get; set; }
        public int Month { get; set; }
        public decimal BudgetAmount { get; set; }
        public int WalletId { get; set; }

        // Relaciones
        public Wallet Wallet { get; set; }
    }
}
