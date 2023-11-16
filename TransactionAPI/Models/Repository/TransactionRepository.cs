
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

        
        public async Task<bool> AddTransactionAsync(Transaction transaction)
        {
            var getObjId = await _DbCon.Transactions.FirstOrDefaultAsync(
                x => x.Id == transaction.Id);

            if (getObjId != null)
                return false;

            try
            {
                _DbCon.Transactions.Add(transaction);
                await _DbCon.SaveChangesAsync();
            }
            catch
            {
                return false;
            }

            return true;

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

        public async Task<bool> EditTransactionAsync(int id, Transaction transaction)
        {
            var getObj = _DbCon.Transactions.FirstOrDefault(
                x => x.Id == id);

            if (getObj == null)
                return false;

            try
            {
                _DbCon.Transactions.Update(getObj);
                await _DbCon.SaveChangesAsync();
            }
            catch { return false; }

            return true;
            

        }

        public async Task<Transaction> GetTransactionAsync(int id)
        {
            var getObj = await _DbCon.Transactions.FirstOrDefaultAsync(x => x.Id == id);

            if(getObj == null)
                return null;

            return getObj;
                
        }

        public async Task<IEnumerable<Transaction>> GetTransactionsAsync()
        {
            var getObjs = _DbCon.Transactions.Select(x => x);

            return getObjs;
        }
    }
}
