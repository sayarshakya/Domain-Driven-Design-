using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wpm.Management.Domain.ValueObjects
{
    public record Weight
    {
        public decimal Value { get; init; }
        
        public Weight(decimal value)
        {
            if(value < 0)
            {
                throw new ArgumentException("Weight value is not valid.");
            }

            Value = value;
        }
    }
}
