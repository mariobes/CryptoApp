using CryptoApp.Models;

namespace CryptoApp.Business;

public interface ICryptoService
{
    public Crypto RegisterCrypto(CryptoCreateUpdateDTO cryptoCreateUpdateDTO);
    public IEnumerable<Crypto> GetAllCryptos();
    public Crypto GetCryptoById(int cryptoId);
    public void UpdateCrypto(int cryptoId, CryptoCreateUpdateDTO cryptoCreateUpdateDTO);
    public void DeleteCrypto(int cryptoId);
}
