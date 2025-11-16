using SealMarket.Core.Entities;

public class Account
{
    public int Id { get; private set; }
    public int UserId { get; private set; }
    public User? User { get; private set; }
    public decimal Balance { get; private set; }
    public string Login { get; private set; }
    public string PasswordHash { get; private set; }
    public string EmailAddress { get; private set; }
    public string PhoneNumber { get; private set; }
    public DateTime CreatedAt { get; private set; }
    public Cart? Cart { get; private set; }
    public List<Notification> Notifications { get; private set; }

    private Account() { }

    public Account(string login, string passwordHash, string email, string phone)
    {
        Login = login;
        PasswordHash = passwordHash;
        EmailAddress = email;
        PhoneNumber = phone;
        CreatedAt = DateTime.UtcNow;
        Balance = 0;
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

    public void CreateCart(string name = "Default Cart")
    {
        if (Cart != null)
            throw new InvalidOperationException("Account already has a cart");

        Cart = new Cart(name);
    }
}