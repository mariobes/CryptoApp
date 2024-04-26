using CryptoApp.Models;
using Microsoft.EntityFrameworkCore;

namespace CryptoApp.Data
{
    public class UserEFRepository : IUserRepository
    {
        private readonly CryptoAppContext _context;

        public UserEFRepository(CryptoAppContext context)
        {
            _context = context;
        }

        public void AddUser(User user)
        {
            _context.Users.Add(user);
            SaveChanges();
        }

        public IEnumerable<User> GetAllUsers() 
        {
            var users = _context.Users;
            return users;
        }

        public User GetUser(int userId)
        {
            var user = _context.Users.FirstOrDefault(user => user.Id == userId);
            return user;
            //.Include(u => u.Transactions).ToList();     Habra que poner esto para que no de null, probablemente al debugear funcionara, y al devolverlo con la api dara eror de referencia circular, habra que utilizar DTOs
        }

        public void UpdateUser(User user)
        {
            _context.Entry(user).State = EntityState.Modified;
            SaveChanges();
        }

        public void DeleteUser(int userId) {
            var user = GetUser(userId);
            _context.Users.Remove(user);
            SaveChanges();
        }
        
        public void SaveChanges()
        {
            _context.SaveChanges();
        }

        public void AddTransaction(Transaction transaction)
        {
            _context.Transactions.Add(transaction);
            SaveChanges();
        }

    }   
}