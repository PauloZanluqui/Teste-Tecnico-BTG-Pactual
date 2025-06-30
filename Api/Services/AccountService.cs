using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Api.DTOs;
using Api.Entities;

namespace api.Services
{
    public class AccountService : IAccountService
    {
        private readonly IAccountRepository _accountRepository;

        public AccountService(IAccountRepository accountRepository)
        {
            _accountRepository = accountRepository;
        }

        public async Task<Account> CreateAccountAsync(CreateAccountRequest request)
        {
            var existingByDocumentNumber = await _accountRepository.GetByKeyAsync(request.DocumentNumber);
            if (existingByDocumentNumber != null) throw new InvalidOperationException("This CPF is already registered.");

            var duplicate = await _accountRepository.GetByAgencyAndAccountNumberAsync(request.Agency, request.AccountNumber);
            if (duplicate != null) throw new InvalidOperationException("There is already an account registered with that branch and number.");

            var account = new Account
            {
                DocumentNumber = request.DocumentNumber,
                Agency = request.Agency,
                AccountNumber = request.AccountNumber,
                AvailableLimit = request.AvailableLimit
            };

            await _accountRepository.CreateOrChangeAsync(account);
            return account;
        }

        public async Task<Account> GetAccountByDocumentAsync(string documentNumber)
        {
            var account = await _accountRepository.GetByKeyAsync(documentNumber);
            if (account == null) throw new KeyNotFoundException("Account not found.");
            return account;
        }

        public async Task<List<Account>> GetAccountsAsync()
        {
            return await _accountRepository.GetAllAsync();
        }

        public async Task<Account> ChangeAccountLimitAsync(string documentNumber, decimal newLimit)
        {
            var account = await _accountRepository.GetByKeyAsync(documentNumber);
            if (account == null) throw new KeyNotFoundException("Account not found.");

            if (newLimit < 0) throw new InvalidOperationException("The limit cannot be negative.");

            account.AvailableLimit = newLimit;
            await _accountRepository.CreateOrChangeAsync(account);
            return account;
        }

        public async Task DeleteAccountAsync(string documentNumber)
        {
            if (string.IsNullOrEmpty(documentNumber)) throw new InvalidOperationException("Document number is required.");

            var account = await _accountRepository.GetByKeyAsync(documentNumber);
            if (account == null) throw new KeyNotFoundException("Account not found.");

            await _accountRepository.DeleteAsync(documentNumber);
        }
    }

    public interface IAccountService
    {
        Task<Account> CreateAccountAsync(CreateAccountRequest request);
        Task<Account> GetAccountByDocumentAsync(string documentNumber);
        Task<List<Account>> GetAccountsAsync();
        Task<Account> ChangeAccountLimitAsync(string documentNumber, decimal newLimit);
        Task DeleteAccountAsync(string documentNumber);
    }
}