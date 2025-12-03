using System.ComponentModel.DataAnnotations;

namespace SealMarket.Application.DTOs.Requests.UpdateDTOs
{
    public class UpdateCartItemDto
    {
        [Range(1, 99)]
        public int Quantity { get; set; }
    }
}
