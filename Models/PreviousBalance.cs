namespace Budget1.Models
{
    public class PreviousBalance
    {
        public int Id { get; set; }
        public int WalletId { get; set; }
        public Wallet Wallet { get; set; } = default!;

        public DateTime Month { get; set; } // Siempre será el primer día del mes (ej. 2025-06-01)
        public decimal Balance { get; set; } // Balance acumulado al cierre de ese mes
    }
}
