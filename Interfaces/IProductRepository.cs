using BuildingMaterials.Models;
using System.Collections.Generic;

namespace BuildingMaterials.Interfaces
{
    public interface IProductRepository
    {
        Product GetProduct(int id);
        IEnumerable<Product>  GetAllProducts();
        Product Add(Product product);
        Product Update(Product product);
        Product Delete(int id);
    }
}
