using System.ComponentModel.DataAnnotations;

namespace SealMarket.Application.DTOs.Requests.CreateDTOs
{
    public record CreateCartDto
    (
        [Required]
        [MaxLength(50)]
        string Name
    );
}
