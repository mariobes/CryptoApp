using System.Text.Json;
using CryptoApp.Models;

namespace CryptoApp.Data;

public class CryptoRepository : ICryptoRepository
{
    private Dictionary<string, Crypto> _cryptos = new Dictionary<string, Crypto>();
    private readonly string _filePath = "cryptos.json";
    private readonly string _logsFilePath = "logs.json";

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
                LogError("Error al leer el archivo de criptomonedas", e);
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

    public Crypto GetCrypto(string name)
    {
            var allCryptos = GetAllCryptos();
            foreach (var crypto in allCryptos.Values)
            {
                if (crypto.Name.Equals(name, StringComparison.OrdinalIgnoreCase))
                {
                    return crypto;
                }
            }
            
            return null;
    }

    public Dictionary<string, Crypto> GetAllCryptos()
    {
        return new Dictionary<string, Crypto>(_cryptos);
    }

    public void RemoveCrypto(Crypto crypto)
    {
        _cryptos.Remove(crypto.Id.ToString());
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
            LogError("Error al guardar cambios en el archivo de criptomonedas", e);
            throw new Exception("Ha ocurrido un error al guardar cambios en el archivo de criptomonedas", e);
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