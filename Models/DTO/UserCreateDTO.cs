using System.ComponentModel.DataAnnotations;

namespace CryptoApp.Models;

public class UserCreateDTO
{
    [Required]
    [StringLength(50, ErrorMessage = "El nombre debe tener menos de 50 caracteres")]
    public string? Name { get; set; }

    [Required]
    //[RegularExpression(@"^(0[1-9]|[12][0-9]|3[01])-(0[1-9]|1[0-2])-\d{4}$", ErrorMessage = "El formato de fecha debe ser: dd-mm-yyyy")]
    public DateTime Birthdate { get; set; }

    [Required]
    [EmailAddress(ErrorMessage = "El correo electrónico no es válido")]
    public string? Email { get; set; }

    [Required]
    [MinLength(8, ErrorMessage = "La contraseña debe tener al menos 8 caracteres")]
    public string? Password { get; set; }

    [Required]
    [Phone(ErrorMessage = "El número de teléfono no es válido")]
    public string? Phone { get; set; }

    [Required]
    public string? DNI { get; set; }

    [Required]
    public string? Nationality { get; set; }

}