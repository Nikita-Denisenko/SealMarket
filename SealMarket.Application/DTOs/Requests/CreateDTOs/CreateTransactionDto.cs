using System.ComponentModel.DataAnnotations;

namespace SealMarket.Application.DTOs.Requests.CreateDTOs
{
    public record CreateTransactionDto
    (
        [Required]
        [Range(0, int.MaxValue)]
        int AccountId,

        [Required]
        [Range(0, int.MaxValue)]
        decimal TotalSum,

        [Required]
        bool IsSuccessful,

        [Required]
        [MaxLength(400)]
        string Message
    );
}
