using Microsoft.AspNetCore.Mvc;

using CryptoApp.Data;
using CryptoApp.Business;
using CryptoApp.Models;

namespace CryptoApp.API.Controllers;

[ApiController]
[Route("[controller]")]
public class UsersController : ControllerBase
{
    private readonly ILogger<UsersController> _logger;
    private readonly IUserService _userService;

    public UsersController(ILogger<UsersController> logger, IUserService userService)
    {
        _logger = logger;
        _userService = userService;
    }

    [HttpGet]
    public ActionResult<IEnumerable<User>> GetAllUsers()
    {
        try {
            var users = _userService.GetAllUsers();
            return Ok(users);
        }     
        catch (Exception ex)
        {
            _logger.LogError($"Error al obtener todos los usuarios. {ex.Message}");
            return BadRequest($"Error al obtener todos los usuarios. {ex.Message}");
        }
    }

    [HttpGet("{userId}")]
    public IActionResult GetUser(int userId)
    {
        try
        {
            var user = _userService.GetUserById(userId);
            return Ok(user);
        }
        catch (KeyNotFoundException knfex)
        {
            _logger.LogWarning($"No se ha encontrado el usuario con ID: {userId}. {knfex.Message}");
           return NotFound($"No se ha encontrado el usuario con ID: {userId}. {knfex.Message}");
        }
        catch (Exception ex)
        {
            _logger.LogError($"Error al obtener el usuario con ID: {userId}. {ex.Message}");
            return BadRequest($"Error al obtener el usuario con ID: {userId}. {ex.Message}");
        }
    }

    [HttpPost]
    public IActionResult CreateUser([FromBody] UserCreateDTO userCreateDTO)
    {
        if (!ModelState.IsValid)  {return BadRequest(ModelState); } 

        try {
            var user = _userService.RegisterUser(userCreateDTO);
            return CreatedAtAction(nameof(GetUser), new { userId = user.Id }, user);
        }     
        catch (Exception ex)
        {
            _logger.LogError($"Error al registrar el usuario. {ex.Message}");
            return BadRequest($"Error al registrar el usuario. {ex.Message}");
        }
    }

    [HttpPut("{userId}")]
    public IActionResult UpdateUser(int userId, UserUpdateDTO userUpdateDTO)
    {
        if (!ModelState.IsValid)  {return BadRequest(ModelState); } 

        try {
            _userService.UpdateUser(userId, userUpdateDTO);
            return NoContent();
        }     
        catch (KeyNotFoundException knfex)
        {
            _logger.LogWarning($"No se ha encontrado el usuario con ID: {userId}. {knfex.Message}");
            return NotFound($"No se ha encontrado el usuario con ID: {userId}. {knfex.Message}");
        }
        catch (Exception ex)
        {
            _logger.LogError($"Error al actualizar el usuario con ID: {userId}. {ex.Message}");
            return BadRequest($"Error al actualizar el usuario con ID: {userId}. {ex.Message}");
        }
    }

    [HttpDelete("{userId}")]
    public IActionResult DeleteUser(int userId)
    {
        try
        {
            _userService.DeleteUser(userId);
            return NoContent();
        }
        catch (KeyNotFoundException knfex)
        {
            _logger.LogWarning($"No se ha encontrado el usuario con ID: {userId}. {knfex.Message}");
            return NotFound($"No se ha encontrado el usuario con ID: {userId}. {knfex.Message}");
        }
        catch (Exception ex)
        {
            _logger.LogError($"Error al eliminar el usuario con ID: {userId}. {ex.Message}");
            return BadRequest($"Error al eliminar el usuario con ID: {userId}. {ex.Message}");
        }
    }

    [HttpPost("Deposit/")]
    public IActionResult MakeDeposit([FromBody] DepositWithdrawalDTO depositWithdrawalDTO)
    {
        try {
            _userService.MakeDeposit(depositWithdrawalDTO);
            return Ok("Depósito realizado correctamente.");
        }     
        catch (KeyNotFoundException knfex)
        {
            _logger.LogWarning($"No se ha encontrado el usuario con ID: {depositWithdrawalDTO.UserId}. {knfex.Message}");
            return NotFound($"No se ha encontrado el usuario con ID: {depositWithdrawalDTO.UserId}. {knfex.Message}");
        }
        catch (Exception ex)
        {
            _logger.LogError($"Error al hacer el depósito del usuario con ID: {depositWithdrawalDTO.UserId}. {ex.Message}");
            return BadRequest($"Error al hacer el depósito del usuario con ID: {depositWithdrawalDTO.UserId}. {ex.Message}");
        }
    }

    [HttpPost("Withdrawal/")]
    public IActionResult MakeWithdrawal([FromBody] DepositWithdrawalDTO depositWithdrawalDTO)
    {
        try {
            _userService.MakeWithdrawal(depositWithdrawalDTO);
            return Ok("Retiro realizado correctamente.");
        }     
        catch (KeyNotFoundException knfex)
        {
            _logger.LogWarning($"No se ha encontrado el usuario con ID: {depositWithdrawalDTO.UserId}. {knfex.Message}");
            return NotFound($"No se ha encontrado el usuario con ID: {depositWithdrawalDTO.UserId}. {knfex.Message}");
        }
        catch (Exception ex)
        {
            _logger.LogError($"Error al hacer el retiro del usuario con ID: {depositWithdrawalDTO.UserId}. {ex.Message}");
            return BadRequest($"Error al hacer el retiro del usuario con ID: {depositWithdrawalDTO.UserId}. {ex.Message}");
        }
    }

    [HttpPost("BuyCrypto/")]
    public IActionResult BuyCrypto([FromBody] BuySellCrypto buySellCrypto)
    {
        try {
            _userService.BuyCrypto(buySellCrypto);
            return Ok("Compra realizada correctamente.");
        }     
        catch (KeyNotFoundException knfex)
        {
            _logger.LogWarning($"No se ha encontrado el usuario con ID: {buySellCrypto.UserId}. {knfex.Message}");
            return NotFound($"No se ha encontrado el usuario con ID: {buySellCrypto.UserId}. {knfex.Message}");
        }
        catch (Exception ex)
        {
            _logger.LogError($"Error al hacer la compra del usuario con ID: {buySellCrypto.UserId}. {ex.Message}");
            return BadRequest($"Error al hacer la compra del usuario con ID: {buySellCrypto.UserId}. {ex.Message}");
        }
    }

    [HttpPost("SellCrypto/")]
    public IActionResult SellCrypto([FromBody] BuySellCrypto buySellCrypto)
    {
        try {
            _userService.SellCrypto(buySellCrypto);
            return Ok("Venta realizada correctamente.");
        }     
        catch (KeyNotFoundException knfex)
        {
            _logger.LogWarning($"No se ha encontrado el usuario con ID: {buySellCrypto.UserId}. {knfex.Message}");
            return NotFound($"No se ha encontrado el usuario con ID: {buySellCrypto.UserId}. {knfex.Message}");
        }
        catch (Exception ex)
        {
            _logger.LogError($"Error al hacer la venta del usuario con ID: {buySellCrypto.UserId}. {ex.Message}");
            return BadRequest($"Error al hacer la venta del usuario con ID: {buySellCrypto.UserId}. {ex.Message}");
        }
    }

    [HttpGet("Transactions/")]
    public ActionResult<IEnumerable<Transaction>> GetTransactions([FromQuery] TransactionQueryParameters transactionQueryParameters)
    {
        try {
            var transactions = _userService.GetAllTransactions(transactionQueryParameters);
            return Ok(transactions);
        }     
        catch (Exception ex)
        {
            _logger.LogError($"Error al obtener todas las transacciones. {ex.Message}");
            return BadRequest($"Error al obtener todas las transacciones. {ex.Message}");
        }
    }

    [HttpGet("MyCryptos/{userId}")]
    public ActionResult<IEnumerable<Transaction>> MyCryptos(int userId)
    {
        try {
            var userCryptos = _userService.MyCryptos(userId);
            return Ok(userCryptos);
        }     
        catch (Exception ex)
        {
            _logger.LogError($"Error al obtener las criptomonedas del usuario {userId}. {ex.Message}");
            return BadRequest($"Error al obtener las criptomonedas del usuario {userId}. {ex.Message}");
        }
    }

}
