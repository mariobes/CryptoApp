using CryptoApp.Business;
using CryptoApp.Models;

namespace CryptoApp.Presentation;

public class PrivateUserMenu
{
    public readonly IUserService _userService;
    public readonly ICryptoService _cryptoService;
    private User currentUser;

    public PrivateUserMenu(IUserService userService, ICryptoService cryptoService)
    {
        _userService = userService;
        _cryptoService = cryptoService;
    }

    public void MainPrivateUserMenu(string userEmail)
    {
        currentUser = _userService.GetUser(userEmail);

        Console.WriteLine("\n~~~~~~~~~~~~ CRYPTOAPP ~~~~~~~~~~~~\n");
        Console.WriteLine($"Teléfono: {currentUser.Phone}\n");
        Console.WriteLine($"Correo: {currentUser.Email}\n");
        Console.WriteLine($"Contraseña: {currentUser.Password}\n");
        Console.WriteLine($"Nombre: {currentUser.Name}\n");
        Console.WriteLine($"Fecha de nacimiento: {currentUser.Birthdate}\n");
        Console.WriteLine($"Nacionalidad: {currentUser.Nationality}\n");
        Console.WriteLine($"DNI: {currentUser.DNI}\n");

        Console.WriteLine("1. Modificar teléfono");
        Console.WriteLine("2. Modificar correo");
        Console.WriteLine("3. Modificar contraseña");
        Console.WriteLine("4. Volver");
        SelectPrivateUserMenuOption(Console.ReadLine());
    }

    public void SelectPrivateUserMenuOption(string option)
    {
        PublicUserMenu publicUserMenu = new(_userService, _cryptoService);

        switch (option)
        {
        case "1":
            Console.Write("Nuevo número de teléfono: ");
            string phone = InputEmpty();
            _userService.UpdateUser(currentUser.Email, newPhone: phone);
            MainPrivateUserMenu(currentUser.Email);
        break;
        case "2":
            Console.Write("Nueva dirección de correo: ");
            string email = InputEmpty();
            _userService.UpdateUser(currentUser.Email, newEmail: email);
            MainPrivateUserMenu(currentUser.Email);
        break;
        case "3":
            Console.Write("Nueva contraseña: ");
            string password = InputEmpty();
            _userService.UpdateUser(currentUser.Email, NewPassword: password);
            MainPrivateUserMenu(currentUser.Email);
        break;
        case "4":
            publicUserMenu.MainPublicUserMenu(currentUser.Email);
        break;
        default:
            Console.WriteLine("Introduce una opción válida");
            SelectPrivateUserMenuOption(Console.ReadLine());
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