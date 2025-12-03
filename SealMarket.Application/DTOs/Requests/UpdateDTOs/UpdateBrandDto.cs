namespace SealMarket.Application.DTOs.Requests.UpdateDTOs
{
    public record UpdateBrandDto
    (
        string? Name = null,
        string? LogoUrl = null,
        string? Description = null
    );
}
