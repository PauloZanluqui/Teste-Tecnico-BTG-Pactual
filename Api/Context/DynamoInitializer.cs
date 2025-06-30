using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.Model;

namespace Api.Context
{
    public class DynamoInitializer
    {
        private readonly IAmazonDynamoDB _client;

        public DynamoInitializer(IAmazonDynamoDB client)
        {
            _client = client;
        }

        public async Task CreateTablesIfNotExistsAsync()
        {
            var existingTables = (await _client.ListTablesAsync()).TableNames;

            if (!existingTables.Contains("Accounts"))
                await CreateTableAccounts();

            if (!existingTables.Contains("Transactions"))
                await CreateTablesTransactions();
        }

        private async Task CreateTableAccounts()
        {
            var request = new CreateTableRequest
            {
                TableName = "Accounts",
                AttributeDefinitions = new List<AttributeDefinition>
                {
                    new AttributeDefinition("DocumentNumber", ScalarAttributeType.S)
                },
                KeySchema = new List<KeySchemaElement>
                {
                    new KeySchemaElement("DocumentNumber", KeyType.HASH)
                },
                ProvisionedThroughput = new ProvisionedThroughput
                {
                    ReadCapacityUnits = 5,
                    WriteCapacityUnits = 5
                }
            };

            await _client.CreateTableAsync(request);
            Console.WriteLine($"Table 'Accounts' created successfully.");
        }

        private async Task CreateTablesTransactions()
        {
            var request = new CreateTableRequest
            {
                TableName = "Transactions",
                AttributeDefinitions = new List<AttributeDefinition>
                {
                    new AttributeDefinition("TransactionId", ScalarAttributeType.S)
                },
                KeySchema = new List<KeySchemaElement>
                {
                    new KeySchemaElement("TransactionId", KeyType.HASH)
                },
                ProvisionedThroughput = new ProvisionedThroughput
                {
                    ReadCapacityUnits = 5,
                    WriteCapacityUnits = 5
                }
            };

            await _client.CreateTableAsync(request);
            Console.WriteLine($"Table 'Transactions' created successfully.");
        }

    }
}
