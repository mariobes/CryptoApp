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
            User user = new User(name, birthday, email, password, phone, dni, nationality);
            _repository.AddUser(user);
            _repository.SaveChanges();
        }
        catch (Exception e)
        {
            throw new Exception("Ha ocurrido un error al registrar el usuario", e);
        }

    }

    private Dictionary<string, User> GetAllUsers()
    {
        try
        {
            Dictionary<string, User> allUsers = _repository.GetAllUsers();
            return allUsers;
        }
        catch (Exception e)
        {
            throw new Exception("Ha ocurrido un error al obtener los usuarios", e);
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
                Console.WriteLine($"ID: {user.Id}, Nombre: {user.Name}, Email: {user.Email}, Teléfono: {user.Phone}, DNI: {user.DNI}, Nacionalidad: {user.Nationality}, Efectivo: {user.Cash}, Billetera: {user.Wallet}");
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
            var allUsers = _repository.GetAllUsers();
            foreach (var user in allUsers.Values)
            {
                if (user.Email.Equals(email, StringComparison.OrdinalIgnoreCase))
                {
                    return user;
                }
            }

            return null;
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
            User userToDelete = GetUser(userEmail);
            _repository.DeleteUser(userToDelete);
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
            User userToUpdate = GetUser(userEmail);

            if (userToUpdate != null)
            {
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
                Console.WriteLine("Has actualizado tu cuenta correctamente");
            }
        }
        catch (Exception e)
        {
            throw new Exception("Ha ocurrido un error al actualizar el usuario", e);
        }

    }


}