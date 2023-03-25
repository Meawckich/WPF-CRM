using BuildingMaterials.Models;
using System.Collections.Generic;

namespace BuildingMaterials.Interfaces
{
    public interface IOrderRepository
    {
        Order GetOrder(int id);
        IEnumerable<Order> GetALlOrders();
        Order Add(Order order);
        Order Update(Order order);
        Order Delete(int id);
    }
}
