using Microsoft.AspNetCore.Mvc;
using CryptoApp.Business;
using CryptoApp.Models;
using Microsoft.AspNetCore.Authorization;

namespace CryptoApp.API.Controllers;

[ApiController]
[Route("[controller]")]
public class TransactionsController : ControllerBase
{
    private readonly ILogger<TransactionsController> _logger;
    private readonly IUserService _userService;
    private readonly IAuthService _authService;

    public TransactionsController(ILogger<TransactionsController> logger, IUserService userService, IAuthService authService)
    {
        _logger = logger;
        _userService = userService;
        _authService = authService;
    }

    [Authorize(Roles = Roles.Admin + "," + Roles.User)]
    [HttpPost("deposit")]
    public IActionResult MakeDeposit([FromBody] DepositWithdrawalDTO depositWithdrawalDTO)
    {
        if (!_authService.HasAccessToResource(Convert.ToInt32(depositWithdrawalDTO.UserId), HttpContext.User)) 
            {return Forbid(); }

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

    [Authorize(Roles = Roles.Admin + "," + Roles.User)]
    [HttpPost("withdrawal")]
    public IActionResult MakeWithdrawal([FromBody] DepositWithdrawalDTO depositWithdrawalDTO)
    {
        if (!_authService.HasAccessToResource(Convert.ToInt32(depositWithdrawalDTO.UserId), HttpContext.User)) 
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
    [HttpPost("buycrypto")]
    public IActionResult BuyCrypto([FromBody] BuySellCrypto buySellCrypto)
    {
        if (!_authService.HasAccessToResource(Convert.ToInt32(buySellCrypto.UserId), HttpContext.User)) 
            {return Forbid(); }

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

    [Authorize(Roles = Roles.Admin + "," + Roles.User)]
    [HttpPost("sellcrypto")]
    public IActionResult SellCrypto([FromBody] BuySellCrypto buySellCrypto)
    {
        if (!_authService.HasAccessToResource(Convert.ToInt32(buySellCrypto.UserId), HttpContext.User)) 
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
    [HttpGet("{userId}")]
    public ActionResult<IEnumerable<Transaction>> GetTransactions(int userId, [FromQuery] TransactionQueryParameters transactionQueryParameters)
    {
        if (!_authService.HasAccessToResource(Convert.ToInt32(userId), HttpContext.User)) 
            {return Forbid(); }

        try {
            var transactions = _userService.GetAllTransactions(userId, transactionQueryParameters);
            return Ok(transactions);
        }     
        catch (Exception ex)
        {
            _logger.LogError($"Error al obtener todas las transacciones. {ex.Message}");
            return BadRequest($"Error al obtener todas las transacciones. {ex.Message}");
        }
    }

    [Authorize(Roles = Roles.Admin + "," + Roles.User)]
    [HttpGet("{userId}/mycryptos")]
    public ActionResult<IEnumerable<Transaction>> MyCryptos(int userId)
    {
        if (!_authService.HasAccessToResource(Convert.ToInt32(userId), HttpContext.User)) 
            {return Forbid(); }

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
