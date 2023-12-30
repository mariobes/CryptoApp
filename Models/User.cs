namespace CryptoApp.Models;

public class User
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public DateTime Birthdate { get; set; }
    public string? Email { get; set; }
    public string? Password { get; set; }
    public string? Phone { get; set; }
    public string? DNI { get; set; }
    public string? Nationality { get; set; }
    public double Cash { get; set; }
    public double Wallet { get; set; }
    public List<Transaction> Transactions { get; set; }

    public static int UserIdSeed { get; set; }

    public User() {}

    public User(string name, DateTime birthdate, string email, string password, string phone, string dni, string nationality) 
    {
        Id = UserIdSeed++;
        Name = name;
        Birthdate = birthdate;
        Email = email;
        Password = password;
        Phone = phone;
        DNI = dni;
        Nationality = nationality;
        Cash = 0.0;
        Wallet = 0.0;
        Transactions = new List<Transaction>();
    }
}
