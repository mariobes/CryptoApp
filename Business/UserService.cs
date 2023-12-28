using CryptoApp.Data;
using CryptoApp.Models;

namespace CryptoApp.Business;

public class UserService : IUserService
{
    private readonly IUserRepository _repository;

    public UserService(IUserRepository repository)
    {
        _repository = repository;
    }

    public void RegisterUser(string name, DateTime birthday, string email, string password, string phone, string dni, string country)
    {
        try 
        {
            User user = new User(name, birthday, email, password, phone, dni, country);
            _repository.AddUser(user);
            _repository.SaveChanges();
        }
        catch (Exception e)
        {
            throw new Exception("No se ha podido registrar el usuario", e);
        }

    }

    public bool checkUserExist(string dni, string email, string phone)
    {
        Dictionary<string, User> allUsers = _repository.GetAllUsers();
        foreach (var user in allUsers.Values)
        {
            if (user.DNI.Equals(dni, StringComparison.OrdinalIgnoreCase) || 
                user.Email.Equals(email, StringComparison.OrdinalIgnoreCase) ||
                user.Phone.Equals(phone, StringComparison.OrdinalIgnoreCase))
            {
                return true;
            }
        }
        
        return false;
    }

    public bool checkLogin(string email, string pasword)
    {
        Dictionary<string, User> allUsers = _repository.GetAllUsers();
        foreach (var user in allUsers.Values)
        {
            if (user.Email.Equals(email, StringComparison.OrdinalIgnoreCase) &&
                user.Password.Equals(pasword))
            {
                return true;
            }
        }
        return false;
    }
        


}