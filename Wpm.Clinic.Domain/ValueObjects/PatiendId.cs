namespace Wpm.Clinic.Domain.ValueObjects
{
    public record PatiendId
    {
        public Guid Value { get; init; }
        public PatiendId(Guid value)
        {
            if (value == Guid.Empty)
            {
                throw new ArgumentNullException("value", "The identifier is not valid.");
            }
            Value = value;
        }

        public static implicit operator PatiendId(Guid value)
        {
            return new PatiendId(value);
        }

        public static implicit operator Guid(PatiendId value)
        {
            return value.Value;
        }
    }
}
