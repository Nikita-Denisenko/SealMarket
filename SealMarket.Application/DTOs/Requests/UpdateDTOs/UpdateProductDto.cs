namespace SealMarket.Application.DTOs.Requests.UpdateDTOs
{
    public record UpdateProductDto
    (
        string? Name = null,
        string? Description = null,
        string? ImageUrl = null,
        int? Quantity = null,
        decimal? Price = null,
        bool? IsActive = null
    );
}
