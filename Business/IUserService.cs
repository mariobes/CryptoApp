namespace CryptoApp.Business;

public interface IUserService
{
    void RegisterUser(string name, DateTime birthday, string email, string password, string phone, string dni, string country);
}
