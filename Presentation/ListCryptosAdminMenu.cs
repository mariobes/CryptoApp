using CryptoApp.Business;
using CryptoApp.Models;

namespace CryptoApp.Presentation;

public class ListCryptosAdminMenu
{
    public readonly IUserService _userService;
    public readonly ICryptoService _cryptoService;
  
    public ListCryptosAdminMenu(IUserService userService, ICryptoService cryptoService)
    {
        _userService = userService;
        _cryptoService = cryptoService;

    }

    public void MainListCryptosAdminMenu()
    {
        Console.WriteLine("\n~~~~~~~~~~~~ CRYPTOAPP ~~~~~~~~~~~~\n");
        _cryptoService.PrintAllCryptos();
        Console.WriteLine("1. Buscar criptomoneda");
        Console.WriteLine("2. Borrar criptomoneda");
        Console.WriteLine("3. Editar criptomoneda");
        Console.WriteLine("4. Volver");
        SelectListCryptosOption(Console.ReadLine());
    }

    public void SelectListCryptosOption(string option)
    {
        AdminMenu adminMenu = new(_userService, _cryptoService);

        switch (option)
        {
        case "1":
            _cryptoService.SearchCrypto();
            adminMenu.MainAdminMenu();
        break;
        case "2":
            DeleteCrypto();
        break;
        case "3":           
            UpdateCrypto();
        break;
        case "4":            
            adminMenu.MainAdminMenu();
        break;
        default:
            Console.WriteLine("Introduce una opción válida");
            SelectListCryptosOption(Console.ReadLine());
        break;
        }
    }

    private void DeleteCrypto()
    {
        Console.WriteLine("Borrar criptomoneda por su nombre: ");
        string CryptoName = _cryptoService.InputEmpty();
        Crypto crypto = _cryptoService.GetCrypto(CryptoName);

        if (crypto == null)
        {
            Console.WriteLine("No se ha encontrado ninguna criptomoneda por ese nombre");
        }
        else
        {
            _cryptoService.DeleteCrypto(CryptoName);
            Console.WriteLine("Criptomoneda eliminada correctamente");
        }

        MainListCryptosAdminMenu();
    }

    private void UpdateCrypto()
    {
        Console.WriteLine("Editar criptomoneda por su nombre: ");
        string cryptoName = _cryptoService.InputEmpty(); 
        Crypto crypto = _cryptoService.GetCrypto(cryptoName);

        if (crypto == null)
        {
            Console.WriteLine("No se ha encontrado ninguna criptomoneda por ese nombre");
        }
        else
        {
            Console.Write("No escribas nada si no quieres modificar el parámetro.\n");

            Console.Write($"Nombre ({crypto.Name}): ");
            string name = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(name)) name = crypto.Name;

            Console.Write($"Abreviatura ({crypto.Symbol}): ");
            string symbol = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(symbol)) symbol = crypto.Symbol;
        
            Console.Write($"Descripción ({crypto.Description}): ");
            string description = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(description)) description = crypto.Description;

            Console.Write($"Valor ({crypto.Value}): ");
            double value;
            string valueInput = Console.ReadLine();

            if (!string.IsNullOrWhiteSpace(valueInput) && 
                double.TryParse(valueInput, out value))
            {
                crypto.Value = value;
            }
            else
            {
                value = crypto.Value;
                Console.WriteLine($"Valor inválido, se mantendrá el valor existente ({crypto.Value})\n");
            }

            Console.Write($"Desarrollador ({crypto.Developer}): ");
            string developer = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(developer)) developer = crypto.Developer;

            Console.Write($"Descentralizada (Si: Y | No: N) ({crypto.Descentralized}): ");
            bool descentralized;
            string descentralizedInput;
            descentralizedInput = Console.ReadLine();

            if (descentralizedInput.ToLower() == "y")
            {
                descentralized = true;
            }
            else if (descentralizedInput.ToLower() == "n")
            {
                descentralized = false;
            }
            else
            {
                descentralized = crypto.Descentralized;
                Console.WriteLine($"Valor inválido, se mantendrá el valor existente ({crypto.Descentralized})\n");
            }
     

            _cryptoService.UpdateCrypto(cryptoName, name, symbol, description, value, developer, descentralized);
            Console.WriteLine("Has actualizado la criptomoneda correctamente"); 
        }

        MainListCryptosAdminMenu();
    }

}
