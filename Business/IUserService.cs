using CryptoApp.Models;

namespace CryptoApp.Business;

public interface IUserService
{
    public User RegisterUser(UserCreateDTO userCreateDTO);
    public IEnumerable<User> GetAllUsers();
    public User GetUserById(int userId);
    public void UpdateUser(int userId, UserUpdateDTO userUpdateDTO);
    public void DeleteUser(int userId);
    User CheckLogin(string email, string password);

    public void MakeDeposit(DepositWithdrawalDTO depositWithdrawalDTO);
    public void MakeWithdrawal(DepositWithdrawalDTO depositWithdrawalDTO);
    public void BuyCrypto(BuySellCrypto buySellCrypto);
    public void SellCrypto(BuySellCrypto buySellCrypto);
    public IEnumerable<Transaction> GetAllTransactions(TransactionQueryParameters transactionQueryParameters);
    public Dictionary<string, double> MyCryptos(int userId);
}
