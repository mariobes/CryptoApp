using System.Text.Json;
using CryptoApp.Models;

namespace CryptoApp.Data;

public class UserRepository : IUserRepository
{
    private Dictionary<string, User> _users = new Dictionary<string, User>();
    private readonly string _filePath = "users.json";

    public UserRepository()
    {
        if (File.Exists(_filePath))
        {
            string jsonString = File.ReadAllText(_filePath);
            var users = JsonSerializer.Deserialize<IEnumerable<User>>(jsonString);
            _users = users.ToDictionary(acc => acc.Id.ToString());
        }
        if (_users.Count == 0)
        {
            User.UserIdSeed = 1;
        }
        else
        {
            User.UserIdSeed = _users.Count + 1;
        }
            //int totalUserId = _users.Count;
            //int userIdSeed = User.userIdSeedPublic;
    }

    public void AddUser(User user)
    {
        _users[user.Id.ToString()] = user;
    }

    public Dictionary<string, User> GetAllUsers()
    {
        return new Dictionary<string, User>(_users);
    }

    public void DeleteUser(User user)
    {
        _users.Remove(user.Id.ToString());
    }

    public void UpdateUser(User user)
    {
        _users[user.Id.ToString()] = user;
    }

    public void SaveChanges()
    {
        var options = new JsonSerializerOptions { WriteIndented = true };
        string jsonString = JsonSerializer.Serialize(_users.Values, options);
        File.WriteAllText(_filePath, jsonString);
    }


}