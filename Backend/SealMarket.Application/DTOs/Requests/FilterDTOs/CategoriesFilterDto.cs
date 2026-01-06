using static SealMarket.Core.Constants.CategoryOrderParameters;

namespace SealMarket.Application.DTOs.Requests.FilterDTOs
{
    public record CategoriesFilterDto
    (
        int Page = 1,
        int Size = 20,
        string OrderParam = Name,
        bool ByAscending = true,
        string SearchText = ""
    );
}
