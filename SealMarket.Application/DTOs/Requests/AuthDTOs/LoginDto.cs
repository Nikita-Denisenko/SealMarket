using System.ComponentModel.DataAnnotations;

namespace SealMarket.Application.DTOs.Requests.AuthDTOs
{
    public record LoginDto
    (
        [Required]
        [MinLength(3)]
        [MaxLength(50)]
        string Login,

        [Required]
        [MinLength(8)]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d).{8,}$")]
        string Password
    );
}
