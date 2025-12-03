namespace SealMarket.Application.DTOs.Requests.UpdateDTOs
{
    public record UpdateAccountDto
    (
        string? Login = null,
        string? Password = null,
        string? Email = null,
        string? PhoneNumber = null
    );
}
