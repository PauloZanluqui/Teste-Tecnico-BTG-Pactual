using System.ComponentModel.DataAnnotations;

namespace Api.DTOs
{
    public class CreateAccountRequest
    {
        public string DocumentNumber { get; set; }
        public string Agency { get; set; }
        public string AccountNumber { get; set; }
        public decimal AvailableLimit { get; set; }
    }
}
