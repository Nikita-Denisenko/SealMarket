using static SealMarket.Core.Constans.UserOrderParameters;

namespace SealMarket.Application.DTOs.Requests
{
    public record UsersFilterDto
    (
        int Page = 1,
        int Size = 20,
        int MinAge = 1,
        int MaxAge = 110,
        string OrderParam = Name,
        bool ByAscending = true,
        string SearchText = ""
    );
}
