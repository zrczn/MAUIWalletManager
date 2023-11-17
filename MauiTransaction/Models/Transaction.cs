using System.ComponentModel.DataAnnotations;
using System.Reflection.PortableExecutable;

namespace MauiTransaction.Models
{

    public class Transaction
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Title { get; set; }

        public int CategoryId { get; set; }
        public Category Category { get; set; }

        [Required]
        public Decimal Value { get; set; }
        public DateTime Date { get; set; }

    }
}
