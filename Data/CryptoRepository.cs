using System.Text.Json;
using CryptoApp.Models;

namespace CryptoApp.Data;

public class CryptoRepository : ICryptoRepository
{
    private Dictionary<string, Crypto> _cryptos = new Dictionary<string, Crypto>();
    private readonly string _filePath = "cryptos.json";

    public CryptoRepository()
    {
        if (File.Exists(_filePath))
        {
            string jsonString = File.ReadAllText(_filePath);
            var cryptos = JsonSerializer.Deserialize<IEnumerable<Crypto>>(jsonString);
            _cryptos = cryptos.ToDictionary(acc => acc.Id.ToString());
        }
        if (_cryptos.Count == 0)
        {
            Crypto.CryptoIdSeed = 1;
        }
        else
        {
            Crypto.CryptoIdSeed = _cryptos.Count + 1;
        }
    }

    public void AddCrypto(Crypto crypto)
    {
        _cryptos[crypto.Id.ToString()] = crypto;
    }

    public Dictionary<string, Crypto> GetAllCryptos()
    {
        return new Dictionary<string, Crypto>(_cryptos);
    }

    public void DeleteCrypto(Crypto crypto)
    {
        _cryptos.Remove(crypto.Id.ToString());
    }

    public void UpdateCrypto(Crypto crypto)
    {
        _cryptos[crypto.Id.ToString()] = crypto;
    }

    public void SaveChanges()
    {
        var options = new JsonSerializerOptions { WriteIndented = true };
        string jsonString = JsonSerializer.Serialize(_cryptos.Values, options);
        File.WriteAllText(_filePath, jsonString);
    }

}