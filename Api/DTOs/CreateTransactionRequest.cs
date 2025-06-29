namespace Api.DTOs
{
    public class CreateTransactionRequest
    {
        public string AccountDocumentNumber { get; set; }
        public decimal TransactionValue { get; set; }
    }
}
