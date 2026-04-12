namespace Wpm.Clinic.Domain.ValueObjects
{
    public record Text
    {
        public string Value { get; init; }
        public Text(string value)
        {
            Validate(value);
            Value = value;
        }

        private void Validate(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                throw new ArgumentNullException("value", "Text cannot be null or whitespace.");
            }

            if (value.Length > 500)
            {
                throw new ArgumentException("Text cannot be longer than 500 characters.");
            }

        }

        public static implicit operator Text(string value)
        {
            return new Text(value);
        }
    }
}
