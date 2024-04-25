using CryptoApp.Models;

namespace CryptoApp.Business;

public interface IUserService
{
    public User RegisterUser(UserCreateDTO userCreateDTO);
    public IEnumerable<User> GetAllUsers();
    public User GetUserById(int userId);
    public void UpdateUser(int userId, UserUpdateDTO userUpdateDTO);
    public void DeleteUser(int userId);
}
