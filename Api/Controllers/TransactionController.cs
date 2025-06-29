using Api.DTOs;
using Api.Entities;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [ApiController]
    [Route("transaction")]
    public class TransactionController : ControllerBase
    {
        private readonly ITransactionRepository _transactionRepository;
        private readonly IAccountRepository _accountRepository;

        public TransactionController(ITransactionRepository transactionRepository, IAccountRepository accountRepository)
        {
            _transactionRepository = transactionRepository;
            _accountRepository = accountRepository;
        }

        [HttpPost]
        public async Task<IActionResult> CreateTransactionAsync([FromBody] CreateTransactionRequest request)
        {
            if (request == null) return BadRequest("Transaction cannot be null");

            var transaction = new Transaction
            {
                TransactionId = Guid.NewGuid().ToString(),
                AccountDocumentNumber = request.AccountDocumentNumber,
                TransactionValue = request.TransactionValue,
                TransactionDate = DateTime.UtcNow,
            };

            // Validação 1 – Conta existe
            var account = await _accountRepository.GetByKeyAsync(transaction.AccountDocumentNumber);
            if (account == null) return NotFound("Account not found");

            // Validação 2 – Valor da transação não pode ser negativo
            if (transaction.TransactionValue < 0) return BadRequest("Transaction value cannot be negative");

            // Validação 3 – Verifica se o saldo da conta é suficiente
            if (transaction.TransactionValue > account.AvailableLimit) return BadRequest("Insufficient funds for this transaction");

            // Atualiza o saldo da conta
            account.AvailableLimit -= transaction.TransactionValue;
            await _transactionRepository.CreateAsync(transaction);
            await _accountRepository.CreateOrChangeAsync(account);
            return CreatedAtAction(nameof(GetTransactionById), new { transactionId = transaction.TransactionId }, new { transaction, account.AvailableLimit, Status = "Transaction created successfully" });
        }

        [HttpGet("{transactionId}")]
        public async Task<IActionResult> GetTransactionById(string transactionId)
        {
            var transaction = await _transactionRepository.GetByKeyAsync(transactionId);
            if (transaction == null)
                return NotFound("Transaction not found");

            return Ok(transaction);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllTransactionsAsync()
        {
            var transactions = await _transactionRepository.GetAllAsync();
            return Ok(transactions);
        }
    }
}
