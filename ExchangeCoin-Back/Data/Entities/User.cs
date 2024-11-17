using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ExchangeCoin_Back.Data.Entities
{
    public class User
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Username { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }

        public string Role { get; set; }
        public int Trys { get; set; }

        [ForeignKey("SuscripcionId")]
        public Subscription Subscription { get; set; }
        public int SubscriptionId { get; set; }
    }
}
