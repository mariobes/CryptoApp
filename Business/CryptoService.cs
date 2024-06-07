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

    public Crypto RegisterCrypto(CryptoCreateUpdateDTO cryptoCreateUpdateDTO)
    {
        var registeredCrypto = _repository.GetAllCryptos().FirstOrDefault(c => c.Name.Equals(cryptoCreateUpdateDTO.Name, StringComparison.OrdinalIgnoreCase));
        if (registeredCrypto != null)
        {
            throw new Exception("El nombre de la criptomoneda ya existe.");
        }
        Crypto crypto = new(cryptoCreateUpdateDTO.Name, cryptoCreateUpdateDTO.Symbol, cryptoCreateUpdateDTO.Description, cryptoCreateUpdateDTO.Value, cryptoCreateUpdateDTO.Developer, cryptoCreateUpdateDTO.Descentralized);
        _repository.AddCrypto(crypto);
        return crypto;
    }

    public IEnumerable<Crypto> GetAllCryptos(CryptoQueryParameters? cryptoQueryParameters)
    {
        return _repository.GetAllCryptos(cryptoQueryParameters);
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

        var registeredCrypto = _repository.GetAllCryptos().FirstOrDefault(c => c.Name.Equals(cryptoCreateUpdateDTO.Name, StringComparison.OrdinalIgnoreCase));
        if ((registeredCrypto != null) && (cryptoId != registeredCrypto.Id))
        {
            throw new Exception("El nombre de la criptomoneda ya existe.");
        }

        crypto.Name = cryptoCreateUpdateDTO.Name;
        crypto.Symbol = cryptoCreateUpdateDTO.Symbol;
        crypto.Description = cryptoCreateUpdateDTO.Description;
        crypto.Value = cryptoCreateUpdateDTO.Value;
        crypto.Developer = cryptoCreateUpdateDTO.Developer;
        crypto.Descentralized = cryptoCreateUpdateDTO.Descentralized;
        _repository.UpdateCrypto(crypto);
    }

    public void DeleteCrypto(int cryptoId)
    {
        var crypto = _repository.GetCrypto(cryptoId);
        if (crypto == null)
        {
            throw new KeyNotFoundException($"Criptomoneda con ID {cryptoId} no encontrada");
        }
        _repository.DeleteCrypto(cryptoId);
    }
    
}