namespace CryptoApp.Models;

public class Crypto
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public string? Symbol { get; set; }
    public string? Description { get; set; }
    public DateTime RegisterDate { get; set; }
    public double Value { get; set; }
    public string? Developer { get; set; }
    public bool Descentralized { get; set; }
    
    public static int CryptoIdSeed { get; set; }

    public Crypto() {}

    public Crypto(string name, string symbol, string description, double value, string developer, bool descentralized) 
    {
        Id = CryptoIdSeed++;
        Name = name;
        Symbol = symbol;
        Description = description;
        RegisterDate = DateTime.Now;
        Value = value;
        Developer = developer;
        Descentralized = descentralized;
    }
}
