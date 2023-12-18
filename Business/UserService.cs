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
        User user = new User(name, birthday, email, password, phone, dni, country);
        _repository.AddUser(user);
        _repository.SaveChanges();
    }

    /*public bool checkUserExist(string dni, string email)
    {

    }*/
        


}