using CryptoApp.Data;
using CryptoApp.Models;

namespace CryptoApp.Business;

public class TransactionService : ITransactionService
{
    private readonly IUserRepository _userRepository;
    private readonly ICryptoRepository _cryptoRepository;
    private readonly ITransactionRepository _transactionRepository;

    public TransactionService(IUserRepository userRepository, ICryptoRepository cryptoRepository, ITransactionRepository transactionRepository)
    {
        _userRepository = userRepository;
        _cryptoRepository = cryptoRepository;
        _transactionRepository = transactionRepository;
    }

    public void MakeDeposit(DepositWithdrawalDTO depositWithdrawalDTO)
    {
        var user = _userRepository.GetUser(depositWithdrawalDTO.UserId);
        if (user == null)
        {
            throw new KeyNotFoundException($"Usuario con ID {depositWithdrawalDTO.UserId} no encontrado");
        }

        Transaction transaction = new(depositWithdrawalDTO.UserId, "Ingresar dinero", depositWithdrawalDTO.Amount, depositWithdrawalDTO.PaymentMethod);
        user.Cash += depositWithdrawalDTO.Amount;
         _userRepository.UpdateUser(user);
         _transactionRepository.AddTransaction(transaction);
    }

    public void MakeWithdrawal(DepositWithdrawalDTO depositWithdrawalDTO)
    {
        var user = _userRepository.GetUser(depositWithdrawalDTO.UserId);
        if (user == null)
        {
            throw new KeyNotFoundException($"Usuario con ID {depositWithdrawalDTO.UserId} no encontrado");
        }

        if (user.Cash < depositWithdrawalDTO.Amount + 1) //El 1 es transaction.Charge
        {
            throw new Exception("No tienes suficiente saldo para realizar el retiro");
        }

        Transaction transaction = new(depositWithdrawalDTO.UserId, "Retirar dinero", depositWithdrawalDTO.Amount, depositWithdrawalDTO.PaymentMethod);
        user.Cash -= depositWithdrawalDTO.Amount + transaction.Charge;
         _userRepository.UpdateUser(user);
         _transactionRepository.AddTransaction(transaction);
    }

    public void BuyCrypto(BuySellCrypto buySellCrypto)
    {
        var user = _userRepository.GetUser(buySellCrypto.UserId);
        var crypto = _cryptoRepository.GetCrypto(buySellCrypto.CryptoId);
        if (user == null) throw new KeyNotFoundException($"Usuario con ID {buySellCrypto.UserId} no encontrado");
        if (crypto == null) throw new KeyNotFoundException($"Criptomoneda con ID {buySellCrypto.CryptoId} no encontrada");
        
        if (user.Cash < buySellCrypto.Amount + 1) //El 1 es transaction.Charge
        {
            throw new Exception("No tienes suficiente saldo para realizar la compra");
        }

        Transaction transaction = new(buySellCrypto.UserId, buySellCrypto.CryptoId, $"Comprar {crypto.Name}", buySellCrypto.Amount);
        user.Cash -= transaction.Amount + transaction.Charge;
        user.Wallet += transaction.Amount;
        _userRepository.UpdateUser(user);
        _transactionRepository.AddTransaction(transaction);
    }

    public void SellCrypto(BuySellCrypto buySellCrypto)
    {
        var user = _userRepository.GetUser(buySellCrypto.UserId);
        var crypto = _cryptoRepository.GetCrypto(buySellCrypto.CryptoId);
        if (user == null) throw new KeyNotFoundException($"Usuario con ID {buySellCrypto.UserId} no encontrado");
        if (crypto == null) throw new KeyNotFoundException($"Criptomoneda con ID {buySellCrypto.CryptoId} no encontrada");
        
        var userHasCrypto = HasCrypto(buySellCrypto.UserId, buySellCrypto.CryptoId);
        var userCryptoBalance = GetCryptoBalance(buySellCrypto.UserId, buySellCrypto.CryptoId);

        if (!userHasCrypto  || userCryptoBalance == 0)
        {
            throw new Exception($"El usuario {buySellCrypto.UserId} no tiene la criptomoneda {buySellCrypto.CryptoId}");
        }

        if (buySellCrypto.Amount >= userCryptoBalance) //El 1 es transaction.Charge
        {
            throw new Exception($"No tienes suficientes fondos para realizar la venta");
        }
        
        Transaction transaction = new(buySellCrypto.UserId, buySellCrypto.CryptoId, $"Vender {crypto.Name}", buySellCrypto.Amount);
        user.Cash += transaction.Amount;
        user.Wallet -= transaction.Amount + transaction.Charge;
        _userRepository.UpdateUser(user);
        _transactionRepository.AddTransaction(transaction);
    }

    public IEnumerable<Transaction> GetAllTransactions(int userId, TransactionQueryParameters transactionQueryParameters)
    {
        return _transactionRepository.GetAllTransactions(userId, transactionQueryParameters);
    }

    public Dictionary<string, double> MyCryptos(int userId)
    {
        var userTransactions = _transactionRepository.GetAllTransactions(userId, new TransactionQueryParameters());

        var totalAmountByCrypto = new Dictionary<string, double>();

        foreach (var transaction in userTransactions)
        {
            if (transaction.CryptoId.HasValue)
            {
                var cryptoName = _cryptoRepository.GetCrypto(transaction.CryptoId.Value).Name;

                if (!totalAmountByCrypto.ContainsKey(cryptoName))
                {
                    totalAmountByCrypto[cryptoName] = 0;
                }
                if (transaction.Concept.StartsWith("Comprar"))
                {
                    totalAmountByCrypto[cryptoName] += transaction.Amount;
                }
                if (transaction.Concept.StartsWith("Vender"))
                {
                    totalAmountByCrypto[cryptoName] -= transaction.Amount + transaction.Charge;
                }  
            }
        }
        return totalAmountByCrypto;
    }

    public bool IsCryptoPurchased(int cryptoId)
    {
        var cryptoPurchased = _transactionRepository.GetAllTransactionsAllUsers().FirstOrDefault(t => t.CryptoId == cryptoId);

        if (cryptoPurchased != null)
        {
            throw new Exception("No se puede eliminar la criptomoneda porque ha sido comprada por algún usuario.");
        }

        return cryptoPurchased != null;
    }
   
    public bool HasCrypto(int userId, int cryptoId)
    {
        var userTransactions = _transactionRepository.GetAllTransactions(userId, new TransactionQueryParameters());

        foreach (var transaction in userTransactions)
        {
            if (transaction.CryptoId == cryptoId)
            {
                return true;
            }
        }
        return false;
    }

    public double GetCryptoBalance(int userId, int cryptoId)
    {
        var userTransactions = _transactionRepository.GetAllTransactions(userId, new TransactionQueryParameters());

        double balance = 0;

        foreach (var transaction in userTransactions)
        {
            if (transaction.CryptoId == cryptoId)
            {
                if (transaction.Concept.StartsWith("Comprar"))
                {
                    balance += transaction.Amount;
                }
                else if (transaction.Concept.StartsWith("Vender"))
                {
                    balance -= transaction.Amount + transaction.Charge;
                }
            }
        }
        return balance;
    }
    
}