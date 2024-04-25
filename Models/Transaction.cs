using System.ComponentModel.DataAnnotations;

namespace CryptoApp.Models;

public class Transaction
{
    [Key]
    public int Id { get; set; }

    [Required]
    public int UserId { get; set; }
    public User User { get; set; }

    public int? CryptoId { get; set; }
    public Crypto Crypto { get; set; }

    [Required]
    public string? Concept { get; set; }

    [Required]
    public double Amount { get; set; }
    
    public DateTime Date { get; set; }
    public double Charge { get; set; }
    public string? Payment_Method { get; set; }

    public static int IdTransactionSeed;

    public Transaction() {}

    public Transaction (int userId, string concept, double amount, string paymentMethod)
    {
        Id = IdTransactionSeed++;
        UserId = userId;
        CryptoId = null;
        Concept = concept;
        Amount = amount;
        Date = DateTime.Now;
        Charge = 1;
        Payment_Method = paymentMethod;
    }

    public Transaction (int userId, int cryptoId, string concept, double amount, double cryptoPrice)
    {
        Id = IdTransactionSeed++;
        UserId = userId;
        CryptoId = cryptoId;
        Concept = concept;
        Amount = amount;
        Date = DateTime.Now;
        Charge = 1;
        Payment_Method = null;
    }

}
