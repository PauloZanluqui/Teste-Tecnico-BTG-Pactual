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

        public async Task CriarTabelaSeNaoExistirAsync()
        {
            var tableName = "Accounts";
            var tables = await _client.ListTablesAsync();

            if (tables.TableNames.Contains(tableName))
            {
                Console.WriteLine($"Tabela '{tableName}' já existe.");
                return;
            }

            var request = new CreateTableRequest
            {
                TableName = tableName,
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
            Console.WriteLine($"Tabela '{tableName}' criada com sucesso.");
        }
    }
}
