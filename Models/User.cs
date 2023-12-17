namespace CryptoApp.Models;

public class User
{
    public static int Id { get; set; } = 1;
    public string? Name { get; set; }
    public DateTime Birthdate { get; set; }
    public string? Email { get; set; }
    public string? Password { get; set; }
    public string? Phone { get; set; }
    public string? DNI { get; set; }
    public string? Country { get; set; }
    public double Wallet { get; set; }
    public List<Transaction> transactions { get; set; }

    public User() {}

    public User(string name, DateTime birthdate, string email, string password, string phone, string dni, string country) 
    {
        Name = name;
        Birthdate = birthdate;
        Email = email;
        Password = password;
        Phone = phone;
        DNI = dni;
        Country = country;
        Wallet = 0.0;
        transactions = new List<Transaction>();
    }
}
