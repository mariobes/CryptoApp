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

    public void RegisterCrypto(string name, string symbol, string description, double value, string developer, bool descentralized)
    {
        try 
        {
            Crypto crypto = new Crypto(name, symbol, description, value, developer, descentralized);
            _repository.AddCrypto(crypto);
            _repository.SaveChanges();
        }
        catch (Exception e)
        {
            throw new Exception("Ha ocurrido un error al registrar la criptomoneda", e);
        }

    }

    public bool CheckCryptoExist(string name)
    {
        try
        {
            Dictionary<string, Crypto> allCryptos = _repository.GetAllCryptos();
            foreach (var crypto in allCryptos.Values)
            {
                if (crypto.Name.Equals(name, StringComparison.OrdinalIgnoreCase))
                {
                    return true;
                }
            }

            return false;
        }
        catch (Exception e)
        {
            throw new Exception("Ha ocurrido un error al comprobar la criptomoneda", e);
        }
    }
        






}