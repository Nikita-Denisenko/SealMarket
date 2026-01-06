namespace SealMarket.Application.DTOs.Responses.ReadDTOs.TransactionDtos
{
    public record ShortTransactionDto
    (
        int Id,
        int AccountId,
        decimal TotalSum,
        bool IsSuccessful,
        string Message,
        DateTime CreatedAt
    );
}
