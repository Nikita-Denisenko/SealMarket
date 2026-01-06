namespace SealMarket.Core.Models.Filters
{
    public record CategoriesFilter
    (
        int Page,
        int Size,
        string OrderParam,
        bool ByAscending,
        string SearchText
    );
}
