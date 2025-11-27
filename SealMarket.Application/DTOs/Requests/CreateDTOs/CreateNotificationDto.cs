using System.ComponentModel.DataAnnotations;

namespace SealMarket.Application.DTOs.Requests.CreateDTOs
{
    public record CreateNotificationDto
    (
        [Required]
        [Range(1, int.MaxValue)]
        int AccountId,

        [Required]
        [MinLength(10)]
        [MaxLength(300)]
        string Message
    );
}
