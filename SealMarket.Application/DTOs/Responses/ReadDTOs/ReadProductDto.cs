using SealMarket.Core.Entities;

namespace SealMarket.Application.DTOs.Responses.EntityDtos
{
    public record ReadProductDto
    (
        int Id,
        string Name,
        string Description,
        string ImageUrl,
        int Quantity,
        decimal Price,
        DateTime CreatedAt,
        bool IsActive,
        int BrandId
    );
}
