using CryptoApp.Data;
using CryptoApp.Models;

namespace CryptoApp.Business;

public class CryptoService : ICryptoService
{
    private readonly ICryptoRepository _repository;

    public CryptoService(ICryptoRepository repository)
    {
        _repository = repository;
    }
//string name, string symbol, string description, double value, string developer, bool descentralized
    public Crypto RegisterCrypto(CryptoCreateUpdateDTO cryptoCreateUpdateDTO)
    {
        Crypto crypto = new(cryptoCreateUpdateDTO.Name, cryptoCreateUpdateDTO.Symbol, cryptoCreateUpdateDTO.Description, cryptoCreateUpdateDTO.Value, cryptoCreateUpdateDTO.Developer, cryptoCreateUpdateDTO.Descentralized);
        _repository.AddCrypto(crypto);
        _repository.SaveChanges();
        return crypto;
    }

    public IEnumerable<Crypto> GetAllCryptos()
    {
        return _repository.GetAllCryptos();
    }

    public Crypto GetCryptoById(int cryptoId)
    {
        var crypto = _repository.GetCrypto(cryptoId);
        if (crypto == null)
        {
            throw new KeyNotFoundException($"Criptomoneda con ID {cryptoId} no encontrada");
        }
        return crypto;
    }

    public void UpdateCrypto(int cryptoId, CryptoCreateUpdateDTO cryptoCreateUpdateDTO)
    {
        var crypto = _repository.GetCrypto(cryptoId);
        if (crypto == null)
        {
            throw new KeyNotFoundException($"Criptomoneda con ID {cryptoId} no encontrada");
        }
        crypto.Name = cryptoCreateUpdateDTO.Name;
        crypto.Symbol = cryptoCreateUpdateDTO.Symbol;
        crypto.Description = cryptoCreateUpdateDTO.Description;
        crypto.Value = cryptoCreateUpdateDTO.Value;
        crypto.Developer = cryptoCreateUpdateDTO.Developer;
        crypto.Descentralized = cryptoCreateUpdateDTO.Descentralized;
        _repository.UpdateCrypto(crypto);
        _repository.SaveChanges();
    }

    public void DeleteCrypto(int cryptoId)
    {
        var crypto = _repository.GetCrypto(cryptoId);
        if (crypto == null)
        {
            throw new KeyNotFoundException($"Criptomoneda con ID {cryptoId} no encontrada");
        }
        _repository.DeleteCrypto(cryptoId);
        _repository.SaveChanges();
    }
    
}