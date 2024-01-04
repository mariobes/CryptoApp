using CryptoApp.Models;

namespace CryptoApp.Business;

public interface ICryptoService
{
    void RegisterCrypto(string name, string symbol, string description, double value, string developer, bool descentralized);
    void PrintAllCryptos();
    bool CheckCryptoExist(string name);
    Crypto GetCrypto(string name);
    void DeleteCrypto(string name);
    void UpdateCrypto(Crypto crypto, string newSymbol, string newDescription, double newValue, string newDevelope, bool newDescentralized);
    void SearchCrypto();
    string InputEmpty();
}
