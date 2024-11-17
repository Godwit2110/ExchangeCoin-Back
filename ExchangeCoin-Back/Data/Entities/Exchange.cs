using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ExchangeCoin_Back.Data.Entities
{
    public class Exchange
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [ForeignKey("Idcointochange")]

        public Coin cointochange { get; set; }
        public int Idcointochange { get; set; }

        [ForeignKey("Idcoinchanged")]

        public Coin coinchanged { get; set; }
        public int Idcoinchanged { get; set; }
        public DateTime date { get; set; }

        [ForeignKey("IdUser")]
        public User Userexchange { get; set; }
        public int IdUser { get; set; }
    }
}
