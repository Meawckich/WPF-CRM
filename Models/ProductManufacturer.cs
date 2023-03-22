using System.Collections.Generic;

namespace BuildingMaterials.Models
{
    public class ProductManufacturer
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Address { get; set; }

        public ICollection<Product> products { get; set; }
    }
}
