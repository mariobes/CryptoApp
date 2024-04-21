using Microsoft.AspNetCore.Mvc;

using CryptoApp.Data;
using CryptoApp.Business;
using CryptoApp.Models;

namespace CryptoApp.API.Controllers;

[ApiController]
[Route("[controller]")]
public class CryptosController : ControllerBase
{
    private readonly ICryptoService _cryptoService;

    public CryptosController(ICryptoService cryptoService)
    {
   
        _cryptoService = cryptoService;
    }

    [HttpGet(Name = "GetAllCryptos")]
    public ActionResult<IEnumerable<Crypto>> GetCryptos()
    {
        try {
            var cryptos = _cryptoService.GetAllCryptos();
            return Ok(cryptos);
        }     
        catch (Exception ex)
        {
            return BadRequest(ex);
        }
        
    }

    [HttpGet("{cryptoId}", Name = "GetCrypto")]
    public IActionResult GetCrypto(string cryptoId)
    {
        try
        {
            var crypto = _cryptoService.GetCryptoById(cryptoId);
            return Ok(crypto);
        }
        catch (KeyNotFoundException)
        {
           return NotFound("No encontrado la criptomoneda " + cryptoId);
        }
    }

    [HttpPost]
    public IActionResult RegisterCrypto([FromBody] CryptoCreateUpdateDTO cryptoCreateUpdateDTO)
    {
        if (!ModelState.IsValid)  {return BadRequest(ModelState); } 

        try {
            var crypto = _cryptoService.RegisterCrypto(cryptoCreateUpdateDTO);
            return CreatedAtAction(nameof(GetCrypto), new { cryptoId = crypto.Id }, crypto);
        }     
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPut("{cryptoId}")]
    public IActionResult UpdateCrypto(string cryptoId, CryptoCreateUpdateDTO cryptoCreateUpdateDTO)
    {
        if (!ModelState.IsValid)  {return BadRequest(ModelState); } 

        try {
            _cryptoService.UpdateCrypto(cryptoId, cryptoCreateUpdateDTO);
            return NoContent();
        }     
        catch (KeyNotFoundException)
        {
            return NotFound();
        }
    }

    [HttpDelete("{cryptoId}")]
    public IActionResult DeleteCrypto(string cryptoId)
    {
        try
        {
            _cryptoService.DeleteCrypto(cryptoId);
            return NoContent();
        }
        catch (KeyNotFoundException)
        {
            return NotFound();
        }
    }

}
