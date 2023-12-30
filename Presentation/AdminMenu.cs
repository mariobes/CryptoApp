using CryptoApp.Business;
using CryptoApp.Models;

namespace CryptoApp.Presentation;

public class AdminMenu
{
    public readonly IUserService _userService;
    public readonly ICryptoService _cryptoService;

    public AdminMenu(IUserService userService, ICryptoService cryptoService)
    {
        _userService = userService;
        _cryptoService = cryptoService;
    }

    public void MainAdminMenu()
    {
    Console.WriteLine("\n~~~~~~~~~~~~ CRYPTOAPP ~~~~~~~~~~~~\n");
    Console.WriteLine("1. Nueva criptomoneda");
    Console.WriteLine("2. Lista de criptomonedas");
    Console.WriteLine("3. Lista de usuarios");
    Console.WriteLine("4. Cerrar sesión");
    SelectAdminMenuOption(Console.ReadLine());
    }

    public void SelectAdminMenuOption(string option)
    {
        MainMenu mainMenu = new(_userService, _cryptoService);
        Crypto crypto = new();

        switch (option)
        {
        case "1":
            RegisterCrypto();
        break;
        case "2":
            _cryptoService.PrintAllCryptos();
        break;
        case "3":
            _userService.PrintAllUsers();
        break;
        case "4":
            mainMenu.RegistrationMenu();
        break;
        default:
            Console.WriteLine("Introduce una opción válida");
            SelectAdminMenuOption(Console.ReadLine());
        break;
        }
    }

    private void RegisterCrypto()
    {
        Console.Write("Nombre: ");
        string name = InputEmpty();

        Console.Write("Abreviatura: ");
        string symbol = InputEmpty();
      
        Console.Write("Descripción: ");
        string description = InputEmpty();

        Console.Write("Valor: ");
        double value;
        while (!double.TryParse(Console.ReadLine(), out value))
        {
            Console.WriteLine("Introduce un valor numérico válido: ");
        }

        Console.Write("Desarrollador: ");
        string developer = InputEmpty();

        Console.Write("Descentralizada (Si: Y | No: N): ");
        bool descentralized = false;
        string descentralizedInput;
        do
        {
            descentralizedInput = InputEmpty().ToLower();
            if (descentralizedInput == "y")
            {
                descentralized = true;
            }
            else if (descentralizedInput == "n")
            {
                descentralized = false;
            }
            else
            {
                Console.Write("Error, introduce una opción válida (Si: Y | No: N): ");
            }
        } while (descentralizedInput != "y" && descentralizedInput != "n");

        if (_cryptoService.CheckCryptoExist(name))
        {
            Console.WriteLine("Error, ya existe una criptomoneda con ese nombre");
        }
        else
        {
            _cryptoService.RegisterCrypto(name, symbol, description, value, developer, descentralized);
            Console.WriteLine("¡Criptomoneda registrada con éxito!");
        }

        MainAdminMenu();
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