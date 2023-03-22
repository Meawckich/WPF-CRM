namespace BuildingMaterials.Models
{
    public class ProductProvider
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public int ProviderId { get; set; }

        public virtual Product Product { get; set; }
        public virtual Provider Provider{ get; set; }
    }
}
