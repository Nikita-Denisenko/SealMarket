using System.ComponentModel.DataAnnotations;

namespace SealMarket.Application.DTOs.Requests.UpdateDTOs
{
    public record UpdateAccountDto
    (
        [MinLength(3)]
        [MaxLength(50)]
        string? Login = null,

        [MinLength(8)]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d).{8,}$")]
        string? Password = null,

        [EmailAddress]
        string? Email = null,

        [Phone]
        string? PhoneNumber = null
    );
}
