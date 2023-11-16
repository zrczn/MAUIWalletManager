using Microsoft.AspNetCore.Mvc;
using TransactionAPI.Models;
using TransactionAPI.Models.Repository;

namespace TransactionAPI.Controllers
{

    [ApiController]
    [Route(nameof(Transaction))]
    public class TransactionController : Controller
    {
        private readonly ITransactionRepository _transactionRepository;

        public TransactionController(ITransactionRepository transactionRepository)
        {
            _transactionRepository = transactionRepository;
        }

        [HttpGet]
        [ProducesResponseType(200)]
        [ProducesResponseType(204)]
        public async Task<ActionResult<IEnumerable<Transaction>>> GetTransactions()
        {
            var Transactions = await _transactionRepository.GetTransactionsAsync();

            if (Transactions.Count() <= 0)
                return NoContent();

            return Ok(Transactions);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(200)]
        public async Task<ActionResult<Transaction>> GetTransaction(int id)
        {
            var getTransaction = await _transactionRepository.GetTransactionAsync(id);

            if (getTransaction == null)
                return BadRequest();

            return Ok(getTransaction);
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(200)]
        public async Task<IActionResult> DeleteTransaction(int id)
        {
            bool tryToDelete = await _transactionRepository.DeleteTransactionAsync(id);

            if (tryToDelete)
                return Ok();

            return BadRequest();
        }

        [HttpPost]
        [ProducesResponseType(204)]
        [ProducesResponseType(200)]
        public async Task<IActionResult> CreateTransaction([FromBody] Transaction transaction)
        {
            bool tryToCreateTransaction
                = await _transactionRepository.AddTransactionAsync(transaction);

            if (tryToCreateTransaction)
                return Ok();

            return BadRequest();
        }

        [HttpPut("{id}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(200)]
        public async Task<IActionResult> UpdateTransaction(int id, [FromBody] Transaction transaction)
        {
            bool tryToGetTransaction = await _transactionRepository.EditTransactionAsync(id, transaction);

            if(tryToGetTransaction)
                return Ok();
            return BadRequest();
        }

    }
}
