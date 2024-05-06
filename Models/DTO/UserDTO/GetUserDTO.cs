using CryptoApp.Models;

namespace CryptoApp.Domain;

public class GetUserDTO
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
    public List<int>? Transactions { get; set; }
    public string Role { get; set; }

    public GetUserDTO() {}
    public GetUserDTO(User user)
    {
        Id = user.Id;
        Name = user.Name;
        Birthdate = user.Birthdate;
        Email = user.Email;
        Password = user.Password;
        Phone = user.Phone;
        DNI = user.DNI;
        Nationality = user.Nationality;
        Cash = user.Cash;
        Wallet = user.Wallet;
        Transactions = new List<int>();
        Role = user.Role;

        if (user.Transactions != null)
        {
            foreach (var transaction in user.Transactions)
            {
                Transactions.Add(transaction.Id);
            }
        }
    }
}