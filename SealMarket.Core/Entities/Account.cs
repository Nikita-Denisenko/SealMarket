using SealMarket.Core.Entities;
using System.Globalization;

public class Account
{
    public int Id { get; private set; }
    public int UserId { get; private set; }
    public User User { get; private set; }
    public decimal Balance { get; private set; }
    public string Login { get; private set; }
    public string Password { get; private set; }
    public string Email { get; private set; }
    public string PhoneNumber { get; private set; }
    public DateTime CreatedAt { get; private set; }
    public Cart Cart { get; private set; }
    public List<Notification> Notifications { get; private set; }

    private Account() { }

    public Account
    (
        int userId,
        string login, 
        string password, 
        string email, 
        string phoneNumber
    )
    {
        Login = login;
        Password = password;
        Email = email;
        PhoneNumber = phoneNumber;
        CreatedAt = DateTime.UtcNow;
        Balance = 0;
        UserId = userId;
    }

    public void Deposit(decimal quantity) => Balance += quantity;

    public void Withdraw(decimal quantity)
    {
        if (quantity <= 0)
            throw new ArgumentException("Quantity must be positive number.");

        if (quantity > Balance)
            throw new ArgumentException("Quantity must not be more than balance.");

        Balance -= quantity;
    }

    public void UpdateAcccountData
    (
        string login, 
        string password, 
        string email, 
        string phoneNumber
    )
    {
        Login = login;
        Password = password;
        Email = email;
        PhoneNumber = phoneNumber;
    }
}