using System.Collections.Generic;

namespace BuildingMaterials.Models
{
    public class Order
    {
        public int Id { get; set; }
        public decimal FullPrice { get; set; }
        public int StatusId { get; set; }
        public int UserId { get; set; }
        public virtual Status Status { get; set; }
        public virtual User User { get; set; }
    }
}
