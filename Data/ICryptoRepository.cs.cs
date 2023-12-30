using CryptoApp.Models;

namespace CryptoApp.Data;

public interface ICryptoRepository
{
    void AddCrypto(Crypto crypto);
    Dictionary<string, Crypto> GetAllCryptos();
    void DeleteCrypto(Crypto crypto);
    //void UpdateCrypto(Crypto crypto);
    void SaveChanges();
}
