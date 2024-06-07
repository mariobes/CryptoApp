using CryptoApp.Models;

namespace CryptoApp.Business;

public interface ITransactionService
{
    public void MakeDeposit(DepositWithdrawalDTO depositWithdrawalDTO);
    public void MakeWithdrawal(DepositWithdrawalDTO depositWithdrawalDTO);
    public void BuyCrypto(BuySellCrypto buySellCrypto);
    public void SellCrypto(BuySellCrypto buySellCrypto);
    public IEnumerable<Transaction> GetAllTransactions(int userId, TransactionQueryParameters transactionQueryParameters);
    public Dictionary<string, double> MyCryptos(int userId);
}
