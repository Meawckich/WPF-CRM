using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuildingMaterials.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int TypeId { get; set; }
        public int CategoryId { get; set; }
        public int ManufacturerId { get; set; }

        public virtual ProductType ProductType { get; set; }
        public virtual ProductCategory ProductCategory { get; set; }
        public virtual ProductManufacturer ProductManufacturer { get; set; }
    }
}
