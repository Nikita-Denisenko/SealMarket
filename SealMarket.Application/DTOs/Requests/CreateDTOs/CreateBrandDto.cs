using System.ComponentModel.DataAnnotations;

namespace SealMarket.Application.DTOs.Requests.CreateDTOs
{
    public record CreateBrandDto
    (
        [Required]
        [MaxLength(50)]
        string Name,

        string LogoUrl,

        [MaxLength(400)]
        string Description
    );
}
