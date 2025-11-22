using static SealMarket.Core.Constans.AccountOrderParameters;

namespace SealMarket.Application.DTOs.Requests
{
    public record AccountsFilterDto
    (
        int Page = 1, 
        int Size = 20, 
        decimal MinBalance = 0, 
        decimal MaxBalance = 1000000000,
        string OrderParam = Login,
        bool ByAscending = true,
        string SearchText = ""
    );
}
