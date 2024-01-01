using CryptoApp.Models;

namespace CryptoApp.Business;

public interface IUserService
{
    void RegisterUser(string name, DateTime birthday, string email, string password, string phone, string dni, string nationality);
    void PrintAllUsers();
    bool CheckUserExist(string dni, string email, string phone);
    bool CheckUpdateUser(string fieldName, string newField);
    bool CheckLogin(string email, string password);
    User GetUser(string email);
    void DeleteUser(string email);
    void UpdateUser(string email, string newPhone = null, string newEmail = null, string NewPassword = null);
    void MakeDeposit(User user, string kind, string amount, string paymentMethod);
    void MakeWithdrawal(User user, string kind, string amount, string paymentMethod);
    void PrintAllTransactions(User user);
    string InputEmpty();
}
