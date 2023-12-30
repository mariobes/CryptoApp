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
    Console.WriteLine("3. Salir");
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
            Console.WriteLine("Introduce una opción válida");
            RegistrationMenu();
        break;
        }
    }

    private void SignUp() 
    {
        Console.Write("Nombre: ");
        string name = InputEmpty();

        Console.Write("Fecha de nacimiento (yyyy-mm-dd): ");
        DateTime birthday = CheckDate();
      
        Console.Write("Dirección de correo: ");
        string email = InputEmpty();

        Console.Write("Contraseña: ");
        string password = InputEmpty();

        Console.Write("Número de teléfono: ");
        string phone = InputEmpty();

        Console.Write("DNI: ");
        string dni = InputEmpty();

        Console.Write("País donde resides: ");
        string nationality = InputEmpty();

        if (_userService.CheckUserExist(dni, email, phone))
        {
            Console.WriteLine("Error, ya existe una cuenta asociada al teléfono, correo o DNI introducido.");
            RegistrationMenu();
        }
        else{
            _userService.RegisterUser(name, birthday, email, password, phone, dni, nationality);
            Console.WriteLine("¡Registro completado!");
            RegistrationMenu();
        }
    }

    private void SignIn() {
        Console.Write("Dirección de email: ");
        string email = InputEmpty();
        Console.Write("Contraseña: ");
        string password = InputEmpty();

        if (_userService.CheckLogin(email, password))
        {
            PublicUserMenu publicUserMenu = new(_userService, _cryptoService);
            publicUserMenu.MainPublicUserMenu(email);
        } 
        else if (email == "admin" && password == "admin")
        {
            Console.WriteLine("Has iniciado sesión como Administrador");
            AdminMenu adminMenu = new(_userService, _cryptoService);
            adminMenu.MainAdminMenu();
        }
        else
        {
            Console.WriteLine("El correo o la contraseña introducida es incorrecta.");
            RegistrationMenu();
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

    private DateTime CheckDate()
    {
        string input;
        DateTime birthday;
        bool validDate = false;
        do
        {
            input = Console.ReadLine();
            if (DateTime.TryParse(input, out birthday))
            {
                validDate = true;
            } 
            else 
            {
                Console.WriteLine("La fecha introducida es incorrecta.");
            }

        } while (!validDate);

        return birthday;
    }





}
