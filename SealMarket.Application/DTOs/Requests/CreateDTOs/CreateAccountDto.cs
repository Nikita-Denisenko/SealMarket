using System.ComponentModel.DataAnnotations;

namespace SealMarket.Application.DTOs.Requests.CreateDTOs
{
    public record CreateAccountDto
    (
        [Required]
        [MinLength(3)]
        [MaxLength(50)]
        string Login,

        [Required]
        [EmailAddress]
        string Email,

        [Required]
        [MinLength(8)]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d).{8,}$")]
        string Password,

        [Required]
        [Phone]
        string PhoneNumber
    );
}
