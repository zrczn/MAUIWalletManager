namespace TransactionAPI.Models.Repository
{
    public interface ITransactionRepository
    {
        Task<IEnumerable<TransactionDTO>> GetTransactionsAsync();
        Task<TransactionDTO> GetTransactionAsync(int id);
        Task<bool> DeleteTransactionAsync(int id);
        Task<bool> SaveTransactionAsync(int id, Transaction transaction);
        Task<IEnumerable<Category>> GetCategoriesAsync();

        Task<decimal> GetTotalMoneyAsync();
        Task<decimal> GetTotalIncome();
        Task<decimal> GetTotalOutcome();

    }
}
