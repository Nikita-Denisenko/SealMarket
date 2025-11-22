using static SealMarket.Core.Constans.CartOrderParameters;

namespace SealMarket.Application.DTOs.Requests
{
   public record CartsFilterDto
   (
       int Page = 1,
       int Size = 20,
       decimal MinTotalPrice = 0,
       decimal MaxTotalPrice = 1000000000,
       string OrderParam = Name,
       bool ByAscending = true,
       string SearchText = ""
   );
}
