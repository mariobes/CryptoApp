using CryptoApp.Models;
using CryptoApp.Business;
using CryptoApp.Data;
using CryptoApp.Presentation;


IUserRepository userRepository = new UserRepository();
ICryptoRepository cryptoRepository = new CryptoRepository();
IUserService userService = new UserService(userRepository);
ICryptoService cryptoService = new CryptoService(cryptoRepository);
MainMenu mainMenu = new(userService, cryptoService);
mainMenu.RegistrationMenu();



