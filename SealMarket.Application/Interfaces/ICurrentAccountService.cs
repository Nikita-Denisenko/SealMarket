namespace SealMarket.Application.Interfaces
{
    public interface ICurrentAccountService
    {
        int? AccountId { get; }
        int? UserId { get; }
        string? Email { get; }
        string? Role { get; }
        bool IsAuthenticated { get; }
    }
}
