using Amazon.DynamoDBv2.DataModel;

namespace Api.Entities
{
    [DynamoDBTable("Accounts")]
    public class Account
    {
        [DynamoDBHashKey]
        public string DocumentNumber { get; set; }

        [DynamoDBProperty]
        public string Agency { get; set; }

        [DynamoDBProperty]
        public string AccountNumber { get; set; }

        [DynamoDBProperty]
        public decimal AvailableLimit { get; set; }
    }
}