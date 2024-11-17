using ExchangeCoin_Back.Data;
using ExchangeCoin_Back.Data.Entities;
using ExchangeCoin_Back.Data.Models_DTOs;

namespace ExchangeCoin_Back.Services
{
    public class UserService
    {
        private readonly ExchangeContext _context;
        public UserService(ExchangeContext context)
        {
            _context = context;
        }

        public List<UserAdminDTO> GetUsers()
        {
            var users = _context.Users.Select(u => new UserAdminDTO
            {
                userName = u.Username,
                email = u.Email,
                role = u.Role,
                SubsId = u.SubscriptionId
            }).ToList();

            return users;
        }

        public User GetUser(int id)
        {
            return _context.Users.SingleOrDefault(c => c.Id == id);

        }

        public bool DeleteUser(int id)
        {
            User userToDelete = GetUser(id);
            if (userToDelete != null)
            {
                _context.Users.Remove(userToDelete);
                _context.SaveChanges();
                return true;

            }
            return false;

        }

        public User? GetByUserName(string Name)
        {
            return _context.Users.SingleOrDefault(u => u.Username == Name);
        }

        public bool ValidateCredentials(string Name, string password)
        {
            User? UserForLoggin = GetByUserName(Name);
            if (UserForLoggin != null)
            {
                if (UserForLoggin.Password != password)
                    return true;
            }
            return false;

        }

        public User? ValidateUser(AuthenticationRequestDTO authRequestBody)
        {
            return _context.Users.FirstOrDefault(p => p.Username == authRequestBody.Name && p.Password == authRequestBody.Password);
        }

        public Subscription GetSubscription(int id)
        {
            User user = GetUser(id);
            if (user != null)
            {
                Subscription Subscription = _context.Subscriptions.SingleOrDefault(s => s.Id == user.SubscriptionId);
                return Subscription;

            }


            return null;
        }

        public LoggedUserDTO GetLoggedUser(int id)
        {
            LoggedUserDTO LoggedUser = new LoggedUserDTO();
            User user = GetUser(id);
            if (user != null)
            {
                LoggedUser.Trys = user.Trys;
                LoggedUser.Role = user.Role;
                LoggedUser.Username = user.Username;

                return LoggedUser;

            }
            return LoggedUser;




        }

        public User ChangeSubscription(int idUser, int idSubs)
        {
            User user = GetUser(idUser);
            if (_context.Subscriptions.Any(c => c.Id == idSubs))
            {

                user.SubscriptionId = idSubs;
                _context.SaveChanges();
                return user;
            }
            else
            {
                return user;
            }
        }

        public void Create(CreateandUpdateUserDTO dto)
        {
            User newUser = new User()
            {
                Username = dto.Username,
                Email = dto.Email,
                Password = dto.Password,
                Trys = dto.Trys,
                SubscriptionId = dto.SubscriptionId,
                Role = dto.Role,

            };
            _context.Users.Add(newUser);
            _context.SaveChanges();
        }
    }
}
