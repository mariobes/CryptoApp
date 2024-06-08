using CryptoApp.Data;
using CryptoApp.Models;

namespace CryptoApp.Business;

public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;

    public UserService(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public User RegisterUser(UserCreateDTO userCreateDTO)
    {
        var registeredUser = _userRepository.GetAllUsers().FirstOrDefault(u => u.Email.Equals(userCreateDTO.Email, StringComparison.OrdinalIgnoreCase));
        if (registeredUser != null)
        {
            throw new Exception("El correo electrónico ya está registrado.");
        }
        User user = new(userCreateDTO.Name, userCreateDTO.Birthdate, userCreateDTO.Email, userCreateDTO.Password, userCreateDTO.Phone, userCreateDTO.DNI, userCreateDTO.Nationality);
        _userRepository.AddUser(user);
        return user;
    }

    public IEnumerable<User> GetAllUsers()
    {
        return _userRepository.GetAllUsers();
    }

    public User GetUserById(int userId)
    {
        var user = _userRepository.GetUser(userId);
        if (user == null)
        {
            throw new KeyNotFoundException($"Usuario con ID {userId} no encontrado");
        }
        return user;
    }

    public User GetUserByEmail(string userEmail)
    {
        var user = _userRepository.GetUserByEmail(userEmail);
        if (user == null)
        {
            throw new KeyNotFoundException($"Usuario con email {userEmail} no encontrado");
        }
        return user;
    }
    
    public void UpdateUser(int userId, UserUpdateDTO userUpdateDTO)
    {
        var user = _userRepository.GetUser(userId);
        if (user == null)
        {
            throw new KeyNotFoundException($"Usuario con ID {userId} no encontrado");
        }

        var registeredUser = _userRepository.GetAllUsers().FirstOrDefault(u => u.Email.Equals(userUpdateDTO.Email, StringComparison.OrdinalIgnoreCase));
        if (registeredUser != null)
        {
            throw new Exception("El correo electrónico ya está registrado.");
        }

        user.Email = userUpdateDTO.Email;
        user.Password = userUpdateDTO.Password;
        user.Phone = userUpdateDTO.Phone;
        _userRepository.UpdateUser(user);
    }

    public void DeleteUser(int userId)
    {
        var user = _userRepository.GetUser(userId);
        if (user == null)
        {
            throw new KeyNotFoundException($"Usuario con ID {userId} no encontrado");
        }
        _userRepository.DeleteUser(userId);
    }
    
}