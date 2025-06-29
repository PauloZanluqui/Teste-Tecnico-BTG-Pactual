using Amazon.DynamoDBv2.DataModel;
using Api.Entities;

public class TransactionRepository : ITransactionRepository
{
    private readonly IDynamoDBContext _context;

    public TransactionRepository(IDynamoDBContext context)
    {
        _context = context;
    }

    public async Task CreateAsync(Transaction Transaction)
    {
        await _context.SaveAsync(Transaction);
    }

    public async Task<List<Transaction>> GetAllAsync()
    {
        return await _context.ScanAsync<Transaction>(new List<ScanCondition>()).GetRemainingAsync();
    }

    public async Task<Transaction> GetByKeyAsync(string transactionId)
    {
        return await _context.LoadAsync<Transaction>(transactionId);
    }
}

public interface ITransactionRepository
{
    Task CreateAsync(Transaction Transaction);
    Task<List<Transaction>> GetAllAsync();
    Task<Transaction> GetByKeyAsync(string transactionId);
}