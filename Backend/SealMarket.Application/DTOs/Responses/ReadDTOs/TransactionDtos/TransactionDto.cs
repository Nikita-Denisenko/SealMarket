namespace SealMarket.Application.DTOs.Responses.ReadDTOs.TransactionDtos
{
    public record TransactionDto
    (
        int Id,
        int AccountId,
        string Login,
        string Email,
        decimal Balance,
        decimal TotalSum,
        bool IsSuccessful,
        string Message,
        DateTime CreatedAt
    );
}