using System.Collections.Generic;

namespace BuildingMaterials.Models
{
    public class ProductCategory
    {
        public int Id { get; set; }
        public string Title{ get; set; }

        public ICollection<Product> products { get; set; }
    }
}
