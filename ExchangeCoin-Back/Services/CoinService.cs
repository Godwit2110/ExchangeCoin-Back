using ExchangeCoin_Back.Data;
using ExchangeCoin_Back.Data.Entities;
using ExchangeCoin_Back.Data.Models_DTOs;
using Microsoft.VisualBasic;

namespace ExchangeCoin_Back.Services
{
    public class CoinService
    {
        private readonly ExchangeContext _context;
        private readonly UserService _userService;
        public CoinService(ExchangeContext context, UserService userService)
        {
            _context = context;
            _userService = userService;
        }

        public List<Coin> GetCoins()
        {
            return _context.Coin.ToList();
        }

        public Coin GetCoinsById(int id)
        {
            return _context.Coin.SingleOrDefault(c => c.Id == id);
        }

        public Coin GetCoinsByName(string name)
        {
            Coin Coin = _context.Coin.SingleOrDefault(c => c.denomination == name);
            return Coin;
        }

        public bool DeleteCoin(int id)
        {
            Coin CoinToDelete = GetCoinsById(id);
            if (CoinToDelete != null)
            {
                _context.Coin.Remove(CoinToDelete);
                _context.SaveChanges();
                return true;

            }
            return false;

        }



        public List<string> GetCoinsList()
        {
            return _context.Coin.Select(c => c.denomination).ToList();
        }

        public double Exchange(int userID, string cointochangeName, string coinchangedName, int amount)
        {
            Coin cointochange = GetCoinsByName(cointochangeName);
            Coin coinchanged = GetCoinsByName(coinchangedName);
            User user = _userService.GetUser(userID);
            double result;
            Subscription Subscription = _context.Subscriptions.SingleOrDefault(c => c.Id == user.SubscriptionId);
            int maxtrys = Subscription.MaxTrys;




            if (user.Trys < maxtrys)
            {
                result = (amount * cointochange.value) * coinchanged.value;

                user.Trys = user.Trys + 1;

                _context.Users.Update(user);

            }
            else result = -99;

            Exchange exchange = new Exchange()
            {

                Idcointochange = cointochange.Id,

                Idcoinchanged = coinchanged.Id,

                IdUser = user.Id,
                date = DateTime.Now,
            };

            _context.Exchange.Add(exchange);
            _context.SaveChanges();


            return result;
        }

        public void CreateCoin(CreateandUpdateCoinDTO dto)
        {
            Coin NewCoin = new Coin()
            {
                value = dto.value,
                denomination = dto.denomination,
                name = dto.name,

            };
            _context.Coin.Add(NewCoin);
            _context.SaveChanges();
        }

        public void UpdateCoin(CreateandUpdateCoinDTO dto, int CoinId)
        {
            Coin? UpdateCoin = _context.Coin.SingleOrDefault(Coin => Coin.Id == CoinId);
            if (UpdateCoin is not null)
            {
                UpdateCoin.value = dto.value;
                UpdateCoin.denomination = dto.denomination;
                UpdateCoin.name = dto.name;
                _context.SaveChanges();
            }

        }
    }
}
