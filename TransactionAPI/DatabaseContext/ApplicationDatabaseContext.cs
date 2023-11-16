using Microsoft.EntityFrameworkCore;
using System.Transactions;
using TransactionAPI.Models;

using Transaction = TransactionAPI.Models.Transaction;

namespace TransactionAPI.DatabaseContext
{
    public class ApplicationDatabaseContext : DbContext
    {
        public ApplicationDatabaseContext(DbContextOptions<ApplicationDatabaseContext> options)
            : base(options)
        {
            
        }

        public DbSet<Transaction> Transactions { get; set; }
        public DbSet<Category> Categories { get; set; }
    }
}
