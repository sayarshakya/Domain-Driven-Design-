using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wpm.Management.Domain.ValueObjects
{
    public record WeightRange
    {
        public decimal From { get; init; }
        public decimal To { get; init; }
        public WeightRange(decimal from, decimal to)
        {

            From = from;
            To = to;
        }
    }
}
