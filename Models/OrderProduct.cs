namespace BuildingMaterials.Models
{
    public class OrderProduct
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public int OrderId { get; set; }
        public int Count { get; set; }
        public decimal ItemCost { get; set; }
        public virtual Product Product { get; set; }
        public virtual Order Order { get; set; }
    }
}
