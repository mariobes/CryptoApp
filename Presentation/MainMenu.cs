using CryptoApp.Business;

namespace CryptoApp.Presentation;

public class MainMenu
{
    public readonly IUserService _userService;
    public readonly ICryptoService _cryptoService;

    public MainMenu(IUserService userService, ICryptoService cryptoService)
    {
        _userService = userService;
        _cryptoService = cryptoService;
    }

    public void RegistrationMenu()
    {
    Console.WriteLine("\n~~~~~~~~~~~~ CRYPTOAPP ~~~~~~~~~~~~\n");
    Console.WriteLine("1. Nuevo registro");
    Console.WriteLine("2. Iniciar sesión");
    Console.WriteLine("3. Cerrar");
    SelectRegistrationOption(Console.ReadLine());
    }

    public void SelectRegistrationOption(string option)
    {
        switch (option)
        {
        case "1":
            SignUp();
        break;
        case "2":
            SignIn();
        break;
        case "3":
            Console.Write("¡Hasta pronto!");
        break;
        default:
            Console.WriteLine("Opción no válida");
        break;
        }
    }

    private void SignUp() 
    {
        Console.Write("Nombre: ");
        string name = Console.ReadLine();
        Console.Write("Introduce tu fecha de nacimiento (yyyy-mm-dd): ");
        if (DateTime.TryParse(Console.ReadLine(), out DateTime birthday))
        {
            Console.Write("Dirección de email: ");
            string email = Console.ReadLine();

            Console.Write("Contraseña: ");
            string password = Console.ReadLine();

            Console.Write("Número de teléfono: ");
            string phone = Console.ReadLine();

            Console.Write("DNI: ");
            string dni = Console.ReadLine();

            Console.Write("País donde resides: ");
            string country = Console.ReadLine();

            //Comprobar que el dni y el gmail no coinciden
            Console.WriteLine("¡Registro completado!");
            RegistrationMenu();
        } 
        else
        {
            Console.WriteLine("La fecha introducida es incorrecta");
            RegistrationMenu();
        } 
    }

    private void SignIn() {
        Console.Write("Dirección de email: ");
        string email = Console.ReadLine();
        Console.Write("Contraseña: ");
        string password = Console.ReadLine();
        //Comprobar que el correo y la contraseña coinciden
        Console.WriteLine("¡Hola, ${name}!");
        RegistrationMenu();
    }





}
