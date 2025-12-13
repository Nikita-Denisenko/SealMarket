namespace SealMarket.Application.DTOs.Responses.ReadDTOs
{
    public record ReadCartItemDto
    (
        int Id,
        int ProductId,
        int CartId,
        int Quantity,
        DateTime AddedAt,
        string ProductName,
        decimal ProductPrice,
        decimal TotalPrice
    );
}
