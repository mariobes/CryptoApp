using Microsoft.AspNetCore.Mvc;

using CryptoApp.Business;
using CryptoApp.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authorization.Infrastructure;

namespace CryptoApp.API.Controllers;

[ApiController]
[Route("[controller]")]
public class CryptosController : ControllerBase
{
    private readonly ILogger<CryptosController> _logger;
    private readonly ICryptoService _cryptoService;
    private readonly ITransactionService _transactionService;

    public CryptosController(ILogger<CryptosController> logger, ICryptoService cryptoService, ITransactionService transactionService)
    {
        _logger = logger;
        _cryptoService = cryptoService;
        _transactionService = transactionService;
    }

    [HttpGet(Name = "GetAllCryptos")] 
    public ActionResult<IEnumerable<Crypto>> GetCryptos([FromQuery] CryptoQueryParameters cryptoQueryParameters)
    {
        if (!ModelState.IsValid)  {return BadRequest(ModelState); } 

        try 
        {
            var cryptos = _cryptoService.GetAllCryptos(cryptoQueryParameters);
            HttpContext.Response.Headers.Add("Access-Control-Allow-Origin", "http://localhost:5173");
            return Ok(cryptos);
        }
        catch (Exception ex)
        {
            return BadRequest(ex);
        }
    }

    [Authorize(Roles = Roles.Admin + "," + Roles.User)]
    [HttpGet("{cryptoId}")]
    public IActionResult GetCrypto(int cryptoId)
    {
        try
        {
            var crypto = _cryptoService.GetCryptoById(cryptoId);
            return Ok(crypto);
        }
        catch (KeyNotFoundException knfex)
        {
            _logger.LogWarning($"No se ha encontrado la criptomoneda con ID: {cryptoId}. {knfex.Message}");
           return NotFound($"No se ha encontrado la criptomoneda con ID: {cryptoId}. {knfex.Message}");
        }
        catch (Exception ex)
        {
            _logger.LogError($"Error al obtener la criptomoneda con ID: {cryptoId}. {ex.Message}");
            return BadRequest($"Error al obtener la criptomoneda con ID: {cryptoId}. {ex.Message}");
        }
    }

    [Authorize(Roles = Roles.Admin)]
    [HttpPost]
    public IActionResult CreateCrypto([FromBody] CryptoCreateUpdateDTO cryptoCreateUpdateDTO)
    {
        if (!ModelState.IsValid)  {return BadRequest(ModelState); } 

        try {
            var crypto = _cryptoService.RegisterCrypto(cryptoCreateUpdateDTO);
            HttpContext.Response.Headers.Add("Access-Control-Allow-Origin", "http://localhost:5173");
            return CreatedAtAction(nameof(GetCrypto), new { cryptoId = crypto.Id }, crypto);
        }     
        catch (Exception ex)
        {
            _logger.LogError($"Error al registrar la criptomoneda. {ex.Message}");
            return BadRequest($"Error al registrar la criptomoneda. {ex.Message}");
        }
    }

    [Authorize(Roles = Roles.Admin)]
    [HttpPut("{cryptoId}")]
    public IActionResult UpdateCrypto(int cryptoId, CryptoCreateUpdateDTO cryptoCreateUpdateDTO)
    {
        if (!ModelState.IsValid)  {return BadRequest(ModelState); } 

        try {
            _cryptoService.UpdateCrypto(cryptoId, cryptoCreateUpdateDTO);
            HttpContext.Response.Headers.Add("Access-Control-Allow-Origin", "http://localhost:5173");
            return NoContent();
        }     
        catch (KeyNotFoundException knfex)
        {
            _logger.LogWarning($"No se ha encontrado la criptomoneda con ID: {cryptoId}. {knfex.Message}");
            return NotFound($"No se ha encontrado la criptomoneda con ID: {cryptoId}. {knfex.Message}");
        }
        catch (Exception ex)
        {
            _logger.LogError($"Error al actualizar la criptomoneda con ID: {cryptoId}. {ex.Message}");
            return BadRequest($"Error al actualizar la criptomoneda con ID: {cryptoId}. {ex.Message}");
        }
    }

    [Authorize(Roles = Roles.Admin)]
    [HttpDelete("{cryptoId}")]
    public IActionResult DeleteCrypto(int cryptoId)
    {
        try
        {
            
            if (!_transactionService.IsCryptoPurchased(cryptoId))
            {
                _cryptoService.DeleteCrypto(cryptoId);
            }
            HttpContext.Response.Headers.Add("Access-Control-Allow-Origin", "http://localhost:5173");
            return NoContent();
        }
        catch (KeyNotFoundException knfex)
        {
            _logger.LogWarning($"No se ha encontrado la criptomoneda con ID: {cryptoId}. {knfex.Message}");
            return NotFound($"No se ha encontrado la criptomoneda con ID: {cryptoId}. {knfex.Message}");
        }
        catch (Exception ex)
        {
            _logger.LogError($"Error al eliminar la criptomoneda con ID: {cryptoId}. {ex.Message}");
            return BadRequest($"Error al eliminar la criptomoneda con ID: {cryptoId}. {ex.Message}");
        }
    }

}
