using CryptoApp.Business;
using CryptoApp.Models;

namespace CryptoApp.Presentation;

public class ListCryptosMenu
{
    public readonly IUserService _userService;
    public readonly ICryptoService _cryptoService;
  
    public ListCryptosMenu(IUserService userService, ICryptoService cryptoService)
    {
        _userService = userService;
        _cryptoService = cryptoService;

    }

    public void MainListCryptosMenu()
    {
        Console.WriteLine("\n~~~~~~~~~~~~ CRYPTOAPP ~~~~~~~~~~~~\n");
        _cryptoService.PrintAllCryptos();
        Console.WriteLine("1. Buscar criptomoneda");
        Console.WriteLine("2. Borrar criptomoneda");
        Console.WriteLine("3. Volver");
        SelectListCryptosOption(Console.ReadLine());
    }

    public void SelectListCryptosOption(string option)
    {
        AdminMenu adminMenu = new(_userService, _cryptoService);

        switch (option)
        {
        case "1":
            Console.WriteLine("Buscar criptomoneda por su nombre: ");
            string searchCryptoName = InputEmpty();
            SearchCrypto(searchCryptoName);
        break;
        case "2":
            Console.WriteLine("Borrar criptomoneda por su nombre: ");
            string deleteCryptoName = InputEmpty();
            DeleteCrypto(deleteCryptoName);
        break;
        case "3":            
            adminMenu.MainAdminMenu();
        break;
        default:
            Console.WriteLine("Introduce una opción válida");
            SelectListCryptosOption(Console.ReadLine());
        break;
        }
    }

    public void SearchCrypto(string name)
    {
        Crypto crypto = _cryptoService.GetCrypto(name);

        if (crypto == null)
        {
            Console.WriteLine("No se ha encontrado ninguna criptomoneda por ese nombre");
            MainListCryptosMenu();
        }
        else
        {
            Console.WriteLine($"ID: {crypto.Id}, Nombre: {crypto.Name}, Abreviatura: {crypto.Symbol}, Descripción: {crypto.Description}, Fecha de Registro: {crypto.RegisterDate}, Valor: {crypto.Value}, Desarrollador: {crypto.Developer}, Descentralizada: {crypto.Descentralized}");
            AdminMenu adminMenu = new(_userService, _cryptoService);
            adminMenu.MainAdminMenu();
        }
    }

    public void DeleteCrypto(string name)
    {
        Crypto crypto = _cryptoService.GetCrypto(name);

        if (crypto == null)
        {
            Console.WriteLine("No se ha encontrado ninguna criptomoneda por ese nombre");
            MainListCryptosMenu();
        }
        else
        {
            _cryptoService.DeleteCrypto(name);
            Console.WriteLine("Criptomoneda eliminada correctamente");
            AdminMenu adminMenu = new(_userService, _cryptoService);
            adminMenu.MainAdminMenu();
        }
    }

    private string InputEmpty()
    {
        string input;
        do
        {
            input = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(input))
            {
                Console.WriteLine("El campo está vacío.");
            }
        } while (string.IsNullOrWhiteSpace(input));

        return input;
    }

}
