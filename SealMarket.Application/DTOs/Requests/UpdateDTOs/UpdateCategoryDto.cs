using System.ComponentModel.DataAnnotations;

namespace SealMarket.Application.DTOs.Requests.UpdateDTOs
{
    public record UpdateCategoryDto
    (
        [MinLength(2)]
        [MaxLength(50)]
        string? Name,

        [Url]
        [StringLength(250)]
        string? ImageUrl,

        [MaxLength(400)]
        string? Description
    );
}
