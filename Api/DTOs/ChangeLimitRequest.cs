using System.ComponentModel.DataAnnotations;

namespace Api.DTOs
{
    public class ChangeLimitRequest
    {
        [Required]
        [Range(0, double.MaxValue, ErrorMessage = "Available limit must be 0 or greater")]
        public decimal NewLimit { get; set; }
    }
}
