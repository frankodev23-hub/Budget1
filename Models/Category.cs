using System.Transactions;

namespace Budget1.Models
{
    using System.ComponentModel.DataAnnotations;

    public class Category
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        [RegularExpression("^(Expense|Income)$")]
        public string Type { get; set; }

        public ICollection<Transaction> Transactions { get; set; }
    }

}
