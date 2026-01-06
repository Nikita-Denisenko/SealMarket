using System.ComponentModel.DataAnnotations;

namespace SealMarket.Application.DTOs.Requests.AuthDTOs
{
    public record RegisterDto
    (
        [Required]
        [MinLength(2)]
        [MaxLength(120)]
        string UserName,

        [Required]
        [DataType(DataType.Date)]
        DateOnly BirthDate,

        [Required]
        [MaxLength(50)]
        string City,

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
        string PhoneNumber,

        string? SecretCode 
    );
}
