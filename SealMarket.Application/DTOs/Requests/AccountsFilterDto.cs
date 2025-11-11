using SealMarket.Core.Constans;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SealMarket.Application.DTOs.Requests
{
    public record AccountsFilterDto
    (
        int Page = 1, 
        int Size = 20, 
        decimal MinBalance = 0, 
        decimal MaxBalance = 1000000000,
        string OrderParam = AccountOrderParameters.Login,
        bool ByAscending = true
    );
}
