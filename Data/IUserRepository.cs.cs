using CryptoApp.Models;

namespace CryptoApp.Data;

public interface IUserRepository
{
    void AddUser(User user);

    void UpdateAccount(User user);
    void SaveChanges();

}
