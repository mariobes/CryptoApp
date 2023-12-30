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
            Crypto crypto = new(name, symbol, description, value, developer, descentralized);
            _repository.AddCrypto(crypto);
            _repository.SaveChanges();
        }
        catch (Exception e)
        {
            throw new Exception("Ha ocurrido un error al registrar la criptomoneda", e);
        }

    }

    private Dictionary<string, Crypto> GetAllCryptos()
    {
        try
        {
            Dictionary<string, Crypto> allCryptos = _repository.GetAllCryptos();
            return allCryptos;
        }
        catch (Exception e)
        {
            throw new Exception("Ha ocurrido un error al obtener las criptomonedas", e);
        }
    }

    public void PrintAllCryptos()
    {
        try
        {
            Dictionary<string, Crypto> allCryptos = _repository.GetAllCryptos();
            Console.WriteLine("Lista de criptomonedas:\n");
            foreach (var crypto in allCryptos.Values)
            {
                Console.WriteLine($"ID: {crypto.Id}, Nombre: {crypto.Name}, Abreviatura: {crypto.Symbol}, Descripción: {crypto.Description}, Fecha de Registro: {crypto.RegisterDate}, Valor: {crypto.Value}, Desarrollador: {crypto.Developer}, Descentralizada: {crypto.Descentralized}\n");
            }
        }
        catch (Exception e)
        {
            throw new Exception("Ha ocurrido un error al obtener las criptomonedas", e);
        }
    }

    public bool CheckCryptoExist(string name)
    {
        try
        {
            var allCryptos = GetAllCryptos();
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

    public Crypto GetCrypto(string name)
    {
        try
        {
            var allCryptos = _repository.GetAllCryptos();
            foreach (var crypto in allCryptos.Values)
            {
                if (crypto.Name.Equals(name, StringComparison.OrdinalIgnoreCase))
                {
                    return crypto;
                }
            }

            return null;
        }
        catch (Exception e)
        {
            throw new Exception("Ha ocurrido un error al obtener la criptomoneda", e);
        }
    }

    public void DeleteCrypto(string cryptoName)
    {
        try
        {
            Crypto cryptoToDelete = GetCrypto(cryptoName);
            _repository.DeleteCrypto(cryptoToDelete);
            _repository.SaveChanges();
        }
        catch (Exception e)
        {
            throw new Exception("Ha ocurrido un error al borrar la criptomoneda", e);
        }
    }
        






}