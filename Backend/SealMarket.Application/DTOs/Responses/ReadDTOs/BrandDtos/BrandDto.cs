namespace SealMarket.Application.DTOs.Responses.ReadDTOs.BrandDtos
{
    public record BrandDto
    (
        int Id,
        string Name,
        string LogoUrl,
        string Description,
        int ProductQuantity
    );
}
