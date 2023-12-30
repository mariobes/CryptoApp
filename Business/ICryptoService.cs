using CryptoApp.Models;

namespace CryptoApp.Business;

public interface ICryptoService
{
    void RegisterCrypto(string name, string symbol, string description, double value, string developer, bool descentralized);
    void PrintAllCryptos();
    bool CheckCryptoExist(string name);
    //Crypto GetCrypto(string name);
    //void DeleteCrypto(string name);
    //void UpdateCrypto(string name, string newName = null, string newSymbol = null, string newDescription = null, double? newValue = null, string newDeveloper = null, bool? newDescentralized = null);
}
