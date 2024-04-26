using System.ComponentModel.DataAnnotations;

namespace CryptoApp.Models;

public class BuySellCrypto
{
    [Range(1, int.MaxValue, ErrorMessage = "El ID del usuario no es válido")]
    public int UserId { get; set; }

    [Range(1, int.MaxValue, ErrorMessage = "El ID de la criptomoneda no es válido")]
    public int CryptoId { get; set; }

    [Range(10, double.MaxValue, ErrorMessage = "El saldo inicial debe ser mayor que 10")]
    public double Amount { get; set; }
}