using CryptoApp.Models;

namespace CryptoApp.Business;

public interface IUserService
{
    public User RegisterUser(UserCreateDTO userCreateDTO);
    public IEnumerable<User> GetAllUsers();
    public User GetUserById(int userId);
    public void UpdateUser(int userId, UserUpdateDTO userUpdateDTO);
    public void DeleteUser(int userId);

    public void MakeDeposit(DepositWithdrawalDTO depositWithdrawalDTO);
    public void Withdrawal(DepositWithdrawalDTO depositWithdrawalDTO);
    //public void SellCrypto(User user, Crypto crpto, string concept, string amount);
    //public void PrintAllTransactions(User user);
}
