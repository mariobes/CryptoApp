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
        Console.WriteLine("4. Borrar mi cuenta");
        Console.WriteLine("5. Volver");
        SelectPrivateUserMenuOption(Console.ReadLine());
    }

    public void SelectPrivateUserMenuOption(string option)
    {
        MainMenu mainMenu = new(_userService, _cryptoService);
        PublicUserMenu publicUserMenu = new(_userService, _cryptoService);

        switch (option)
        {
        case "1":
            Console.Write("Nuevo número de teléfono: ");
            string phone = InputEmpty();
            UpdateUserField("phone", phone);
        break;
        case "2":
            Console.Write("Nueva dirección de correo: ");
            string email = InputEmpty();
            UpdateUserField("email", email);
        break;
        case "3":
            Console.Write("Nueva contraseña: ");
            string password = InputEmpty();
            UpdateUserField("password", password);
        break;
        case "4":
            _userService.DeleteUser(currentUser.Email);
            mainMenu.RegistrationMenu();
        break;
        case "5":
            publicUserMenu.MainPublicUserMenu(currentUser.Email);
        break;
        default:
            Console.WriteLine("Introduce una opción válida");
            SelectPrivateUserMenuOption(Console.ReadLine());
        break;
        }
    }


    private void UpdateUserField(string fieldName, string newField)
    {
        if (!_userService.CheckUpdateUser(fieldName, newField))
        {
            if (fieldName == "phone")
            {
                _userService.UpdateUser(currentUser.Email, newPhone: newField);
            }

            if (fieldName == "email")
            {
                _userService.UpdateUser(currentUser.Email, newEmail: newField);
            }
        }
        else
        {
            Console.WriteLine("El campo introducido ya se encuentra registrado.");
        }

        if (fieldName == "password")
        {
            _userService.UpdateUser(currentUser.Email, NewPassword: newField);
        }

        MainPrivateUserMenu(currentUser.Email);
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