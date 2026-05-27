using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Budget1.Models
{
    public class Hold
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }

        [Column("wallet_id")]
        public int WalletId { get; set; }

        [Column("month_hold")]
        public DateTime MonthHold { get; set; }

        [Column("balance_hold")]
        public decimal BalanceHold { get; set; }

        public Wallet Wallet { get; set; }
    }
}
