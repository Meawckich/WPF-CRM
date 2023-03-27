using BuildingMaterials.Commands;
using BuildingMaterials.DataDTOs;
using BuildingMaterials.DbContext;
using BuildingMaterials.Models;
using BuildingMaterials.Stores;
using Microsoft.EntityFrameworkCore.Storage;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using static BuildingMaterials.Commands.AddingToBasket;

namespace BuildingMaterials.ViewModels
{
    public class ProductViewModel : ViewModelBase
    {
        private ProductDTO _product;
        private DialogStore _store;
        private SqlServerDbContext _context;
        private decimal _money;
        private int _counts;
        public ProductDTO Product
        {
            get => _product;
            set
            {
                _product= value;
                OnPropertyChanged(nameof(Product));
            }
        }
        public decimal Money
        {
            get => _money;
            set
            {
                _money= value;
                OnPropertyChanged(nameof(Money));
            }
        }
        public int Counts
        {
            get => _counts;
            set
            {
                if(value >=0)
                {
                    _counts = value;
                    OnPropertyChanged(nameof(Counts));
                    GetFullPrice();
                }
                else
                {
                    _counts = 0;
                }

            }
        }
        public ProductViewModel(ProductDTO product, DialogStore store, SqlServerDbContext context)
        {
            _context = context;
            _store = store;
            AddingToBasket = new AddingToBasket(this);
            _product = product;
        }

        public void GetFullPrice()
        {
            if (Product.Count >= Counts)
            {
               Money = Product.Cost * Counts;
            }
            else
            {
                Money = 0;
            }
        }
        public ICommand AddingToBasket { get; set; }

        public void OnAddedToBusket()
        {
            var item = _context.Providers.ToList();
            BasketDTO dTO = new BasketDTO()
            {
                Counts = Counts,
                Fullprice = Money,
                NumberId = Product.Number,
                ProviderId = item.Where(x => x.Title == Product.Provider).Select(x => x.Id).FirstOrDefault()
            };
            if (_store.Basket != null)
            {
                _store.Basket = new ObservableCollection<BasketDTO>(_store.Basket) { dTO };
            }
            else
            {
                _store.Basket = new ObservableCollection<BasketDTO>
                {
                    dTO
                };
            }

        }
    }
}
