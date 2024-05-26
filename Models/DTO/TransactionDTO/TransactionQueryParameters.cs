using System.ComponentModel.DataAnnotations;

namespace CryptoApp.Models;

public class TransactionQueryParameters
{
    [Range(1, int.MaxValue, ErrorMessage = "El ID de la criptomoneda no es válido")]
    public int? CryptoId { get; set; }

    public DateTime? FromDate { get; set; }
    public DateTime? ToDate { get; set; }

    [StringLength(20, ErrorMessage = "El concepto debe tener menos de 20 caracteres")]
    public string? Concept { get; set; }
}