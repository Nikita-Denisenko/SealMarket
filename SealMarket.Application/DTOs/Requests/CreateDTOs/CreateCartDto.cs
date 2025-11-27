using System.ComponentModel.DataAnnotations;

namespace SealMarket.Application.DTOs.Requests.CreateDTOs
{
    public record CreateCartDto
    (
        [Required]
        [MaxLength(50)]
        string Name,

        [Required]
        [Range(1, int.MaxValue)]
        int AccountId
    );
}
