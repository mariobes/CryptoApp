using CryptoApp.Models;

namespace CryptoApp.Data;

public interface IUserRepository
{
    public void AddUser(User user);
    public User GetUser(string userId);
    public IEnumerable<User> GetAllUsers();
    public void DeleteUser(string userId);
    public void UpdateUser(User user);
    void SaveChanges();
}
