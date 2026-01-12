public class User
{
    public int Id { get; private set; }
    public string Name { get; private set; } = string.Empty;
    public DateOnly BirthDate { get; private set; }
    public string City { get; private set; } = string.Empty;
    public string AvatarUrl { get; private set; } = string.Empty;
    public Account? Account { get; private set; }

    private User() { }

    public User(string name, DateOnly birthDate, string city)
    {
        Name = name;
        BirthDate = birthDate;
        City = city;
    }

    public void UpdatePersonalInfo(string name, string city, string avatarUrl)
    {
        Name = name;
        City = city;
        AvatarUrl = avatarUrl;
    }
} 