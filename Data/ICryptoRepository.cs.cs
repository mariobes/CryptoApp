using CryptoApp.Models;

namespace CryptoApp.Data;

public interface ICryptoRepository
{
    void AddCrypto(Crypto crypto);
    Crypto GetCrypto(string name);
    Dictionary<string, Crypto> GetAllCryptos();
    void RemoveCrypto(Crypto crypto);
    void UpdateCrypto(Crypto crypto);
    void SaveChanges();
    void LogError(string message, Exception exception);
}
