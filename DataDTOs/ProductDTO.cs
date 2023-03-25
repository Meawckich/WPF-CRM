namespace BuildingMaterials.DataDTOs
{
    public class ProductDTO
    {
        public int Number { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int Count { get; set; }
        public string Measurement { get; set; }
        public string Type { get; set; }
        public string Category { get; set; }
        public string Manufacturer { get; set; }
        public string Provider { get; set; }
        public decimal Cost { get; set; }
    }
}
