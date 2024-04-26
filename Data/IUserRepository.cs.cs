using CryptoApp.Models;

namespace CryptoApp.Data;

public interface IUserRepository
{
    public void AddUser(User user);
    public User GetUser(int userId);
    public IEnumerable<User> GetAllUsers();
    public void DeleteUser(int userId);
    public void UpdateUser(User user);
    void SaveChanges();

    //void MakeDeposit(User user, string concept, string amount, string paymentMethod);
    //void MakeWithdrawal(User user, string concept, string amount, string paymentMethod);
    //void BuyCrypto(User user, Crypto crpto, string concept, string amount);
    //void SellCrypto(User user, Crypto crpto, string concept, string amount);
    //void PrintAllTransactions(User user);
}
