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
        _repository.SaveChanges();
        return user;
    }

    public IEnumerable<User> GetAllUsers()
    {
        return _repository.GetAllUsers();
    }

    public User GetUserById(string userId)
    {
        var user = _repository.GetUser(userId);
        if (user == null)
        {
            throw new KeyNotFoundException($"El usuario con ID {userId} no existe.");
        }
        return user;
    }

    public void DeleteUser(string userId)
    {
        var user = _repository.GetUser(userId);
        if (user == null)
        {
            throw new KeyNotFoundException($"El usuario con ID {userId} no existe.");
        }

        _repository.DeleteUser(userId);
        _repository.SaveChanges();
    }
    
    public void UpdateUser(string userId, UserUpdateDTO userUpdateDTO)
    {
        var user = _repository.GetUser(userId);
        if (user == null)
        {
            throw new KeyNotFoundException($"El usuario con ID {userId} no existe.");
        }

        user.Email = userUpdateDTO.Email;
        user.Password = userUpdateDTO.Password;
        user.Phone = userUpdateDTO.Phone;
        _repository.UpdateUser(user);
        _repository.SaveChanges();
    }
    
}