namespace Domain.Entities.Product
{
    public class ProductEntity
    {
        public int Id { get; private set; }
        public string Name { get; private set; } = string.Empty;
        public string Description { get; private set; } = string.Empty;
        public SKU Sku { get; private set; }

        private ProductEntity() { }

        public ProductEntity(string name, string description, SKU sku)
        {
            if (string.IsNullOrWhiteSpace(name)) throw new ArgumentNullException(nameof(name));
            if (string.IsNullOrWhiteSpace(description)) throw new ArgumentNullException(nameof(description));
            if (sku == null || sku.Value == null) throw new ArgumentNullException(nameof(sku));

            this.Name = name;
            this.Description = description;
            this.Sku = sku;
        }

        public void UpdateName(string name)
        {
            if (string.IsNullOrWhiteSpace(name)) throw new ArgumentNullException(nameof(name));

            this.Name = name;
        }

        public void UpdateDescription(string description)
        {
            if (string.IsNullOrWhiteSpace(description)) throw new ArgumentNullException(nameof(description));

            this.Description = description;
        }

        public void UpdateSKU(string sku)
        {
            if (string.IsNullOrWhiteSpace(sku)) throw new ArgumentNullException(nameof(sku));

            this.Sku = SKU.Create(sku)!;
        }
    }
}