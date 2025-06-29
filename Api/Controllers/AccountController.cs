using Api.DTOs;
using Api.Entities;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [ApiController]
    [Route("account")]
    public class AccountController : ControllerBase
    {
        private readonly IAccountRepository _accountRepository;

        public AccountController(IAccountRepository accountRepository)
        {
            _accountRepository = accountRepository;
        }

        [HttpPost]
        public async Task<IActionResult> CreateAccountAsync([FromBody] Account account)
        {
            if (account == null) return BadRequest("Account cannot be null");

            // Validação 1 – CPF (DocumentNumber) já existe
            var existingByCpf = await _accountRepository.GetByKeyAsync(account.DocumentNumber);
            if (existingByCpf != null) return Conflict("This CPF is already registered.");

            // Validação 2 – Agência + Conta já existe
            var allAccounts = await _accountRepository.GetAllAsync();
            var duplicate = allAccounts.FirstOrDefault(a => a.Agency == account.Agency && a.AccountNumber == account.AccountNumber);

            if (duplicate != null) return Conflict("There is already an account registered with that branch and number.");

            await _accountRepository.CreateOrChangeAsync(account);

            return CreatedAtAction(nameof(GetAccountByDocument), new { documentNumber = account.DocumentNumber }, account);
        }

        [HttpGet("{documentNumber}")]
        public async Task<IActionResult> GetAccountByDocument(string documentNumber)
        {
            var account = await _accountRepository.GetByKeyAsync(documentNumber);
            if (account == null)
                return NotFound();

            return Ok(account);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllAccountsAsync()
        {
            var accounts = await _accountRepository.GetAllAsync();
            return Ok(accounts);
        }

        [HttpPatch("changelimit/{documentNumber}")]
        public async Task<IActionResult> ChangeLimitAsync(string documentNumber, [FromBody] ChangeLimitRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var account = await _accountRepository.GetByKeyAsync(documentNumber);
            if (account == null)
                return NotFound("Account not found");

            account.AvailableLimit = request.NewLimit;
            await _accountRepository.CreateOrChangeAsync(account);

            return Ok(account);
        }

        [HttpDelete("{documentNumber}")]
        public async Task<IActionResult> DeleteAccountAsync(string documentNumber)
        {
            var account = await _accountRepository.GetByKeyAsync(documentNumber);
            if (account == null)
                return NotFound("Account not found");

            await _accountRepository.DeleteAsync(documentNumber);
            return NoContent();
        }
    }
}
