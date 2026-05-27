using System.Transactions;

namespace Budget1.Models
{
    public class Wallet
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Balance { get; set; }
        public decimal InitialBalance { get; set; }
        public DateTime CreatedDate { get; set; }
        public string Currency { get; set; } = "USD";

        public int Position { get; set; } // ✅ Orden en la lista

        // 👇 Nuevo: estilo/diseño de tarjeta
        public string Theme { get; set; } = "default";

        public ICollection<Transaction> Transactions { get; set; }
        public ICollection<MonthlyBudget> MonthlyBudgets { get; set; } = new List<MonthlyBudget>();
        public ICollection<Hold> Holds { get; set; }
    }

}
