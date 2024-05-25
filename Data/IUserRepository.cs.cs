using CryptoApp.Models;

namespace CryptoApp.Data;

public interface IUserRepository
{
    public void AddUser(User user);
    public User GetUser(int userId);
    public User GetUserByEmail(string userEmail);
    public IEnumerable<User> GetAllUsers();
    public void DeleteUser(int userId);
    public void UpdateUser(User user);
    void SaveChanges();
    public void AddTransaction(Transaction transaction);
    public IEnumerable<Transaction> GetAllTransactions(TransactionQueryParameters transactionQueryParameters);
}
