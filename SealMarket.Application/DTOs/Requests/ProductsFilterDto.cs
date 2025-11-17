using static SealMarket.Core.Constans.ProductOrderParameters;

namespace SealMarket.Application.DTOs.Requests
{
    public record ProductsFilterDto
    (
        int Page = 1,
        int Size = 20,
        decimal MinPrice = 0,
        decimal MaxPrice = 1000000000,
        string OrderParam = Name,
        bool ByAscending = true
    );
}
