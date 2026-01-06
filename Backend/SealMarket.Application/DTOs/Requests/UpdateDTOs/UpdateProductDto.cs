using System.ComponentModel.DataAnnotations;

namespace SealMarket.Application.DTOs.Requests.UpdateDTOs
{
    public record UpdateProductDto
    (
        [StringLength(100, MinimumLength = 3)]
        string? Name = null,

        [StringLength(500)]
        string? Description = null,

        [Url, StringLength(250)]
        string? ImageUrl = null,

        [Range(0, int.MaxValue)]
        int? Quantity = null,

        [Range(0.01, 1000000)]
        decimal? Price = null,

        bool? IsActive = null
    );
}
