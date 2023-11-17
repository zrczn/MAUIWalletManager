using System.ComponentModel.DataAnnotations;

namespace MauiTransaction.Models
{
    public class TransactionDTO
    {
        public int Id { get; set; }
        public string Title { get; set; }

        public string CategoryName { get; set; }

        public Decimal Value { get; set; }
        public DateTime Date { get; set; }

    }
}
