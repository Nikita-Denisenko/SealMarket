namespace SealMarket.Application.DTOs.Responses.CreatedDTOs
{
    public record CreatedAccountDto
    (
       int Id,
       int UserId,
       string Login,
       DateTime CreatedAt
    );
}
