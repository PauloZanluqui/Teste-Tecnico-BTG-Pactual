using api.Services;
using Api.DTOs;
using Api.Entities;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [ApiController]
    [Route("account")]
    public class AccountController : ControllerBase
    {
        private readonly IAccountService _accountService;

        public AccountController(IAccountService accountService)
        {
            _accountService = accountService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateAccountAsync([FromBody] CreateAccountRequest request)
        {
            try
            {
                var account = await _accountService.CreateAccountAsync(request);
                return CreatedAtAction(nameof(GetAccountByDocument), new { documentNumber = account.DocumentNumber }, account);
            }
            catch (Exception ex)
            {
                return Conflict(ex.Message);
            }
        }

        [HttpGet("{documentNumber}")]
        public async Task<IActionResult> GetAccountByDocument(string documentNumber)
        {
            try
            {
                if (string.IsNullOrEmpty(documentNumber))
                    return BadRequest("Document number is required.");

                var account = await _accountService.GetAccountByDocumentAsync(documentNumber);
                return Ok(account);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetAllAccountsAsync()
        {
            try
            {
                var accounts = await _accountService.GetAccountsAsync();
                return Ok(accounts);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPatch("changelimit/{documentNumber}")]
        public async Task<IActionResult> ChangeLimitAsync(string documentNumber, [FromBody] ChangeLimitRequest request)
        {
            try
            {
                var account = await _accountService.ChangeAccountLimitAsync(documentNumber, request.NewLimit);

                return Ok(account);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{documentNumber}")]
        public async Task<IActionResult> DeleteAccountAsync(string documentNumber)
        {
            try
            {
                await _accountService.DeleteAccountAsync(documentNumber);
                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
