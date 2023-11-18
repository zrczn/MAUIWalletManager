
using Microsoft.EntityFrameworkCore;
using TransactionAPI.DatabaseContext;

namespace TransactionAPI.Models.Repository
{
    public class TransactionRepository : ITransactionRepository
    {
        private readonly ApplicationDatabaseContext _DbCon;

        public TransactionRepository(ApplicationDatabaseContext Dbcon)
        {
            _DbCon = Dbcon;
        }

        public async Task<IEnumerable<Category>> GetCategoriesAsync()
        {
            var getObjs = await _DbCon.Categories.Select(x => x).ToListAsync();

            if (getObjs == null)
                return null;

            return getObjs;
        }

        public async Task<bool> SaveTransactionAsync(int id, Transaction transaction)
        {
            var existingTransaction = await _DbCon.Transactions.FirstOrDefaultAsync(x => x.Id == id);

            if (existingTransaction != null)
            {
                existingTransaction.Title = transaction.Title;
                existingTransaction.Date = transaction.Date;
                existingTransaction.Value = transaction.Value;
                existingTransaction.CategoryId = transaction.CategoryId;
            }
            else
            {
                var newTransaction = new Transaction
                {
                    Title = transaction.Title,
                    Date = transaction.Date,
                    Value = transaction.Value,
                    CategoryId = transaction.CategoryId
                };

                await _DbCon.Transactions.AddAsync(newTransaction);
            }

            try
            {
                await _DbCon.SaveChangesAsync();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<bool> DeleteTransactionAsync(int id)
        {
            var getObj = await _DbCon.Transactions.FirstOrDefaultAsync(
                x => x.Id == id);

            if (getObj == null)
                return false;

            try
            {
                _DbCon.Transactions.Remove(getObj);
                await _DbCon.SaveChangesAsync();
            }
            catch { return false; }

            return true;
        }

        public async Task<TransactionDTO> GetTransactionAsync(int id)
        {
            var getObj = await _DbCon.Transactions.FirstOrDefaultAsync(x => x.Id == id);

            if (getObj == null)
                return null;

            var getCategoryName = await _DbCon.Categories.FirstOrDefaultAsync(x => x.Id == getObj.CategoryId);

            TransactionDTO transactionDTO = new TransactionDTO()
            {
                Id = getObj.Id,
                CategoryName = getCategoryName.Name,
                Date = getObj.Date,
                Title = getObj.Title,
                Value = getObj.Value
            };

            return transactionDTO;

        }

        public async Task<IEnumerable<TransactionDTO>> GetTransactionsAsync()
        {
            var getObjs = await _DbCon.Transactions
                .Include(q => q.Category)
                .Select(x => new TransactionDTO()
                {
                    Id = x.Id,
                    Date = x.Date,
                    Title = x.Title,
                    Value = x.Value,
                    CategoryName = x.Category.Name
                }).ToListAsync();

            return getObjs;
        }

        public async Task<decimal> GetTotalMoneyAsync()
            => await _DbCon.Transactions.SumAsync(x => x.Value);

        public async Task<decimal> GetTotalIncome()
            => await _DbCon.Transactions.Select(x => x.Value).Where(y => y > 0).SumAsync(z => z);

        public async Task<decimal> GetTotalOutcome()
            => await _DbCon.Transactions.Select(x => x.Value).Where(y => y < 0).SumAsync(z => z);
    }
}
