using Amazon.DynamoDBv2.DataModel;

namespace Api.Entities
{
    [DynamoDBTable("Transactions")]
    public class Transaction
    {
        [DynamoDBHashKey]
        public string TransactionId { get; set; } = Guid.NewGuid().ToString();

        [DynamoDBProperty]
        public string AccountDocumentNumber { get; set; }

        [DynamoDBProperty]
        public decimal TransactionValue { get; set; }

        [DynamoDBProperty]
        public DateTime TransactionDate { get; set; } = DateTime.UtcNow;
    }
}