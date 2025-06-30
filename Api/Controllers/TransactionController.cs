using api.Services;
using Api.DTOs;
using Api.Entities;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [ApiController]
    [Route("transaction")]
    public class TransactionController : ControllerBase
    {
        private readonly ITransactionService _transactionService;
        public TransactionController(ITransactionService transactionService)
        {
            _transactionService = transactionService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateTransactionAsync([FromBody] CreateTransactionRequest request)
        {
            try
            {
                var (transaction, account) = await _transactionService.CreateTransactionAsync(request);
                return CreatedAtAction(nameof(GetTransactionById), new { transactionId = transaction.TransactionId }, new { transaction, account.AvailableLimit, Status = "Transaction created successfully" });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            // return CreatedAtAction(nameof(GetTransactionById), new { transactionId = transaction.TransactionId }, new { transaction, account.AvailableLimit, Status = "Transaction created successfully" });
        }

        [HttpGet("{transactionId}")]
        public async Task<IActionResult> GetTransactionById(string transactionId)
        {
            try
            {
                var transaction = await _transactionService.GetTransactionByIdAsync(transactionId);
                return Ok(transaction);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetAllTransactionsAsync()
        {
            try
            {
                var transactions = await _transactionService.GetTransactionsAsync();
                return Ok(transactions);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
