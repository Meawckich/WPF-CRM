using BuildingMaterials.DataDTOs;
using BuildingMaterials.DbContext;
using BuildingMaterials.Interfaces;
using BuildingMaterials.Models;
using BuildingMaterials.Services;
using Microsoft.EntityFrameworkCore.Query.Internal;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace BuildingMaterials.Commands
{
    public class SaveOrderToDb : CommandBase
    {
        private readonly SqlServerDbContext _context;
        public ObservableCollection<OrderProductsDTO> Orders;
        private readonly User _user;
        public SaveOrderToDb(CreateOrder comma,SqlServerDbContext context, User user)
        {
            comma.OnCollectionChanged += ChangeCollection;
            _context = context;
            _user = user;
        }
        public override void Execute(object parameter)
        {
            if (Orders == null)
                return;
            decimal price = 0M;
            foreach (var item in Orders)
            {
                price = price + item.ProductsCost;
            }
            var order = new Order()
            {
                StatusId = 3,
                FullPrice = price,
                UserId = _user.Id,
            };
            _context.Orders.Add(order);
            _context.SaveChanges();
            int orderId = _context.Orders.OrderBy(x => x.Id).Select(x => x.Id).Last();
            foreach(var item in Orders)
            {
                var orderProduct = new OrderProduct()
                {
                    ProductId = item.ProductID,
                    OrderId = orderId,
                    Count = item.Counts,
                    ItemCost = item.ProductsCost
                };
                _context.OrderProducts.Add(orderProduct);
            }
            _context.SaveChanges();
            string path = @$"{Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.FullName}\FinePrimerOne.docx";
            IDocHelper word = new WordProcessService(path,_context,Orders);
            for(int i=0; i < Orders.Count;i++)
            {

            }

        }

        public void ChangeCollection(ObservableCollection<OrderProductsDTO> collection)
        {
            Orders = collection;
        }
    }
}
