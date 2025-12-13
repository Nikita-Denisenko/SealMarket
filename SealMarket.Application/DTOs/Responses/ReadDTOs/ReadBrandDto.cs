using SealMarket.Core.Entities;

namespace SealMarket.Application.DTOs.Responses.EntityDtos
{
    public record ReadBrandDto
    (
        int Id,
        string Name,
        string LogoUrl,
        string Description,
        List<ReadProductDto> Products
    );
}
