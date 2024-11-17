using ExchangeCoin_Back.Data.Entities;

namespace ExchangeCoin_Back.Data.Models_DTOs
{
    public class ExchangeDTO
    {
        public User username { get; set; }
        public Coin cointochange { get; set; }
        public Coin coinchanged { get; set; }
        public int amount { get; set; }
    }
}
