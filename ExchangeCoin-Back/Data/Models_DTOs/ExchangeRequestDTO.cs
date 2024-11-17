namespace ExchangeCoin_Back.Data.Models_DTOs
{
    public class ExchangeRequestDTO
    {
        public string cointochangeName { get; set; }
        public string coinchangedName { get; set; }
        public int amount { get; set; }
    }
}
