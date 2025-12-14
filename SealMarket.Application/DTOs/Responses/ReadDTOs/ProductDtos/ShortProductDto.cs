using SealMarket.Core.Entities;

namespace SealMarket.Application.DTOs.Responses.ReadDTOs.ProductDtos
{
    public record ShortProductDto
    (
        int Id,
        string Name,
        string ImageUrl,
        decimal Price,
        string BrandName,
        int BrandId
    );
}
