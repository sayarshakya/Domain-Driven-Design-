using Wpm.Clinic.Domain.ValueObjects;
using Wpm.SharedKernel;

namespace Wpm.Clinic.Domain.Entities
{
    public class Consultation : AggregateRoot
    {
        private readonly List<DrugAdministration> administeredDrugs = new();
        private readonly List<VitalSigns> vitalSignReadings = new();
        public DateTimeRange When { get; private set; }
        public Text? Diagnosis { get; private set; }
        public Text? Treatment { get; private set; }
        public PatiendId PatiendId { get; init; }
        public Weight? CurrentWeight { get; private set; }
        public ConsultationStatus Status { get; private set; }
        public IReadOnlyCollection<DrugAdministration> AdministeredDrugs => administeredDrugs;
        public IReadOnlyCollection<VitalSigns> VitalSignReadings => vitalSignReadings;

        public Consultation(PatiendId patiendId)
        {
            Id = Guid.NewGuid();
            PatiendId = patiendId;
            Status = ConsultationStatus.Open;
            When = DateTime.UtcNow;
        }

        public void RegisterVitalSigns(IEnumerable<VitalSigns> vitalSigns)
        {
            ValidateConsulationStatus();
            vitalSignReadings.AddRange(vitalSigns);
        }

        public void AdministerDrug(DrugId drugId, Dose dose)
        {
            ValidateConsulationStatus();
            var newDrugAdministration = new DrugAdministration(drugId, dose);
            administeredDrugs.Add(newDrugAdministration);

        }

        public void End()
        {
            ValidateConsulationStatus();
            if(Diagnosis == null || Treatment == null || CurrentWeight == null)
            {
                throw new InvalidOperationException("The consultation cannot be ended.");
            }

            Status = ConsultationStatus.Closed;
            When = new DateTimeRange(When.StartedAt, DateTime.UtcNow);
        }

        public void SetWeight(Weight weight)
        {
            ValidateConsulationStatus();
            CurrentWeight = weight;
        }

        public void SetDiagnosis(Text diagnosis)
        {
            ValidateConsulationStatus();
            Diagnosis = diagnosis;
        }

        public void SetTreatment(Text treatment)
        {
            ValidateConsulationStatus();
            Treatment = treatment;
        }

        private void ValidateConsulationStatus()
        {
            if (Status == ConsultationStatus.Closed)
            {
                throw new InvalidOperationException("The consultation is already closed.");
            }
        }
    }

    public enum ConsultationStatus
    {
        Open,
        Closed
    }
}


