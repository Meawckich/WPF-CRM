using BuildingMaterials.Commands;
using BuildingMaterials.DataDTOs;
using BuildingMaterials.DbContext;
using BuildingMaterials.Models;
using BuildingMaterials.Stores;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows.Input;

namespace BuildingMaterials.ViewModels
{
    public class CatalogViewModel : ViewModelBase
    {
        private string _fullName;
        private CommandBase _currentTarget;
        private int _basketcount;
        private decimal _price;

        public ICommand ShowCatalog { get; set; }
        public ICommand ShowOrders { get; set; }
        public ICommand CreateOrder { get; set; }
        public string FullName
        {
            get => _fullName;
            set
            {
                _fullName = value;
                OnPropertyChanged(nameof(FullName));
            }
        }
        public int BasketCount
        {
            get => _basketcount;
            set
            {
                _basketcount = value;
                OnPropertyChanged(nameof(BasketCount));
            }
        }
        public decimal Price
        {
            get => _price;
            set
            {
                _price = value;
                OnPropertyChanged(nameof(Price));
            }
        }
        public CommandBase CurrentTarget
        {
            get => _currentTarget;
            set
            {
                _currentTarget = value;
                OnPropertyChanged(nameof(CurrentTarget));
            }
        }

        public CatalogViewModel(DialogStore store, User user , SqlServerDbContext context)
        {
            CreateOrder = new CreateOrder(store,this, context, user);
            ShowCatalog = new ShowCatalog(store,this, context);
            ShowOrders = new ShowOrders(this, context , user);
            CurrentTarget = new ShowCatalog(store, this, context);
            FullName= user.FullName.Split(" ")[0] + " " + user.FullName.Split(" ")[1][0] + "." + user.FullName.Split(" ")[2][0] + ".";
            store.CollectionAdded += OnBasketItemsAdded;

        }

        public void OnBasketItemsAdded(ObservableCollection<BasketDTO> basket)
        {
            BasketCount = 0;
            Price = 0;
           foreach (var item in basket)
            {
                BasketCount = BasketCount + item.Counts;
                Price = Price + item.Fullprice;
            }
        }
    }
}
