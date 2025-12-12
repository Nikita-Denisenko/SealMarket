using System.ComponentModel.DataAnnotations;

namespace SealMarket.Application.DTOs.Requests.CreateDTOs
{
    public record CreateNotificationDto
    (
        [Required]
        [MinLength(10)]
        [MaxLength(300)]
        string Message
    );
}
