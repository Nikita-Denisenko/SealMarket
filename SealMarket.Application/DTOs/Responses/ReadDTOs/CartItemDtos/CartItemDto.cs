namespace SealMarket.Application.DTOs.Responses.ReadDTOs
{
    public record CartItemDto
    (
        int Id,
        int ProductId,
        int Quantity,
        DateTime AddedAt,
        string ProductName,
        string ProductImageUrl,
        decimal ProductPrice,
        decimal TotalPrice
    );
}
