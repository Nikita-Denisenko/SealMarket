using System.ComponentModel.DataAnnotations;

namespace SealMarket.Application.DTOs.Requests.CreateDTOs
{
    public record CreateProductDto
    (
        [Required]
        [MaxLength(100)]
        [MinLength(2)]
        string Name,

        [Required]
        [Range(1, int.MaxValue)]
        int BrandId,

        [MaxLength(500)]
        string Description,

        [Url]
        [MaxLength(500)]
        string ImageUrl,

        [Required]
        [Range(0, int.MaxValue)]
        int Quantity,

        [Required]
        [Range(0.01, double.MaxValue)]
        decimal Price,

        bool IsActive = true
    );
}
