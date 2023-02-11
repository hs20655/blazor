using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Shared
{
    public enum SearchOperation
    {
        // Operation
        Equal = 0,
        NotEqual = 1,
        LessThan = 2,
        LessThanOrEqual = 3,
        GreaterThan = 4,
        GreaterThanOrEqual = 5,
        Contains = 6,
        NotContains = 7,
        StartsWith = 8,
        NotStartsWith = 9,
        EndsWith = 10,
        NotEndsWith = 11,

        // Between
        Between = 12,
        NotBetween = 13,

        // Null
        Null = 14,
        NotNull = 15
    }
}
