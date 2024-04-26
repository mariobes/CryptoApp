using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CryptoApp.Models;

public class Transaction
{
    [Key]
    public int Id { get; set; }

    //[ForeignKey("User")]
    [Required]
    public int UserId { get; set; }

    public User User { get; set; }

    //[ForeignKey("Crypto")]
    public int? CryptoId { get; set; }
    
    public Crypto Crypto { get; set; }

    [Required]
    public string? Concept { get; set; }

    [Required]
    public double Amount { get; set; }
    
    public DateTime Date { get; set; }
    public double Charge { get; set; }
    public string? Payment_Method { get; set; }

    public Transaction() {}

    public Transaction (int userId, string concept, double amount, string paymentMethod)
    {
        UserId = userId;
        CryptoId = null;
        Concept = concept;
        Amount = amount;
        Date = DateTime.Now;
        Charge = 1;
        Payment_Method = paymentMethod;
    }

    public Transaction (int userId, int cryptoId, string concept, double amount)
    {
        UserId = userId;
        CryptoId = cryptoId;
        Concept = concept;
        Amount = amount;
        Date = DateTime.Now;
        Charge = 1;
        Payment_Method = null;
    }

}
