using BuildingMaterials.DataDTOs;
using BuildingMaterials.DbContext;
using BuildingMaterials.Models;
using BuildingMaterials.Stores;
using BuildingMaterials.ViewModels;
using Microsoft.Identity.Client;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows.Input;

namespace BuildingMaterials.Commands
{
    public class CreateOrder : CommandBase , INotifyPropertyChanged
    {
        private ObservableCollection<OrderProductsDTO> _ordersList;
        private SqlServerDbContext _context;
        private readonly User _user;
        private CatalogViewModel _catalogViewModel;
        private DialogStore _store;
        public delegate void OnCollectionChanging(ObservableCollection<OrderProductsDTO> dTOs);
        public event OnCollectionChanging OnCollectionChanged;

        public ObservableCollection<OrderProductsDTO> OrdersList
        {
            get => _ordersList;
            set
            {
                _ordersList = value;
                OnPropertyChanged(nameof(OrdersList));
            }
        }
        public CreateOrder(DialogStore store, CatalogViewModel catalogViewModel,SqlServerDbContext context, User user)
        {
            _context = context;
            _user = user;
            _store = store;
            store.CollectionAdded += AddingCollection;
            _catalogViewModel = catalogViewModel;
            SaveOrderToDb = new SaveOrderToDb(this, context, user);
        }
        public override void Execute(object parameter)
        {
            _catalogViewModel.CurrentTarget = this;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public void AddingCollection(ObservableCollection<BasketDTO> basket)
        { 
            var prods = _context.Products.ToList();
            var providers = _context.Providers.ToList();
            IEnumerable<OrderProductsDTO> temp = from b in basket
                       join p in prods on b.NumberId equals p.Id
                       join s in providers on b.ProviderId equals s.Id
                       select new OrderProductsDTO
                       {
                           ProductID = p.Id,
                           ProductsCost = b.Fullprice,
                           ProductTitle = p.Title,
                           Counts = b.Counts,
                           ProviderInn = s.Inn,
                           ProviderAddres = s.Address,
                           ProviderTitle = s.Title
                       };


            OrdersList = new ObservableCollection<OrderProductsDTO>(temp);
            OnCollectionChanged?.Invoke(OrdersList);
        }

        public ICommand SaveOrderToDb { get; set; }
        public void OnPropertyChanged([CallerMemberName] string name = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}
