public class User
{
    public int Id { get; private set; }
    public string Name { get; private set; }
    public DateOnly BirthDate { get; private set; }
    public string City { get; private set; }
    public Account? Account { get; private set; }

    private User() { }

    public User(string name, DateOnly birthDate, string city)
    {
        Name = name;
        BirthDate = birthDate;
        City = city;
    }

    public void CreateAccount(string login, string password, string email, string phone)
    {
        if (Account != null)
            throw new InvalidOperationException("User already has an account");

        Account = new Account(login, password, email, phone, Id);
        Console.WriteLine("Account was Created!");
    }

    public void UpdatePersonalInfo(string name, string city)
    {
        Name = name;
        City = city;
    }
}