using static SealMarket.Core.Constans.BrandOrderParameters;

namespace SealMarket.Application.DTOs.Requests.FilterDTOs
{
    public record BrandsFilterDto
    (
        int Page = 1,
        int Size = 20,
        decimal MinAverageProductPrice = 0,
        decimal MaxAverageProductPrice= 1000000000,
        string OrderParam = Name,
        string SearchText = ""
    );
}
