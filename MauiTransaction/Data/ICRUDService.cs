using MauiTransaction.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

using Transaction = MauiTransaction.Models.Transaction;

namespace MauiTransaction.Data
{
    public interface ICRUDService
    {
        Task<IEnumerable<TransactionDTO>> GetTransactionsAsync();
        Task<TransactionDTO> GetTransactionAsync(int id);
        Task<bool> SaveTransactionAsync(int id, Transaction transaction);
        Task<IEnumerable<Category>> GetCategoriesAsync();

        Task<decimal> GetTotal(int mode);
        Task<bool> DeleteTransactionAsync(int id);


    }
}
