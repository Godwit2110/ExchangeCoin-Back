using System.ComponentModel.DataAnnotations.Schema;

namespace ExchangeCoin_Back.Data.Models_DTOs
{
    public class CreateandUpdateUserDTO
    {
        public string Username { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }

        public string Role { get; set; } = "Free";
        public int Trys { get; set; } = 0;

        [ForeignKey("SubscriptionId")]

        public int SubscriptionId { get; set; } = 1;
    }
}
