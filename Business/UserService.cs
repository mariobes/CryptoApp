using CryptoApp.Data;
using CryptoApp.Models;

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
            throw new Exception("Ha ocurrido un error al registrar el usuario", e);
        }

    }

    public void PrintAllUsers()
    {
        try
        {
            Dictionary<string, User> allUsers = _repository.GetAllUsers();
            Console.WriteLine("Lista de usuarios:\n");
            foreach (var user in allUsers.Values)
            {
                Console.WriteLine($"ID: {user.Id}, Nombre: {user.Name}, Email: {user.Email}, Teléfono: {user.Phone}, DNI: {user.DNI}, Nacionalidad: {user.Nationality}, Efectivo: {user.Cash}, Billetera: {user.Wallet}\n");
            }
        }
        catch (Exception e)
        {
            throw new Exception("Ha ocurrido un error al obtener los usuarios", e);
        }
    }

    public bool CheckUserExist(string dni, string email, string phone)
    {
        try
        {
            var allUsers = _repository.GetAllUsers();
            foreach (var user in allUsers.Values)
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
            throw new Exception("Ha ocurrido un error al comprobar el usuario", e);
        }
    }

    public bool CheckUpdateUser(string fieldName, string newField)
    {
        try
        {
            var allUsers = _repository.GetAllUsers();
            foreach (var user in allUsers.Values)
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
            throw new Exception("Ha ocurrido un error al comprobar el usuario", e);
        }
    }

    public bool CheckLogin(string email, string pasword)
    {
        try
        {
            var allUsers = _repository.GetAllUsers();
            foreach (var user in allUsers.Values)
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

                if (user.Transactions.Count == 0)
                {
                    Transaction.IdTransactionSeed = 1;
                }
                else
                {
                    Transaction.IdTransactionSeed = user.Transactions.Count + 1;
                }

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
            throw new Exception("Ha ocurrido un error al realizar el depósito", e);
        }
    }

    public void MakeWithdrawal(User user, string concept, string amountInput, string paymentMethod)
    {
        try
        {
            if (double.TryParse(amountInput, out double amount))
            {
                if (user.Cash < amount)
                {
                    Console.WriteLine("No tienes suficiente saldo para realizar el retiro");
                    return;
                }

                if (user.Transactions.Count == 0)
                {
                    Transaction.IdTransactionSeed = 1;
                }
                else
                {
                    Transaction.IdTransactionSeed = user.Transactions.Count + 1;
                }

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
            throw new Exception("Ha ocurrido un error al realizar el retiro", e);
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
                Console.WriteLine($"ID: {transaction.Id}, Criptomoneda: {transaction.Crypto}, Concepto: {transaction.Concept}, Fecha: {transaction.Date}, Comisión: {transaction.Charge}, Cantidad: {transaction.Amount}, Método de pago: {transaction.Payment_Method}\n");
            }
        }
        catch (Exception e)
        {
            throw new Exception("Ha ocurrido un error al obtener las transacciones", e);
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
            throw new Exception("Ha ocurrido un error al comprobar el campo", e);
        }
    }

}