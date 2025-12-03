using System.ComponentModel.DataAnnotations;

namespace SealMarket.Application.DTOs.Requests.CreateDTOs
{
    public record CreateCartItemDto
    (
       [Required]
       int ProductId,

       [Required]
       int Quantity = 1
   );
}
