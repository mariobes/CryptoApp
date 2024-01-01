namespace CryptoApp.Models;

public class Transaction
{
    public int Id { get; set; }
    public Crypto Crypto { get; set; }
    public string? Concept { get; set; }
    public DateTime Date { get; set; }
    public double Charge { get; set; }
    public double Amount { get; set; }
    public string? Payment_Method { get; set; }

    public static int IdTransactionSeed;

    public Transaction() {}

    public Transaction (string concept, double amount, string paymentMethod)
    {
        Id = IdTransactionSeed++;
        Concept = concept;
        Date = DateTime.Now;
        Charge = 1;
        Amount = amount;
        Payment_Method = paymentMethod;
    }


}
