namespace CryptoApp.Models;

public class Transaction
{
    public static int Id { get; set; } = 1;
    public int UserId { get; set; }
    public int CryptoId { get; set; }
    public DateTime Date { get; set; }
    public double Charge { get; set; } = 1.0;
    public decimal TotalPrice { get; set; }
}
