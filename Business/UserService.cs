using System.Security.Cryptography;
using CryptoApp.Data;
using CryptoApp.Models;
using Microsoft.VisualBasic;

namespace CryptoApp.Business;

public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;
    private readonly ICryptoRepository _cryptoRepository;

    public UserService(IUserRepository userRepository, ICryptoRepository cryptoRepository)
    {
        _userRepository = userRepository;
        _cryptoRepository = cryptoRepository;
    }

    public User RegisterUser(UserCreateDTO userCreateDTO)
    {
        User user = new(userCreateDTO.Name, userCreateDTO.Birthdate, userCreateDTO.Email, userCreateDTO.Password, userCreateDTO.Phone, userCreateDTO.DNI, userCreateDTO.Nationality);
        _userRepository.AddUser(user);
        return user;
    }

    public IEnumerable<User> GetAllUsers()
    {
        return _userRepository.GetAllUsers();
    }

    public User GetUserById(int userId)
    {
        var user = _userRepository.GetUser(userId);
        if (user == null)
        {
            throw new KeyNotFoundException($"Usuario con ID {userId} no encontrado");
        }
        return user;
    }
    
    public void UpdateUser(int userId, UserUpdateDTO userUpdateDTO)
    {
        var user = _userRepository.GetUser(userId);
        if (user == null)
        {
            throw new KeyNotFoundException($"Usuario con ID {userId} no encontrado");
        }
        user.Email = userUpdateDTO.Email;
        user.Password = userUpdateDTO.Password;
        user.Phone = userUpdateDTO.Phone;
        _userRepository.UpdateUser(user);
    }

    public void DeleteUser(int userId)
    {
        var user = _userRepository.GetUser(userId);
        if (user == null)
        {
            throw new KeyNotFoundException($"Usuario con ID {userId} no encontrado");
        }
        _userRepository.DeleteUser(userId);
    }

    public void MakeDeposit(DepositWithdrawalDTO depositWithdrawalDTO)
    {
        var user = _userRepository.GetUser(depositWithdrawalDTO.UserId);
        if (user == null)
        {
            throw new KeyNotFoundException($"Usuario con ID {depositWithdrawalDTO.UserId} no encontrado");
        }

        Transaction transaction = new(depositWithdrawalDTO.UserId, "Ingresar dinero", depositWithdrawalDTO.Amount, depositWithdrawalDTO.PaymentMethod);
        //user.Transactions.Add(transaction);
        user.Cash += depositWithdrawalDTO.Amount;
         _userRepository.UpdateUser(user);
         _userRepository.AddTransaction(transaction);
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
            throw new KeyNotFoundException("No tienes suficiente saldo para realizar el retiro");
        }

        Transaction transaction = new(depositWithdrawalDTO.UserId, "Retirar dinero", depositWithdrawalDTO.Amount, depositWithdrawalDTO.PaymentMethod);
        //user.Transactions.Add(transaction);
        user.Cash -= depositWithdrawalDTO.Amount + transaction.Charge;
         _userRepository.UpdateUser(user);
         _userRepository.AddTransaction(transaction);
    }

    public void BuyCrypto(BuySellCrypto buySellCrypto)
    {
        var user = _userRepository.GetUser(buySellCrypto.UserId);
        var crypto = _cryptoRepository.GetCrypto(buySellCrypto.CryptoId);
        if (user == null) throw new KeyNotFoundException($"Usuario con ID {buySellCrypto.UserId} no encontrado");
        if (crypto == null) throw new KeyNotFoundException($"Criptomoneda con ID {buySellCrypto.CryptoId} no encontrada");
        
        if (user.Cash < buySellCrypto.Amount + 1) //El 1 es transaction.Charge
        {
            throw new KeyNotFoundException("No tienes suficiente saldo para realizar la compra");
        }

        Transaction transaction = new(buySellCrypto.UserId, buySellCrypto.CryptoId, $"Comprar {crypto.Name}", buySellCrypto.Amount);
        //user.Transactions.Add(transaction);
        user.Cash -= transaction.Amount + transaction.Charge;
        user.Wallet += transaction.Amount;
        _userRepository.UpdateUser(user);
        _userRepository.AddTransaction(transaction);
    }

    public void SellCrypto(BuySellCrypto buySellCrypto)
    {
        var user = _userRepository.GetUser(buySellCrypto.UserId);
        var crypto = _cryptoRepository.GetCrypto(buySellCrypto.CryptoId);
        if (user == null) throw new KeyNotFoundException($"Usuario con ID {buySellCrypto.UserId} no encontrado");
        if (crypto == null) throw new KeyNotFoundException($"Criptomoneda con ID {buySellCrypto.CryptoId} no encontrada");
        


        Transaction transaction = new(buySellCrypto.UserId, buySellCrypto.CryptoId, $"Vender {crypto.Name}", buySellCrypto.Amount);
        //user.Transactions.Add(transaction);
        user.Cash += transaction.Amount;
        user.Wallet -= transaction.Amount + transaction.Charge;
        _userRepository.UpdateUser(user);
        _userRepository.AddTransaction(transaction);
    }

    public IEnumerable<Transaction> GetAllTransactions(TransactionQueryParameters transactionQueryParameters)
    {
        return _userRepository.GetAllTransactions(transactionQueryParameters);
    }
    
}