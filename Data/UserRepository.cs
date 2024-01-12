using System.Text.Json;
using CryptoApp.Models;

namespace CryptoApp.Data;

public class UserRepository : IUserRepository
{
    private Dictionary<string, User> _users = new Dictionary<string, User>();
    private readonly string _filePath = "users.json";
    private readonly string _logsFilePath = "logs.json";

    public UserRepository()
    {
        if (File.Exists(_filePath))
        {
            try
            {
                string jsonString = File.ReadAllText(_filePath);
                var users = JsonSerializer.Deserialize<IEnumerable<User>>(jsonString);
                _users = users.ToDictionary(acc => acc.Id.ToString());
            }
            catch (Exception e)
            {
                LogError("Error al leer el archivo de usuarios", e);
            }
        }

        if (_users.Count == 0)
        {
            User.UserIdSeed = 1;
        }
        else
        {
            User.UserIdSeed = _users.Count + 1;
        }
    }

    public void AddUser(User user)
    {
        _users[user.Id.ToString()] = user;
    }

    public User GetUser(string userEmail)
    {
        var allUsers = GetAllUsers();
        foreach (var user in allUsers.Values)
        {
            if (user.Email.Equals(userEmail, StringComparison.OrdinalIgnoreCase))
            {
                return user;
            }
        }

        return null;
    }

    public Dictionary<string, User> GetAllUsers()
    {
        return new Dictionary<string, User>(_users);
    }

    public void RemoveUser(User user)
    {
        _users.Remove(user.Id.ToString());
    }

    public void UpdateUser(User user)
    {
        _users[user.Id.ToString()] = user;
    }

    public void SaveChanges()
    {
        try
        {
            var options = new JsonSerializerOptions { WriteIndented = true };
            string jsonString = JsonSerializer.Serialize(_users.Values, options);
            File.WriteAllText(_filePath, jsonString);
        }
        catch (Exception e)
        {
            LogError("Error al guardar cambios en el archivo de usuarios", e);
            throw new Exception("Ha ocurrido un error al guardar cambios en el archivo de usuarios", e);
        }
    }

    public void LogError(string message, Exception exception)
    {
        try
        {
            string log = $"{DateTime.Now} ERROR {message}\n{exception}\n";
            File.AppendAllText(_logsFilePath, log);
        }
        catch (Exception e)
        {
            throw new Exception("Ha ocurrido un error al escribir en logs", e);
        }
    }

}