using System.ComponentModel.DataAnnotations;

namespace ExchangeCoin_Back.Data.Models_DTOs
{
    public class AuthenticationRequestDTO
    {
        [Required]
        public string? Name { get; set; }
        [Required]
        public string? Password { get; set; }
    }
}
