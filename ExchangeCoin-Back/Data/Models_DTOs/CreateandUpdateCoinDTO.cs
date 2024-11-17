using System.ComponentModel.DataAnnotations.Schema;

namespace ExchangeCoin_Back.Data.Models_DTOs
{
    public class CreateandUpdateCoinDTO
    {
        public string name { get; set; }

        public string denomination { get; set; }

        public double value { get; set; }
    }
}
