using CryptoApp.Models;

namespace CryptoApp.Data;

public interface ITransactionRepository
{
    public void AddTransaction(Transaction transaction);
    public IEnumerable<Transaction> GetAllTransactions(int userId, TransactionQueryParameters transactionQueryParameters);
    void SaveChanges();
}
