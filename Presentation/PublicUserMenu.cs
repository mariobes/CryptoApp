using CryptoApp.Business;
using CryptoApp.Models;

namespace CryptoApp.Presentation;

public class PublicUserMenu
{
    public readonly IUserService _userService;
    public readonly ICryptoService _cryptoService;
    private User currentUser;

    public PublicUserMenu(IUserService userService, ICryptoService cryptoService)
    {
        _userService = userService;
        _cryptoService = cryptoService;
    }

    public void MainPublicUserMenu(string userEmail)
    {
        currentUser = _userService.GetUser(userEmail);

        Console.WriteLine("\n~~~~~~~~~~~~ CRYPTOAPP ~~~~~~~~~~~~\n");
        Console.WriteLine($"¡Hola, {currentUser.Name}!\n");
        Console.WriteLine($"Cartera: {currentUser.Wallet} | Efectivo: {currentUser.Cash}\n");
        Console.WriteLine("1. Mi cuenta");
        Console.WriteLine("2. Criptomonedas");
        Console.WriteLine("3. Cerrar sesión");
        SelectPublicUserMenuOption(Console.ReadLine());
    }

    public void SelectPublicUserMenuOption(string option)
    {
        MainMenu mainMenu = new(_userService, _cryptoService);
        PrivateUserMenu privateUserMenu = new(_userService, _cryptoService);

        switch (option)
        {
        case "1":
            privateUserMenu.MainPrivateUserMenu(currentUser.Email);
        break;
        case "2":
            
        break;
        case "3":
            Console.WriteLine("Se ha cerrado la sesión");
            mainMenu.RegistrationMenu();
        break;
        default:
            Console.WriteLine("Introduce una opción válida");
            SelectPublicUserMenuOption(Console.ReadLine());
        break;
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