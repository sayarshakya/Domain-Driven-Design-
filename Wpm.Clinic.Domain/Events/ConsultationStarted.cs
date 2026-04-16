using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wpm.SharedKernel;

namespace Wpm.Clinic.Domain.Events
{
    public record ConsultationStarted(Guid Id, Guid PatientId, DateTime StartedAt) : IDomainEvent;
}
