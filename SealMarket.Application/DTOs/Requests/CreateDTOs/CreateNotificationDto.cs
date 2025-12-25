using System.ComponentModel.DataAnnotations;

namespace SealMarket.Application.DTOs.Requests.CreateDTOs
{
    public record CreateNotificationDto
    (
        [Required]
        [Range(0, int.MaxValue)]
        int AccountId,

        [Required]
        [MinLength(10)]
        [MaxLength(300)]
        string Message,

        [Required]
        [MinLength(3)]
        [MaxLength(50)]
        string Name = "Notification"
    );
}
