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
        Console.WriteLine($"Cartera: {currentUser.Wallet} | Efectivo: {currentUser.Cash}\n");
        Console.WriteLine($"Teléfono: {currentUser.Phone}\n");
        Console.WriteLine($"Correo: {currentUser.Email}\n");
        Console.WriteLine($"Contraseña: {currentUser.Password}\n");
        Console.WriteLine($"Nombre: {currentUser.Name}\n");
        Console.WriteLine($"Fecha de nacimiento: {currentUser.Birthdate}\n");
        Console.WriteLine($"Nacionalidad: {currentUser.Nationality}\n");
        Console.WriteLine($"DNI: {currentUser.DNI}\n");

        Console.WriteLine("1. Modificar teléfono | 2. Modificar correo | 3. Modificar contraseña");
        Console.WriteLine("4. Ingresar dinero | 5. Retirar dinero");
        Console.WriteLine("6. Comprar criptomoneda | 7. Vender criptomoneda");
        Console.WriteLine("8. Ver mis transacciones");
        Console.WriteLine("9. Eliminar cuenta");
        Console.WriteLine("10. Volver");
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
            string phone = _userService.InputEmpty();
            UpdateUserField("phone", phone);
        break;
        case "2":
            Console.Write("Nueva dirección de correo: ");
            string email = _userService.InputEmpty();
            UpdateUserField("email", email);
        break;
        case "3":
            Console.Write("Nueva contraseña: ");
            string password = _userService.InputEmpty();
            UpdateUserField("password", password);
        break;
        case "4":
            MakeOperation("deposit");
        break;
        case "5":
            MakeOperation("drawal");
        break;
        case "6":
            BuyCrypto();
        break;
        case "7":
            SellCrypto();
        break;
        case "8":
            _userService.PrintAllTransactions(currentUser);
            MainPrivateUserMenu(currentUser.Email);
        break;
        case "9":
            _userService.DeleteUser(currentUser.Email);
            Console.WriteLine("Has eliminado tu cuenta");
            mainMenu.RegistrationMenu();
        break;
        case "10":
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
                Console.WriteLine("Has actualizado tu teléfono correctamente");
            }

            if (fieldName == "email")
            {
                _userService.UpdateUser(currentUser.Email, newEmail: newField);
                Console.WriteLine("Has actualizado tu correo correctamente");
            }
        }
        else
        {
            Console.WriteLine("El campo introducido ya se encuentra registrado.");
        }

        if (fieldName == "password")
        {
            _userService.UpdateUser(currentUser.Email, NewPassword: newField);
            Console.WriteLine("Has actualizado tu contraseña correctamente");
        }

        MainPrivateUserMenu(currentUser.Email);
    }

    private void MakeOperation(string kind)
    {
        Console.WriteLine("Se le cobrará una comisión de 1€.");

        if (kind == "deposit") Console.Write("Añadir dinero (Mínimo 10€): ");
        else Console.Write("Retirar dinero: ");
        string amountInput = _userService.InputEmpty();

        string paymentMethod = "";
        string paymentMethodOption;

        if (kind == "drawal")
        {
            paymentMethod = "Cuenta bancaria";
        }
        else
        {
            Console.Write("1. Tarjeta de crédito | 2. Google Pay | 3. Cuenta bancaria\n");
            Console.Write("Introduce tu método de pago: ");
            do
            {
                paymentMethodOption = _userService.InputEmpty();

                switch (paymentMethodOption)
                {
                    case "1":
                        paymentMethod = "Tarjeta de crédito";
                    break;
                    case "2":
                        paymentMethod = "Google Pay";
                    break;
                    case "3":
                        paymentMethod = "Cuenta bancaria";
                    break;
                    default:
                        Console.WriteLine("Introduce una opción válida");
                    break;
                }
            } while (paymentMethodOption != "1" && 
                     paymentMethodOption != "2" &&
                     paymentMethodOption != "3");
        }

        if (kind == "deposit") _userService.MakeDeposit(currentUser, "Ingreso", amountInput, paymentMethod);
        else _userService.MakeWithdrawal(currentUser, "Retiro", amountInput, paymentMethod);

        MainPrivateUserMenu(currentUser.Email);
    }

    private void BuyCrypto()
    {

    }

    private void SellCrypto()
    {

    }

}