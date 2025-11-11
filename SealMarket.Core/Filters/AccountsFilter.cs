using SealMarket.Core.Constans;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SealMarket.Core.Filters
{
    public record AccountsFilter
    (
        int Page,
        int Size,
        decimal MinBalance,
        decimal MaxBalance,
        string OrderParam,
        bool ByAscending
    );
}
