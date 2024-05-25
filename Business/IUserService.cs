using CryptoApp.Models;

namespace CryptoApp.Business;

public interface IUserService
{
    public User RegisterUser(UserCreateDTO userCreateDTO);
    public IEnumerable<User> GetAllUsers();
    public User GetUserById(int userId);
    public User GetUserByEmail(string userEmail);
    public void UpdateUser(int userId, UserUpdateDTO userUpdateDTO);
    public void DeleteUser(int userId);

    public void MakeDeposit(DepositWithdrawalDTO depositWithdrawalDTO);
    public void MakeWithdrawal(DepositWithdrawalDTO depositWithdrawalDTO);
    public void BuyCrypto(BuySellCrypto buySellCrypto);
    public void SellCrypto(BuySellCrypto buySellCrypto);
    public IEnumerable<Transaction> GetAllTransactions(TransactionQueryParameters transactionQueryParameters);
    public Dictionary<string, double> MyCryptos(int userId);
}
