namespace SealMarket.Application.DTOs.Responses.ReadDTOs.CategoryDtos
{
    public record CategoryDto
    (
        int Id,
        string Name,
        string ImageUrl,
        string Description,
        int ProductQuantity
    );
}
