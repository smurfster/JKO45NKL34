namespace Domain.Entities.Product
{
    public record SKU
    {
        private SKU(string value) => Value = value;

        public string Value { get; init; }

        public static SKU? Create(string value)
        {
            if (string.IsNullOrWhiteSpace(value)) return null;

            return new SKU(value);
        }
    }
}