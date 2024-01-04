namespace CryptoApp.Models;

public class Transaction
{
    public int Id { get; set; }
    public Crypto Crypto { get; set; }
    public string? Concept { get; set; }
    public DateTime Date { get; set; }
    public double Charge { get; set; }
    public double Amount { get; set; }
    public double Crypto_Price { get; set; }
    public string? Payment_Method { get; set; }

    public static int IdTransactionSeed;

    public Transaction() {}

    public Transaction (string concept, double amount, string paymentMethod)
    {
        Id = IdTransactionSeed++;
        Crypto = null;
        Concept = concept;
        Date = DateTime.Now;
        Charge = 1;
        Amount = amount;
        Crypto_Price = 0;
        Payment_Method = paymentMethod;
    }

    public Transaction (Crypto crypto, string concept, double amount, double cryptoPrice)
    {
        Id = IdTransactionSeed++;
        Crypto = crypto;
        Concept = concept;
        Date = DateTime.Now;
        Charge = 1;
        Amount = amount;
        Crypto_Price = cryptoPrice;
        Payment_Method = null;
    }

}
