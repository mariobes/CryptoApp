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
    void MakeDeposit(User user, string concept, string amount, string paymentMethod);
    void MakeWithdrawal(User user, string concept, string amount, string paymentMethod);
    void BuyCrypto(User user, Crypto crpto, string concept, string amount);
    void SellCrypto(User user, Crypto crpto, string concept, string amount);
    void PrintAllTransactions(User user);
    void PrintAllCryptosPurchase(User user);
    void UpdateUserWallet(Crypto crypto, double oldCryptoValue, double NewCryptoValue);
    bool CheckCryptoPurchased(Crypto crypto);
    string InputEmpty();
}
