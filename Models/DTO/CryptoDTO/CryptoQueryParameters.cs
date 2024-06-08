using System.ComponentModel.DataAnnotations;

namespace CryptoApp.Models;

public class CryptoQueryParameters
{
    [Required]
    [RegularExpression("value|name", ErrorMessage = "El valor debe ser 'value' o 'name'")]
    public string SortBy { get; set; }

    [RegularExpression("asc|desc", ErrorMessage = "El valor debe ser 'asc' o 'desc'")]
     public string Order { get; set; } = "asc";

    [StringLength(50, ErrorMessage = "El nombre debe tener menos de 50 caracteres")]
    public string? Name { get; set; }

    [StringLength(10, ErrorMessage = "La abreviatura debe tener menos de 10 caracteres")]
    public string? Symbol { get; set; }

    [StringLength(50, ErrorMessage = "El desarrollador debe tener menos de 50 caracteres")]
    public string? Developer { get; set; }
}