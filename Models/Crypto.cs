using System.ComponentModel.DataAnnotations;

namespace CryptoApp.Models;

public class Crypto
{
    [Key]
    public int Id { get; set; }

    [Required]
    public string? Name { get; set; }

    [Required]
    public string? Symbol { get; set; }

    [Required]
    public string? Description { get; set; }

    [Required]
    public DateTime RegisterDate { get; set; }

    [Required]
    public double Value { get; set; }

    [Required]
    public string? Developer { get; set; }
    
    [Required]
    public bool Descentralized { get; set; }

    public Crypto() {}

    public Crypto(string name, string symbol, string description, double value, string developer, bool descentralized) 
    {
        Name = name;
        Symbol = symbol;
        Description = description;
        RegisterDate = DateTime.Now;
        Value = value;
        Developer = developer;
        Descentralized = descentralized;
    }
}
