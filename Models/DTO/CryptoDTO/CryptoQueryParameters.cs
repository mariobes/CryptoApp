using System.ComponentModel.DataAnnotations;

namespace CryptoApp.Models;

public class CryptoQueryParameters
{
    [StringLength(50, ErrorMessage = "El nombre debe tener menos de 50 caracteres")]
    public string? Name { get; set; }

    [StringLength(10, ErrorMessage = "La abreviatura debe tener menos de 10 caracteres")]
    public string? Symbol { get; set; }

    [StringLength(50, ErrorMessage = "El desarrollador debe tener menos de 50 caracteres")]
    public string? Developer { get; set; }
}