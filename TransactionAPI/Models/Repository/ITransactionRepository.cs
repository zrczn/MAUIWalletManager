namespace TransactionAPI.Models.Repository
{
    public interface ITransactionRepository
    {
        Task<IEnumerable<Transaction>> GetTransactionsAsync();
        Task<Transaction> GetTransactionAsync(int id);
        Task<bool> DeleteTransactionAsync(int id);
        Task<bool> EditTransactionAsync(int id, Transaction transaction);
        Task<bool> AddTransactionAsync(Transaction transaction);

    }
}
