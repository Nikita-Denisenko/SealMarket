namespace SealMarket.Application.DTOs.Responses.ReadDTOs
{
    public record ReadCartItemDto
    (
        int Id,
        int ProductId,
        int CartId,
        int Quantity,
        DateTime AddedAt,
        decimal ProductPrice,
        decimal TotalPrice
    );
}
