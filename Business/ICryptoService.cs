using CryptoApp.Models;

namespace CryptoApp.Business;

public interface ICryptoService
{
    public Crypto RegisterCrypto(CryptoCreateUpdateDTO cryptoCreateUpdateDTO);
    public IEnumerable<Crypto> GetAllCryptos();
    public Crypto GetCryptoById(string cryptoId);
    public void UpdateCrypto(string cryptoId, CryptoCreateUpdateDTO cryptoCreateUpdateDTO);
    public void DeleteCrypto(string cryptoId);
}
