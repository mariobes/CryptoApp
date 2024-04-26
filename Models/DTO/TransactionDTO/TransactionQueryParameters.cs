using System.ComponentModel.DataAnnotations;

namespace CryptoApp.Models;

public class TransactionQueryParameters
{
    [Required]
    [Range(1, int.MaxValue, ErrorMessage = "El ID del usuario no es válido")]
    public int UserId { get; set; }

    [Range(1, int.MaxValue, ErrorMessage = "El ID de la criptomoneda no es válido")]
    public int? CryptoId { get; set; }

    public DateTime? FromDate { get; set; }
    public DateTime? ToDate { get; set; }

    [StringLength(20, ErrorMessage = "El mconcepto debe tener menos de 20 caracteres")]
    public string? Concept { get; set; }
}