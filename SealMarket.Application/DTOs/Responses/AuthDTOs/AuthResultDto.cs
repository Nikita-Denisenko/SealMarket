namespace SealMarket.Application.DTOs.Responses.AuthDTOs
{
    public record AuthResultDto
    (
        string Token,
        int AccountId,
        string Email,
        string FullName,
        string Role 
    );
}
