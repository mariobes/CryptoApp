using Microsoft.EntityFrameworkCore;
using CryptoApp.Models;
using Microsoft.Extensions.Logging;

namespace CryptoApp.Data
{
    public class CryptoAppContext : DbContext
    {

        public CryptoAppContext(DbContextOptions<CryptoAppContext> options)
            : base(options)
        {

        }

        public DbSet<User> Users { get; set; }
        public DbSet<Crypto> Cryptos { get; set; }
        public DbSet<Transaction> Transactions { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().HasData(
                new User { Id = 1, Name = "Mario", Birthdate = new DateTime(2001, 1, 1), Email = "mario@gmail.com", Password = "mario12345", Phone = "4574", DNI = "32452464D", Nationality = "España", Cash = 0, Wallet = 0, Role = Roles.Admin},
                new User { Id = 2, Name = "Carlos", Birthdate = new DateTime(2003, 3, 3), Email = "carlos@gmail.com", Password = "carlos12345", Phone = "4567477", DNI = "23523562D", Nationality = "Argentina", Cash = 146, Wallet = 350 },
                new User { Id = 3, Name = "Fernando", Birthdate = new DateTime(2003, 3, 3), Email = "fernando@gmail.com", Password = "fernando12345", Phone = "4745", DNI = "23526445X", Nationality = "España", Cash = 0, Wallet = 0 },
                new User { Id = 4, Name = "Eduardo", Birthdate = new DateTime(2004, 4, 4), Email = "eduardo@gmail.com", Password = "eduardo12345", Phone = "4574548", DNI = "52353425D", Nationality = "España", Cash = 0, Wallet = 0 }
            );

            modelBuilder.Entity<Crypto>().HasData(
                new Crypto { Id = 1, Name = "Bitcoin", Symbol = "BTC", Description = "Bitcoin", RegisterDate = new DateTime(2024, 1, 3, 13, 54, 18), Value = 40000, Developer = "Bitcoin", Descentralized = true },
                new Crypto { Id = 2, Name = "Etherium", Symbol = "ETH", Description = "Etherium", RegisterDate = new DateTime(2024, 1, 3, 13, 54, 48), Value = 2000, Developer = "Etherium", Descentralized = true },
                new Crypto { Id = 3, Name = "Solana", Symbol = "SOL", Description = "Solana", RegisterDate = new DateTime(2024, 1, 3, 13, 55, 6), Value = 90, Developer = "Solana", Descentralized = true },
                new Crypto { Id = 4, Name = "Ripple", Symbol = "XRP", Description = "Ripple", RegisterDate = new DateTime(2024, 1, 3, 13, 55, 25), Value = 0.5, Developer = "Ripple", Descentralized = false }
            );

            modelBuilder.Entity<Transaction>().HasData(
                new Transaction { Id = 1, UserId = 2, CryptoId = null, Concept = "Ingreso", Amount = 500, Date = new DateTime(2024, 1, 3, 17, 52, 7), Charge = 1, Payment_Method = "Google Pay" },
                new Transaction { Id = 2, UserId = 2, CryptoId = 1, Concept = "Comprar Bitcoin", Amount = 100, Date = new DateTime(2024, 1, 3, 17, 54, 21), Charge = 1, Payment_Method = null },
                new Transaction { Id = 3, UserId = 2, CryptoId = 2, Concept = "Comprar Etherium", Amount = 100, Date = new DateTime(2024, 1, 3, 17, 54, 28), Charge = 1, Payment_Method = null },
                new Transaction { Id = 4, UserId = 2, CryptoId = 3, Concept = "Comprar Solana", Amount = 50, Date = new DateTime(2024, 1, 3, 17, 54, 33), Charge = 1, Payment_Method = null },
                new Transaction { Id = 5, UserId = 2, CryptoId = 1, Concept = "Comprar Bitcoin", Amount = 100, Date = new DateTime(2024, 1, 3, 17, 54, 37), Charge = 1, Payment_Method = null }
            );
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder
                .LogTo(Console.WriteLine, LogLevel.Information)
                .EnableSensitiveDataLogging(); // Opcional
        }
    }
}
