using Microsoft.AspNetCore.Mvc;
using CryptoApp.Business;
using CryptoApp.Models;
using Microsoft.AspNetCore.Authorization;

namespace CryptoApp.API.Controllers;

[ApiController]
[Route("[controller]")]
public class UsersController : ControllerBase
{
    private readonly ILogger<UsersController> _logger;
    private readonly IUserService _userService;
    private readonly IAuthService _authService;

    public UsersController(ILogger<UsersController> logger, IUserService userService, IAuthService authService)
    {
        _logger = logger;
        _userService = userService;
        _authService = authService;
    }

    [Authorize(Roles = Roles.Admin)]
    [HttpGet]
    public ActionResult<IEnumerable<User>> GetAllUsers()
    {
        try 
        {
            var users = _userService.GetAllUsers();
            HttpContext.Response.Headers.Add("Access-Control-Allow-Origin", "http://localhost:5173");
            return Ok(users);
        }     
        catch (Exception ex)
        {
            _logger.LogError($"Error al obtener todos los usuarios. {ex.Message}");
            return BadRequest($"Error al obtener todos los usuarios. {ex.Message}");
        }
    }

    [Authorize(Roles = Roles.Admin + "," +  Roles.User)]
    [HttpGet("{userId}")]
    public IActionResult GetUser(int userId)
    {
        if (!_authService.HasAccessToResource(Convert.ToInt32(userId), null, HttpContext.User)) 
            {return Forbid(); }

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

    [Authorize(Roles = Roles.Admin + "," +  Roles.User)]
    [HttpGet("ByEmail")]
    public IActionResult GetUserByEmail(string userEmail)
    {
        if (!_authService.HasAccessToResource(null, userEmail, HttpContext.User)) 
            {return Forbid(); }

        try
        {
            var user = _userService.GetUserByEmail(userEmail);
            return Ok(user);
        }
        catch (KeyNotFoundException knfex)
        {
            _logger.LogWarning($"No se ha encontrado el usuario con email: {userEmail}. {knfex.Message}");
           return NotFound($"No se ha encontrado el usuario con email: {userEmail}. {knfex.Message}");
        }
        catch (Exception ex)
        {
            _logger.LogError($"Error al obtener el usuario con email: {userEmail}. {ex.Message}");
            return BadRequest($"Error al obtener el usuario con email: {userEmail}. {ex.Message}");
        }
    }

    [Authorize(Roles = Roles.Admin + "," + Roles.User)]
    [HttpPut("{userId}")]
    public IActionResult UpdateUser(int userId, UserUpdateDTO userUpdateDTO)
    {
        if (!ModelState.IsValid)  {return BadRequest(ModelState); } 

        if (!_authService.HasAccessToResource(Convert.ToInt32(userId), null, HttpContext.User)) 
            {return Forbid(); }

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

    [Authorize(Roles = Roles.Admin + "," + Roles.User)]
    [HttpDelete("{userId}")]
    public IActionResult DeleteUser(int userId)
    {
        if (!_authService.HasAccessToResource(Convert.ToInt32(userId), null, HttpContext.User)) 
            {return Forbid(); }

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

    [Authorize(Roles = Roles.Admin + "," + Roles.User)]
    [HttpPost("Deposit/")]
    public IActionResult MakeDeposit([FromBody] DepositWithdrawalDTO depositWithdrawalDTO)
    {
        if (!_authService.HasAccessToResource(Convert.ToInt32(depositWithdrawalDTO.UserId), null, HttpContext.User)) 
            {return Forbid(); }

        try {
            _userService.MakeDeposit(depositWithdrawalDTO);
            HttpContext.Response.Headers.Add("Access-Control-Allow-Origin", "http://localhost:5173");
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

    [Authorize(Roles = Roles.Admin + "," + Roles.User)]
    [HttpPost("Withdrawal/")]
    public IActionResult MakeWithdrawal([FromBody] DepositWithdrawalDTO depositWithdrawalDTO)
    {
        if (!_authService.HasAccessToResource(Convert.ToInt32(depositWithdrawalDTO.UserId), null, HttpContext.User)) 
            {return Forbid(); }

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

    [Authorize(Roles = Roles.Admin + "," + Roles.User)]
    [HttpPost("BuyCrypto/")]
    public IActionResult BuyCrypto([FromBody] BuySellCrypto buySellCrypto)
    {
        if (!_authService.HasAccessToResource(Convert.ToInt32(buySellCrypto.UserId), null, HttpContext.User)) 
            {return Forbid(); }

        try {
            _userService.BuyCrypto(buySellCrypto);
            HttpContext.Response.Headers.Add("Access-Control-Allow-Origin", "http://localhost:5173");
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

    [Authorize(Roles = Roles.Admin + "," + Roles.User)]
    [HttpPost("SellCrypto/")]
    public IActionResult SellCrypto([FromBody] BuySellCrypto buySellCrypto)
    {
        if (!_authService.HasAccessToResource(Convert.ToInt32(buySellCrypto.UserId), null, HttpContext.User)) 
            {return Forbid(); }

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

    [Authorize(Roles = Roles.Admin + "," + Roles.User)]
    [HttpGet("Transactions/")]
    public ActionResult<IEnumerable<Transaction>> GetTransactions([FromQuery] TransactionQueryParameters transactionQueryParameters)
    {
        if (!_authService.HasAccessToResource(Convert.ToInt32(transactionQueryParameters.UserId), null, HttpContext.User)) 
            {return Forbid(); }

        try {
            var transactions = _userService.GetAllTransactions(transactionQueryParameters);
            HttpContext.Response.Headers.Add("Access-Control-Allow-Origin", "http://localhost:5173");
            return Ok(transactions);
        }     
        catch (Exception ex)
        {
            _logger.LogError($"Error al obtener todas las transacciones. {ex.Message}");
            return BadRequest($"Error al obtener todas las transacciones. {ex.Message}");
        }
    }

    [Authorize(Roles = Roles.Admin + "," + Roles.User)]
    [HttpGet("MyCryptos/{userId}")]
    public ActionResult<IEnumerable<Transaction>> MyCryptos(int userId)
    {
        if (!_authService.HasAccessToResource(Convert.ToInt32(userId), null, HttpContext.User)) 
            {return Forbid(); }

        try {
            var userCryptos = _userService.MyCryptos(userId);
            HttpContext.Response.Headers.Add("Access-Control-Allow-Origin", "http://localhost:5173");
            return Ok(userCryptos);
        }     
        catch (Exception ex)
        {
            _logger.LogError($"Error al obtener las criptomonedas del usuario {userId}. {ex.Message}");
            return BadRequest($"Error al obtener las criptomonedas del usuario {userId}. {ex.Message}");
        }
    }

}
