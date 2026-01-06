using System.ComponentModel.DataAnnotations;

namespace SealMarket.Application.DTOs.Requests.CreateDTOs
{
    public record CreateBrandDto
    (
        [Required]
        [MinLength(2)]
        [MaxLength(50)]
        string Name,

        [Url]
        [StringLength(250)]
        string LogoUrl = "",

        [MaxLength(400)]
        string Description = ""
    );
}
