
using BuildingMaterials.DataDTOs;
using BuildingMaterials.DbContext;
using BuildingMaterials.Models;
using BuildingMaterials.ViewModels;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Windows.Controls;

namespace BuildingMaterials.Commands
{
    public class ShowOrders : CommandBase , INotifyPropertyChanged
    {
        private CatalogViewModel _viewModel;
        private SqlServerDbContext _context;
        private readonly User _user;
        private ObservableCollection<OrdersDTO> _ordersList;

        public ObservableCollection<OrdersDTO> OrdersList
        {
            get => _ordersList;
            set
            {
                _ordersList = value;
                OnPropertyChanged(nameof(OrdersList));
            }
        }

        public ShowOrders(CatalogViewModel viewModel, SqlServerDbContext context, User user)
        {
            _viewModel = viewModel;
            _context = context;
            _user = user;
        }

        public override void Execute(object parameter)
        {
            _viewModel.CurrentTarget = this;

            var myOrders = _context.Orders.ToList().Where(x => x.UserId == _user.Id);
            var stats = _context.Statuses.ToList();
            var prods = _context.Products.ToList();
            var prodsOrder = _context.OrderProducts.ToList();

            IEnumerable<OrdersDTO> ords = from a in myOrders
                                          join c in stats on a.StatusId equals c.Id
                                          join p in prodsOrder on a.Id equals p.OrderId
                                          join s in prods on p.ProductId equals s.Id
                                          select new OrdersDTO
                                          {
                                              Number = a.Id,
                                              FullPrice = a.FullPrice,
                                              FullName = _user.FullName,
                                              Status = c.Title,
                                              Count = p.Count
                                          };

            OrdersList = new ObservableCollection<OrdersDTO>(ords);

        }

        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged([CallerMemberName] string name = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}
