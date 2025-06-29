using Amazon;
using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DataModel;
using Amazon.Runtime;
using Api.Context;

var builder = WebApplication.CreateBuilder(args);

var awsOptions = builder.Configuration.GetSection("AWS");
var credentials = new BasicAWSCredentials(
    awsOptions["AccessKey"],
    awsOptions["SecretKey"]
);

var config = new AmazonDynamoDBConfig
{
    ServiceURL = awsOptions["ServiceURL"],
};

builder.Services.AddSingleton<IAmazonDynamoDB>(new AmazonDynamoDBClient(credentials, config));
builder.Services.AddSingleton<IDynamoDBContext, DynamoDBContext>();

builder.Services.AddScoped<IAccountRepository, AccountRepository>();
builder.Services.AddScoped<ITransactionRepository, TransactionRepository>();
builder.Services.AddSingleton<DynamoInitializer>();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var initializer = scope.ServiceProvider.GetRequiredService<DynamoInitializer>();
    await initializer.CriarTabelasSeNaoExistiremAsync();
}

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();