using Amazon.DynamoDBv2.DataModel;
using Amazon.DynamoDBv2.DocumentModel;
using Api.Entities;

public class AccountRepository : IAccountRepository
{
    private readonly IDynamoDBContext _context;

    public AccountRepository(IDynamoDBContext context)
    {
        _context = context;
    }

    public async Task CreateOrChangeAsync(Account account)
    {
        await _context.SaveAsync(account);
    }

    public async Task<List<Account>> GetAllAsync()
    {
        return await _context.ScanAsync<Account>(new List<ScanCondition>()).GetRemainingAsync();
    }

    public async Task<Account> GetByKeyAsync(string DocumentNumber)
    {
        return await _context.LoadAsync<Account>(DocumentNumber);
    }

    public async Task<Account?> GetByAgencyAndAccountNumberAsync(string agency, string accountNumber)
    {
        var conditions = new List<ScanCondition>
        {
            new ScanCondition("Agency", ScanOperator.Equal, agency),
            new ScanCondition("AccountNumber", ScanOperator.Equal, accountNumber)
        };

        var accounts = await _context.ScanAsync<Account>(conditions).GetRemainingAsync();
        return accounts.FirstOrDefault();
    }

    public async Task DeleteAsync(string documentNumber)
    {
        await _context.DeleteAsync<Account>(documentNumber);
    }
}

public interface IAccountRepository
{
    Task CreateOrChangeAsync(Account account);
    Task<List<Account>> GetAllAsync();
    Task<Account> GetByKeyAsync(string documentNumber);
    Task<Account?> GetByAgencyAndAccountNumberAsync(string agency, string accountNumber);
    Task DeleteAsync(string documentNumber);
}