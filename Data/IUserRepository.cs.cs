using CryptoApp.Models;

namespace CryptoApp.Data;

public interface IUserRepository
{
    void AddUser(User user);
    Dictionary<string, User> GetAllUsers();
    void DeleteUser(User user);
    void UpdateUser(User user);
    void SaveChanges();
}
