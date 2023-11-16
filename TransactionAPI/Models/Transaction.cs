using System.ComponentModel.DataAnnotations;

namespace TransactionAPI.Models
{

    public class Transaction
    {
        [Key]
        public int Id { get; set; }
        public string Title { get; set; }

        public CategoryId CategoryId { get; set; }
        public Category Category { get; set; }


        public Decimal Value { get; set; }
        public DateTime Date { get; set; }

    }

    public class Category
    {
        public CategoryId categoryId { get; set; }
        public string Type { get; set; }

        public ICollection<Transaction> Transactions { get; set; }
    }


    public enum CategoryId
    {
        Ohters,
        Food,
        Transport,
    }

}
