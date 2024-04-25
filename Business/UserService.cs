using System.Security.Cryptography;
using CryptoApp.Data;
using CryptoApp.Models;
using Microsoft.VisualBasic;

namespace CryptoApp.Business;

public class UserService : IUserService
{
    private readonly IUserRepository _repository;

    public UserService(IUserRepository repository)
    {
        _repository = repository;
    }

    public User RegisterUser(UserCreateDTO userCreateDTO)
    {
        User user = new(userCreateDTO.Name, userCreateDTO.Birthdate, userCreateDTO.Email, userCreateDTO.Password, userCreateDTO.Phone, userCreateDTO.DNI, userCreateDTO.Nationality);
        _repository.AddUser(user);
        return user;
    }

    public IEnumerable<User> GetAllUsers()
    {
        return _repository.GetAllUsers();
    }

    public User GetUserById(int userId)
    {
        var user = _repository.GetUser(userId);
        if (user == null)
        {
            throw new KeyNotFoundException($"Usuario con ID {userId} no encontrado");
        }
        return user;
    }
    
    public void UpdateUser(int userId, UserUpdateDTO userUpdateDTO)
    {
        var user = _repository.GetUser(userId);
        if (user == null)
        {
            throw new KeyNotFoundException($"Usuario con ID {userId} no encontrado");
        }
        user.Email = userUpdateDTO.Email;
        user.Password = userUpdateDTO.Password;
        user.Phone = userUpdateDTO.Phone;
        _repository.UpdateUser(user);
    }

    public void DeleteUser(int userId)
    {
        var user = _repository.GetUser(userId);
        if (user == null)
        {
            throw new KeyNotFoundException($"Usuario con ID {userId} no encontrado");
        }
        _repository.DeleteUser(userId);
    }
    
}