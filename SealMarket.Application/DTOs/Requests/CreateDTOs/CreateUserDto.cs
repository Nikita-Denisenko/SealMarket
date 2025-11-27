using System.ComponentModel.DataAnnotations;

namespace SealMarket.Application.DTOs.Requests.CreateDTOs
{
    public record CreateUserDto
    (
        [Required]
        [MaxLength(120)]
        string Name,

        [Required]
        DateOnly BirthDate,

        [Required]
        [MaxLength(50)]
        string City
    );
}
