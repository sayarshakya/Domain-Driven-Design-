using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wpm.SharedKernel;

namespace Wpm.Management.Domain.Events
{
    public record PetWeightUpdated(Guid Id, decimal Weight) : IDomainEvent;
}
