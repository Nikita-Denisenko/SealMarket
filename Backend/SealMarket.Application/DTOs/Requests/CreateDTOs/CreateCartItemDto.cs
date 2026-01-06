using System.ComponentModel.DataAnnotations;

namespace SealMarket.Application.DTOs.Requests.CreateDTOs
{
    public record CreateCartItemDto
    (       
       [Required]
       [Range(0, int.MaxValue)]
       int ProductId,

       [Range(1, int.MaxValue)]
       int Quantity = 1
   );
}
