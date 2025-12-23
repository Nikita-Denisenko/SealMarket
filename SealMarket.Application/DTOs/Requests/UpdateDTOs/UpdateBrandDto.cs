using System.ComponentModel.DataAnnotations;

namespace SealMarket.Application.DTOs.Requests.UpdateDTOs
{
    public record UpdateBrandDto
    (
        [MinLength(2)]
        [MaxLength(50)]
        string? Name = null,

        [Url]
        [StringLength(250)]
        string? LogoUrl = null,

        [MaxLength(400)]
        string? Description = null
    );
}
