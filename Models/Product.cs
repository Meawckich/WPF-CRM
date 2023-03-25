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
        public int Count { get; set; }
        public string Measurement { get; set; }
        public int ProductTypeId { get; set; }
        public int ProductCategoryId { get; set; }
        public int ProductManufacturerId { get; set; }
        public decimal Cost { get; set; }

        public virtual ProductType ProductType { get; set; }
        public virtual ProductCategory ProductCategory { get; set; }
        public virtual ProductManufacturer ProductManufacturer { get; set; }
    }
}
