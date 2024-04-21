using CryptoApp.Models;

namespace CryptoApp.Data;

public interface ICryptoRepository
{
    public void AddCrypto(Crypto crypto);
    public Crypto GetCrypto(string cryptoId);
    public IEnumerable<Crypto> GetAllCryptos();
    public void DeleteCrypto(string cryptoId);
    public void UpdateCrypto(Crypto crypto);
    void SaveChanges();
}