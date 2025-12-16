namespace SealMarket.Application.Constants
{
    static class Roles
    {
        public const string Customer = nameof(Customer);
        public const string Admin = nameof(Admin);

        public static bool IsValid(string role)
        => role == Customer || role == Admin;
    }
}
