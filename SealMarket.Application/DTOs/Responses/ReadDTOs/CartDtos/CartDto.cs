namespace SealMarket.Application.DTOs.Responses.ReadDTOs.CartDtos
{
    public record CartDto
    (
        int Id,
        string Name,
        decimal TotalPrice,
        List<CartItemDto> CartItems
    );
}
