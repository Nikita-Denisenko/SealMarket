namespace SealMarket.Application.DTOs.Responses.CreatedDTOs
{
    public record CreatedCartItemDto  
    (
        int Id,
        string ProductName,
        int ProductId,
        int CarttId
    );
}
