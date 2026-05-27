using System.ComponentModel.DataAnnotations;

namespace Budget1.Models
{
    public class Tithe
    {
        public int Id { get; set; }

        public int TransactionId { get; set; }  // FK correcto

        public string Name { get; set; }

        [Range(0.01, double.MaxValue, ErrorMessage = "Amount must be greater than zero.")]
        public decimal Amount { get; set; }

        public bool Payed { get; set; }

        public DateTime Date { get; set; }

        public Transaction Transaction { get; set; }
    }

}
