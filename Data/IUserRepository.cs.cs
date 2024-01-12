using CryptoApp.Models;

namespace CryptoApp.Data;

public interface IUserRepository
{
    void AddUser(User user);
    User GetUser(string email);
    Dictionary<string, User> GetAllUsers();
    void RemoveUser(User user);
    void UpdateUser(User user);
    void SaveChanges();
    void LogError(string message, Exception exception);
}
