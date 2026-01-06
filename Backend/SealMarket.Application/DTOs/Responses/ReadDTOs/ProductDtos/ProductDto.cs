using SealMarket.Core.Entities;

namespace SealMarket.Application.DTOs.Responses.ReadDTOs.ProductDtos
{
    public record ProductDto
    (
        int Id,
        string Name,
        string Description,
        string ImageUrl,
        int Quantity,
        decimal Price,
        DateTime CreatedAt,
        bool IsActive,
        int BrandId,
        string BrandName,
        int CategoryId,
        string CategoryName
    );
}
