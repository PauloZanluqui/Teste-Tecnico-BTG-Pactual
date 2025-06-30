using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Api.DTOs;
using Api.Entities;

namespace api.Services
{
    public class TransactionService : ITransactionService
    {
        private readonly ITransactionRepository _transactionRepository;
        private readonly IAccountRepository _accountRepository;

        public TransactionService(ITransactionRepository transactionRepository, IAccountRepository accountRepository)
        {
            _transactionRepository = transactionRepository;
            _accountRepository = accountRepository;
        }

        public async Task<(Transaction, Account)> CreateTransactionAsync(CreateTransactionRequest request)
        {
            if (request == null) throw new ArgumentNullException(nameof(request));

            var transaction = new Transaction
            {
                TransactionId = Guid.NewGuid().ToString(),
                AccountDocumentNumber = request.AccountDocumentNumber,
                TransactionValue = request.TransactionValue,
                TransactionDate = DateTime.UtcNow,
            };

            var account = await _accountRepository.GetByKeyAsync(transaction.AccountDocumentNumber);
            if (account == null) throw new KeyNotFoundException("Account not found");

            if (transaction.TransactionValue < 0) throw new ArgumentException("Transaction value cannot be negative");

            if (transaction.TransactionValue > account.AvailableLimit) throw new InvalidOperationException("Insufficient funds for this transaction");

            account.AvailableLimit -= transaction.TransactionValue;
            await _transactionRepository.CreateAsync(transaction);
            await _accountRepository.CreateOrChangeAsync(account);

            return (transaction, account);
        }

        public async Task<Transaction> GetTransactionByIdAsync(string transactionId)
        {
            if (string.IsNullOrEmpty(transactionId)) throw new ArgumentNullException(nameof(transactionId));

            var transaction = await _transactionRepository.GetByKeyAsync(transactionId);
            if (transaction == null) throw new KeyNotFoundException("Transaction not found");

            return transaction;
        }

        public async Task<List<Transaction>> GetTransactionsAsync()
        {
            return await _transactionRepository.GetAllAsync();
        }
    }

    public interface ITransactionService
    {
        Task<(Transaction, Account)> CreateTransactionAsync(CreateTransactionRequest request);
        Task<Transaction> GetTransactionByIdAsync(string transactionId);
        Task<List<Transaction>> GetTransactionsAsync();
    }
}