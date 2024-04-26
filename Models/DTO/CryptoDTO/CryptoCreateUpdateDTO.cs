using System.ComponentModel.DataAnnotations;

namespace CryptoApp.Models;

public class CryptoCreateUpdateDTO
{
    [Required]
    [StringLength(50, ErrorMessage = "El nombre debe tener menos de 50 caracteres")]
    public string? Name { get; set; }

    [Required]
    [StringLength(10, ErrorMessage = "La abreviatura debe tener menos de 10 caracteres")]
    public string? Symbol { get; set; }

    [Required]
    [StringLength(500, ErrorMessage = "La descripción debe tener menos de 500 caracteres")]
    public string? Description { get; set; }

    [Required]
    [Range(0, double.MaxValue, ErrorMessage = "El valor no puede ser negativo")]
    public double Value { get; set; }

    [Required]
    [StringLength(50, ErrorMessage = "El desarrollador debe tener menos de 50 caracteres")]
    public string? Developer { get; set; }

    [Required(ErrorMessage = "El valor debe ser 'true' o 'false'")]
    public bool Descentralized { get; set; }
}