using static System.Runtime.InteropServices.JavaScript.JSType;

namespace SealMarket.Core.Models.Filters
{
    public record TransactionsFilter
    (
        int Page,
        int Size,
        DateTime FromDateTime,
        DateTime ToDateTime,
        string OrderParam,
        bool ByAscending
    );
}
