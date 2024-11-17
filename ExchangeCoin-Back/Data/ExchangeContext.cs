using ExchangeCoin_Back.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;

namespace ExchangeCoin_Back.Data
{
    public class ExchangeContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Subscription> Subscriptions { get; set; }
        public DbSet<Coin> Coin { get; set; }

        public DbSet<Exchange> Exchange { get; set; }


        public ExchangeContext(DbContextOptions<ExchangeContext> options) : base(options) 
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            Coin Dolar = new Coin()
            {
                Id = 2,
                name = "Dolar EEUU",
                denomination = "USD",
                value = 1,


            };
            Coin Peso = new Coin()
            {
                Id = 1,
                name = "Peso Argentino",
                denomination = "ARS",
                value = 0.002,


            };

            Coin Euro = new Coin()
            {
                Id = 3,
                name = "Euro",
                denomination = "EUR",
                value = 1.09,
            };

            Coin Pound = new Coin()
            {
                Id = 4,
                name = "British Pound",
                denomination = "LBS",
                value = 1.26,
            };


            Subscription Free = new Subscription()
            {
                Id = 1,
                Name = "Free",
                MaxTrys = 5,
            };

            Subscription Trial = new Subscription()
            {
                Id = 2,
                Name = "Trial",
                MaxTrys = 10,
            };

            Subscription Pro = new Subscription()
            {
                Id = 3,
                Name = "Pro",
                MaxTrys = 9999,
            };


            User Admin = new User()
            {
                Id = 1,
                Username = "Admin",
                Password = "123456",
                Email = "admin@admin.com",
                Trys = 0,
                Role = "ADMIN",

                SubscriptionId = Free.Id,


            };

            User alvaro = new User()
            {
                Id = 2,
                Username = "Alvaro",
                Password = "123456",
                Email = "alvarosuaya@gmail.com",
                Trys = 0,
                Role = "Free",
                SubscriptionId = Trial.Id,


            };

            Exchange Exchange1 = new Exchange()
            {
                Id = 1,
                Idcointochange = 1,
                Idcoinchanged = 2,
                IdUser = 1,
                date = DateTime.Now,


            };

            modelBuilder.Entity<User>()
            .HasOne(c => c.Subscription)
            .WithMany()
            .HasForeignKey(c => c.SubscriptionId);


            modelBuilder.Entity<User>().HasData(Admin, alvaro);

            modelBuilder.Entity<Subscription>().HasData(Free, Trial, Pro);

            modelBuilder.Entity<Coin>().HasData(Peso, Dolar, Pound, Euro);

            modelBuilder.Entity<Exchange>().HasData(Exchange1);


            base.OnModelCreating(modelBuilder);
        }
}
