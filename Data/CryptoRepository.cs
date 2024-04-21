using System.Text.Json;
using CryptoApp.Models;

namespace CryptoApp.Data;

public class CryptoRepository : ICryptoRepository
{
    private Dictionary<string, Crypto> _cryptos = new Dictionary<string, Crypto>();
    private readonly string _filePath = Path.Combine("..", "Data", "cryptos.json");

    public CryptoRepository()
    {
        if (File.Exists(_filePath))
        {
            try
            {
                string jsonString = File.ReadAllText(_filePath);
                var cryptos = JsonSerializer.Deserialize<IEnumerable<Crypto>>(jsonString);
                _cryptos = cryptos.ToDictionary(acc => acc.Id.ToString());
            }
            catch (Exception e)
            {
                throw new Exception("Error al leer el archivo de criptomonedas", e);
            }
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

    public Crypto GetCrypto(string cryptoId)
    {
        return _cryptos.TryGetValue(cryptoId, out var crypto) ? crypto : null;
    }

    public IEnumerable<Crypto> GetAllCryptos()
    {
        return _cryptos.Values;
    }

    public void DeleteCrypto(string cryptoId)
    {
        _cryptos.Remove(cryptoId);
    }

    public void UpdateCrypto(Crypto crypto)
    {
        _cryptos[crypto.Id.ToString()] = crypto;
    }

    public void SaveChanges()
    {
        try
        {
            var options = new JsonSerializerOptions { WriteIndented = true };
            string jsonString = JsonSerializer.Serialize(_cryptos.Values, options);
            File.WriteAllText(_filePath, jsonString);
        }
        catch (Exception e)
        {
            throw new Exception("Ha ocurrido un error al guardar cambios en el archivo de criptomonedas", e);
        }
    }

}