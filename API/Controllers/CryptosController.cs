using Microsoft.AspNetCore.Mvc;

using CryptoApp.Data;
using CryptoApp.Business;
using CryptoApp.Models;

namespace CryptoApp.API.Controllers;

[ApiController]
[Route("[controller]")]
public class CryptosController : ControllerBase
{
    private readonly ILogger<UsersController> _logger;
    private readonly ICryptoService _cryptoService;

    public CryptosController(ILogger<UsersController> logger, ICryptoService cryptoService)
    {
        _logger = logger;
        _cryptoService = cryptoService;
    }

    [HttpGet(Name = "GetAllCryptos")] 
    public ActionResult<IEnumerable<Crypto>> GetCryptos([FromQuery] CryptoQueryParameters cryptoQueryParameters)
    {
        if (!ModelState.IsValid)  {return BadRequest(ModelState); } 

        try 
        {
            var cryptos = _cryptoService.GetAllCryptos(cryptoQueryParameters);
            return Ok(cryptos);
        }
        catch (Exception ex)
        {
            return BadRequest(ex);
        }
    }

    /*[HttpGet]
    public ActionResult<IEnumerable<Crypto>> GetAllCryptos() //ESTE METODO LO QUITAREMOS Y SOLO TENDREMOS EL SEARCH. NO VAMOS A TENER UNO CON FILTRO Y UNO SIN FILTRO. SERA DAME TODOS Y ADEMAS 
                                                          //ACEPTO PARAMETROS DE ENTRADA. ES DECIR EN EL SEARCH PONEMOS PARAMETROS DE ENTRADA, PERO DE PRIMERAS YA NOS DA TODOS LOS RESULTADOS, 
                                                          //SI QUEREMOS INTRODUCIMOS PARAMETROS DE ENTRADA PARA FILTRAR O NO
    {
        try {
            var cryptos = _cryptoService.GetAllCryptos();
            return Ok(cryptos);
        }     
        catch (Exception ex)
        {
            _logger.LogError($"Error al obtener todas las criptomonedas. {ex.Message}");
            return BadRequest($"Error al obtener todas las criptomonedas. {ex.Message}");
        }
    }*/

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

    [HttpPost]
    public IActionResult CreateCrypto([FromBody] CryptoCreateUpdateDTO cryptoCreateUpdateDTO)
    {
        if (!ModelState.IsValid)  {return BadRequest(ModelState); } 

        try {
            var crypto = _cryptoService.RegisterCrypto(cryptoCreateUpdateDTO);
            return CreatedAtAction(nameof(GetCrypto), new { cryptoId = crypto.Id }, crypto);
        }     
        catch (Exception ex)
        {
            _logger.LogError($"Error al registrar la criptomoneda. {ex.Message}");
            return BadRequest($"Error al registrar la criptomoneda. {ex.Message}");
        }
    }

    [HttpPut("{cryptoId}")]
    public IActionResult UpdateCrypto(int cryptoId, CryptoCreateUpdateDTO cryptoCreateUpdateDTO)
    {
        if (!ModelState.IsValid)  {return BadRequest(ModelState); } 

        try {
            _cryptoService.UpdateCrypto(cryptoId, cryptoCreateUpdateDTO);
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

    [HttpDelete("{cryptoId}")]
    public IActionResult DeleteCrypto(int cryptoId)
    {
        try
        {
            _cryptoService.DeleteCrypto(cryptoId);
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
