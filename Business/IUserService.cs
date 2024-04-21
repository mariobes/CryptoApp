using CryptoApp.Models;

namespace CryptoApp.Business;

public interface IUserService
{
    public User RegisterUser(UserCreateDTO userCreateDTO);
    public IEnumerable<User> GetAllUsers();
    public User GetUserById(string userId);
    public void DeleteUser(string userId);
    public void UpdateUser(string userId, UserUpdateDTO userUpdateDTO);
}
