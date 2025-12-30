using System.ComponentModel.DataAnnotations;

namespace SealMarket.Application.DTOs.Requests.CreateDTOs
{
    public record CreateCategoryDto
    (
        [Required]
        [MinLength(2)]
        [MaxLength(50)]
        string Name,

        [Required]
        [Url]
        [StringLength(250)]
        string ImageUrl,

        [Required]
        [MaxLength(400)]
        string Description
    );
}
