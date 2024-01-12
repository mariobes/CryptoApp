using System.Security.Cryptography;
using CryptoApp.Data;
using CryptoApp.Models;
using Microsoft.VisualBasic;

namespace CryptoApp.Business;

public class UserService : IUserService
{
    private readonly IUserRepository _repository;

    public UserService(IUserRepository repository)
    {
        _repository = repository;
    }

    public void RegisterUser(string name, DateTime birthday, string email, string password, string phone, string dni, string nationality)
    {
        try 
        {
            User user = new(name, birthday, email, password, phone, dni, nationality);
            _repository.AddUser(user);
            _repository.SaveChanges();
        }
        catch (Exception e)
        {
            _repository.LogError("Error al registrar el usuario", e);
            throw new Exception("Ha ocurrido un error al registrar el usuario", e);
        }
    }

    public void PrintAllUsers()
    {
        try
        {
            Console.WriteLine("Lista de usuarios:\n");
            foreach (var user in _repository.GetAllUsers().Values)
            {
                Console.WriteLine($"ID: {user.Id}, Nombre: {user.Name}, Fecha de nacimiento: {user.Birthdate}, Email: {user.Email}, Teléfono: {user.Phone}, DNI: {user.DNI}, Nacionalidad: {user.Nationality}, Efectivo: {user.Cash:F2} €, Cartera: {user.Wallet:F2} €\n");
            }
        }
        catch (Exception e)
        {
            _repository.LogError("Error al obtener los usuarios", e);
            throw new Exception("Ha ocurrido un error al obtener los usuarios", e);
        }
    }

    public bool CheckUserExist(string dni, string email, string phone)
    {
        try
        {
            foreach (var user in _repository.GetAllUsers().Values)
            {
                if (user.DNI.Equals(dni, StringComparison.OrdinalIgnoreCase) || 
                    user.Email.Equals(email, StringComparison.OrdinalIgnoreCase) ||
                    user.Phone.Equals(phone))
                {
                    return true;
                }
            }

            return false;
        }
        catch (Exception e)
        {
            _repository.LogError("Error al comprobar el usuario", e);
            throw new Exception("Ha ocurrido un error al comprobar el usuario", e);
        }
    }

    public bool CheckUpdateUser(string fieldName, string newField)
    {
        try
        {
            foreach (var user in _repository.GetAllUsers().Values)
            {
                if (fieldName == "phone")
                {
                    if (user.Phone.Equals(newField))
                    {
                        return true;
                    }
                }

                if (fieldName == "email")
                {
                  if (user.Email.Equals(newField))
                    {
                        return true;
                    }
                }
            }

            return false;
        }
        catch (Exception e)
        {
            _repository.LogError("Error al comprobar el usuario", e);
            throw new Exception("Ha ocurrido un error al comprobar el usuario", e);
        }
    }

    public bool CheckLogin(string email, string pasword)
    {
        try
        {
            foreach (var user in _repository.GetAllUsers().Values)
            {
                if (user.Email.Equals(email, StringComparison.OrdinalIgnoreCase) &&
                    user.Password.Equals(pasword))
                {
                    return true;
                }
            }

            return false;
        }
        catch (Exception e)
        {
            _repository.LogError("Error al comprobar el usuario", e);
            throw new Exception("Ha ocurrido un error al comprobar el usuario", e);
        }
    }

    public User GetUser(string email)
    {
        try
        {
            return _repository.GetUser(email);
        }
        catch (Exception e)
        {
            _repository.LogError("Error al obtener el usuario", e);
            throw new Exception("Ha ocurrido un error al obtener el usuario", e);
        }
    }

    public void DeleteUser(string userEmail)
    {
        try
        {
            User userToDelete = _repository.GetUser(userEmail);
            _repository.RemoveUser(userToDelete);
            _repository.SaveChanges();
        }
        catch (Exception e)
        {
            _repository.LogError("Error al borrar el usuario", e);
            throw new Exception("Ha ocurrido un error al borrar el usuario", e);
        }
    }
    
    public void UpdateUser(string userEmail, string newPhone = null, string newEmail = null, string newPassword = null)
    {
        try 
        {
            User userToUpdate = _repository.GetUser(userEmail);

            if (!string.IsNullOrEmpty(newPhone))
            {
                userToUpdate.Phone = newPhone;
            }

            if (!string.IsNullOrEmpty(newEmail))
            {
                userToUpdate.Email = newEmail;
            }

            if (!string.IsNullOrEmpty(newPassword))
            {
                userToUpdate.Password = newPassword;
            }

            _repository.UpdateUser(userToUpdate);
            _repository.SaveChanges();
        }
        catch (Exception e)
        {
            _repository.LogError("Error al actualizar el usuario", e);
            throw new Exception("Ha ocurrido un error al actualizar el usuario", e);
        }

    }

    public void MakeDeposit(User user, string concept, string amountInput, string paymentMethod)
    {
        try
        {
            if (double.TryParse(amountInput, out double amount))
            {
                if (amount < 10)
                {
                    Console.WriteLine("Debe depositar una cantidad mínima de 10€");
                    return;
                }

                AssignId(user);

                user.Cash += amount;
                Transaction transaction = new(concept, amount, paymentMethod);
                user.Transactions.Add(transaction);
                _repository.UpdateUser(user);
                _repository.SaveChanges();

                Console.WriteLine($"Depósito de {transaction.Amount + transaction.Charge}€ realizado con éxito");
            }
            else
            {
                Console.WriteLine("Introduce un valor numérico válido.");
            }
        }
        catch (Exception e)
        {
            _repository.LogError("Error al realizar el depósito", e);
            throw new Exception("Ha ocurrido un error al realizar el depósito", e);
        }
    }

    public void MakeWithdrawal(User user, string concept, string amountInput, string paymentMethod)
    {
        try
        {
            if (double.TryParse(amountInput, out double amount))
            {
                if (user.Cash < amount + 1) //El 1 es transaction.Charge
                {
                    Console.WriteLine("No tienes suficiente saldo para realizar el retiro");
                    return;
                }

                AssignId(user);

                Transaction transaction = new(concept, amount, paymentMethod);
                user.Transactions.Add(transaction);
                user.Cash -= transaction.Amount + transaction.Charge;
                _repository.UpdateUser(user);
                _repository.SaveChanges();

                Console.WriteLine($"Retiro de {transaction.Amount + transaction.Charge}€ realizado con éxito");
            }
            else
            {
                Console.WriteLine("Introduce un valor numérico válido.");
            }
        }
        catch (Exception e)
        {
            _repository.LogError("Error al realizar el retiro", e);
            throw new Exception("Ha ocurrido un error al realizar el retiro", e);
        }
    }

    public void BuyCrypto(User user, Crypto crypto, string concept, string amountInput)
    {
        try
        {
            if (double.TryParse(amountInput, out double amount))
            {
                if (user.Cash < amount + 1) //El 1 es transaction.Charge
                {
                    Console.WriteLine("No tienes suficiente saldo para realizar la compra");
                    return;
                }

                AssignId(user);

                if (user.AllCryptosPurchased.ContainsKey(crypto.Name))
                {
                    user.AllCryptosPurchased[crypto.Name] += amount;
                }
                else
                {
                    user.AllCryptosPurchased.Add(crypto.Name, amount);
                }

                double cryptoPrice = CalculateCryptoValue(crypto, amount);

                Transaction transaction = new(crypto, concept, amount, cryptoPrice);
                user.Transactions.Add(transaction);
                user.Cash -= transaction.Amount + transaction.Charge;
                user.Wallet += transaction.Amount;
                _repository.UpdateUser(user);
                _repository.SaveChanges();

                Console.WriteLine($"Has comprado {transaction.Crypto_Price} de {crypto.Name} por un total de {transaction.Amount}€.");
            }
            else
            {
                Console.WriteLine("Introduce un valor numérico válido.");
            }
        }
        catch (Exception e)
        {
            _repository.LogError("Error al comprar la criptomoneda", e);
            throw new Exception("Ha ocurrido un error al comprar la criptomoneda", e);
        }

    }

    public void SellCrypto(User user, Crypto crypto, string concept, string amountInput)
    {
        try
        {
            if (double.TryParse(amountInput, out double amount))
            {
                if (!user.AllCryptosPurchased.ContainsKey(crypto.Name) ||
                    user.AllCryptosPurchased[crypto.Name] < amount + 1 ||
                    amount == 0) //El 1 es transaction.Charge
                {
                    Console.WriteLine("No tienes suficiente cantidad de criptomoneda para vender.");
                    return;
                }

                AssignId(user);

                double cryptoPrice = CalculateCryptoValue(crypto, amount);

                Transaction transaction = new(crypto, concept, amount, cryptoPrice);
                user.Transactions.Add(transaction);

                user.AllCryptosPurchased[crypto.Name] -= amount + transaction.Charge;

                user.Cash += transaction.Amount;
                user.Wallet -= transaction.Amount + transaction.Charge;
                _repository.UpdateUser(user);
                _repository.SaveChanges();

                Console.WriteLine($"Has vendido {transaction.Crypto_Price} de {crypto.Name} por un total de {transaction.Amount}€.");

                if (user.AllCryptosPurchased[crypto.Name] == 0) //Se queda a 0 pero no se borra
                {
                    user.AllCryptosPurchased.Remove(crypto.Name);
                    _repository.SaveChanges();
                }
            }
            else
            {
                Console.WriteLine("Introduce un valor numérico válido.");
            }
        }
        catch (Exception e)
        {
            _repository.LogError("Error al vender la criptomoneda", e);
            throw new Exception("Ha ocurrido un error al vender la criptomoneda", e);
        }
    }

    public double CalculateCryptoValue(Crypto crypto, double amount)
    {
        try
        {
            double cryptoPrice = amount / crypto.Value;
            return cryptoPrice;
        }
        catch (Exception e)
        {
            _repository.LogError("Error al calcular el valor de la criptomoneda", e);
            throw new Exception("Ha ocurrido un error al calcular el valor de la criptomoneda", e);
        }
    }

    public void PrintAllTransactions(User user)
    {
        try
        {
            List<Transaction> allTransactions = user.Transactions;
            Console.WriteLine("Lista de transacciones:\n");
            foreach (var transaction in allTransactions)
            {
                if (transaction.Crypto == null)
                {
                    Console.WriteLine($"ID: {transaction.Id}, Concepto: {transaction.Concept}, Fecha: {transaction.Date}, Comisión: {transaction.Charge}, Cantidad: {transaction.Amount}, Método de pago: {transaction.Payment_Method}\n");
                }
                else
                {
                    Console.WriteLine($"ID: {transaction.Id}, Criptomoneda: {transaction.Crypto.Name}, Precio: {transaction.Crypto_Price}, Concepto: {transaction.Concept}, Fecha: {transaction.Date}, Comisión: {transaction.Charge}, Cantidad: {transaction.Amount}\n");

                }
            }
        }
        catch (Exception e)
        {
            _repository.LogError("Error al obtener las transacciones", e);
            throw new Exception("Ha ocurrido un error al obtener las transacciones", e);
        }
    }

    public void PrintAllCryptosPurchase(User user)
    {
        try
        {  
            Console.WriteLine("Lista de criptomonedas compradas:\n");

            foreach (var purchase in user.AllCryptosPurchased)
            {
                Console.WriteLine($"{purchase.Key}: {purchase.Value}\n");
            }
        }
        catch (Exception e)
        {
            _repository.LogError("Error al obtener las criptomonedas compradas por el usuario", e);
            throw new Exception("Ha ocurrido un error al obtener las criptomonedas compradas por el usuario", e);
        }
    }

    private void AssignId(User user)
    {
        try
        {
                if (user.Transactions.Count == 0)
                {
                    Transaction.IdTransactionSeed = 1;
                }
                else
                {
                    Transaction.IdTransactionSeed = user.Transactions.Count + 1;
                }
        }
        catch (Exception e)
        {
            _repository.LogError("Error al asignar el ID", e);
            throw new Exception("Ha ocurrido un error al asignar el ID", e);
        }
    }

    public void UpdateUserWallet(Crypto crypto, double oldCryptoValue, double newCryptoValue)
    {
        try
        {
            foreach (var user in _repository.GetAllUsers().Values)
            {
                if (user.AllCryptosPurchased.ContainsKey(crypto.Name))
                {
                double amountUser = user.AllCryptosPurchased[crypto.Name];
                double amountUserUpdated = amountUser * newCryptoValue / oldCryptoValue;
                user.AllCryptosPurchased[crypto.Name] = amountUserUpdated;

                if (amountUserUpdated > amountUser)
                {
                    user.Wallet += amountUserUpdated - amountUser;
                }
                else
                {
                    user.Wallet -=  amountUser - amountUserUpdated;
                }

                _repository.UpdateUser(user);
                _repository.SaveChanges();
                }
            }
        }
        catch (Exception e)
        {
            _repository.LogError("Error al actualizar la cartera del usuario", e);
            throw new Exception("Ha ocurrido un error al actualizar la cartera del usuario", e);
        }
    }

    public bool CheckCryptoPurchased(Crypto crypto)
    {
        try
        {
            foreach (var user in _repository.GetAllUsers().Values)
            {
                if (user.AllCryptosPurchased.ContainsKey(crypto.Name))
                {
                    return true;
                }
            }
            return false;
        }
        catch (Exception e)
        {
            _repository.LogError("Error al comprobar la criptomoneda", e);
            throw new Exception("Ha ocurrido un error al comprobar la criptomoneda", e);
        }

    }

    public string InputEmpty()
    {
        try
        {
            string input;
            do
            {
                input = Console.ReadLine();
                if (string.IsNullOrWhiteSpace(input))
                {
                    Console.WriteLine("El campo está vacío.");
                }
            } while (string.IsNullOrWhiteSpace(input));

            return input;
        }
        catch (Exception e)
        {
            _repository.LogError("Error al comprobar el campo", e);
            throw new Exception("Ha ocurrido un error al comprobar el campo", e);
        }
    }

}