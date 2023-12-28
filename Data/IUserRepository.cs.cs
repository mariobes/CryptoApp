using CryptoApp.Models;

namespace CryptoApp.Data;

public interface IUserRepository
{
    void AddUser(User user);

    Dictionary<string, User> GetAllUsers();
    void UpdateAccount(User user);
    void SaveChanges();
}
