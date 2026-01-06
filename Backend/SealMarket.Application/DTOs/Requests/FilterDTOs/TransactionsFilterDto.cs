using SealMarket.Application.Helpers;
using static SealMarket.Core.Constants.TransactionOrderParameters;
namespace SealMarket.Application.DTOs.Requests.FilterDTOs
{
    public record TransactionsFilterDto
    (
        int Page = 1,
        int Size = 20,
        DateTime? FromDateTime = null,
        DateTime? ToDateTime = null,
        string OrderParam = Date,
        bool ByAscending = false
    );
}
