using CryptoApp.Models;

namespace CryptoApp.Data;

public interface ICryptoRepository
{
    public void AddCrypto(Crypto crypto);
    public Crypto GetCrypto(int cryptoId);
    IEnumerable<Crypto> GetAllCryptos(CryptoQueryParameters? cryptoQueryParameters = null);
    public void DeleteCrypto(int cryptoId);
    public void UpdateCrypto(Crypto crypto);
    void SaveChanges();
}
