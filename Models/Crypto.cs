namespace CryptoApp.Models;

public class Crypto
{
    public static int Id { get; set; } = 1;
    public string? Name { get; set; }
    public string? Description { get; set; }
    public DateTime RegisterDate { get; set; }
    public decimal Value { get; set; }
    public bool Profit { get; set; }


    public Crypto() {}

    public Crypto(string name, string description, decimal value, bool profit) 
    {
        Name = name;
        Description = description;
        RegisterDate = DateTime.Now;
        Value = value;
        Profit = profit;
    }
}
