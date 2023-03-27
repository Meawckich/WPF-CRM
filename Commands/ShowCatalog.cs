using BuildingMaterials.DataDTOs;
using BuildingMaterials.DbContext;
using BuildingMaterials.Models;
using BuildingMaterials.Stores;
using BuildingMaterials.ViewModels;
using MaterialDesignColors;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Documents;
using System.Windows.Input;

namespace BuildingMaterials.Commands
{
    public class ShowCatalog : CommandBase , INotifyPropertyChanged
    {
        private CatalogViewModel _viewModel;
        private SqlServerDbContext _context;
        private ObservableCollection<ProductDTO> _productsList;
        private ObservableCollection<string> _types;
        private ObservableCollection<string> _categories;
        private ObservableCollection<string> _providers;
        private ObservableCollection<string> _manufacturers;
        private string _pickedType;
        private string _pickedCategory;
        private string _provider;
        private string _manufacturer;
        private string _pickedSoundLike;
        private ProductDTO _openedProduct;
        public delegate void OnSelChanging(ProductDTO product);
        public event OnSelChanging OnSelChanged;


        public ICommand OpenProduct { get; set; }
        public string PickedType
        {
            get => _pickedType;
            set
            {
                _pickedType = value;
                OnPropertyChanged(nameof(PickedType));
                Search();
            }
        }
        public ProductDTO OpenedProduct
        {
            get => _openedProduct;
            set
            {
                _openedProduct = value;
                OnPropertyChanged(nameof(OpenedProduct));
                OnSelChanged?.Invoke(OpenedProduct);
            }
        }
        public string PickedCategory
        {
            get => _pickedCategory;
            set
            {
                _pickedCategory = value;
                OnPropertyChanged(nameof(PickedCategory));
                Search();
            }
        }
        public string Provider
        {
            get => _provider;
            set
            {
                _provider = value;
                OnPropertyChanged(nameof(Provider));
                Search();
            }
        }
        public string Manufacturer
        {
            get => _manufacturer;
            set
            {
                _manufacturer = value;
                OnPropertyChanged(nameof(Manufacturer));
                Search();
            }
        }
        public string PickedSoundsLikeSearch
        {
            get => _pickedSoundLike;
            set
            {
                _pickedSoundLike = value;
                OnPropertyChanged(nameof(PickedSoundsLikeSearch));
                Search();
            }
        }

        public ObservableCollection<string> Types
        {
            get => _types;
            set
            {
                _types = value;
                OnPropertyChanged(nameof(Types));
            }
        }
        public ObservableCollection<string> Categories
        {
            get => _categories;
            set
            {
                _categories = value;
                OnPropertyChanged(nameof(Categories));
            }
        }
        public ObservableCollection<string> Providers
        {
            get => _providers;
            set
            {
                _providers = value;
                OnPropertyChanged(nameof(Providers));
            }
        }
        public ObservableCollection<string> Manufacturers
        {
            get => _manufacturers;
            set
            {
                _manufacturers = value;
                OnPropertyChanged(nameof(Manufacturers));
            }
        }

        public ObservableCollection<ProductDTO> ProductsList
        {
            get => _productsList;
            set
            {
                _productsList = value;
                OnPropertyChanged(nameof(ProductsList));
            }
        }

        public ShowCatalog(DialogStore dialogStore,CatalogViewModel viewModel, SqlServerDbContext context)
        {
            
            OpenProduct = new OpenProductCommand(dialogStore, this, context);
            _context = context;
            IEnumerable<string> smthing = from a in context.ProductTypes.ToList()
                                          select a.Title;
            Types = new ObservableCollection<string>(smthing);

            IEnumerable<string> cats = from a in context.ProductCategories.ToList()
                                          select a.Title;
            Categories = new ObservableCollection<string>(cats);

            IEnumerable<string> pro = from a in context.Providers.ToList()
                                          select a.Title;
            Providers= new ObservableCollection<string>(pro);

            IEnumerable<string> man = from a in context.ProductManufacturers.ToList()
                                      select a.Title;
            Manufacturers = new ObservableCollection<string>(man);

            Search();
            _viewModel = viewModel;
        }

        private void Search()
        {
            IEnumerable<ProductType> types;
            IEnumerable<ProductCategory> categories;
            IEnumerable<Provider> provs;
            IEnumerable<ProductManufacturer> manus;
            IEnumerable<Product> prods;


            if(PickedType != null) 
            {
                types = _context.ProductTypes.Where(x => x.Title == PickedType).ToList();
            }
            else
            {
                types = _context.ProductTypes.ToList();
            }

            if (PickedCategory != null)
            {
                categories = _context.ProductCategories.Where(x => x.Title == PickedCategory).ToList();
            }
            else
            {
                categories = _context.ProductCategories.ToList();
            }

            if(Provider != null)
            {
                provs = _context.Providers.Where(x => x.Title == Provider).ToList();
            }
            else
            {
                provs = _context.Providers.ToList();
            }

            if (Manufacturer != null)
            {
                manus = _context.ProductManufacturers.Where(x => x.Title == Manufacturer).ToList();
            }
            else
            {
                manus = _context.ProductManufacturers.ToList();
            }

            if( PickedSoundsLikeSearch != null) 
            {
                prods = _context.Products.Where(x => x.Title.StartsWith(PickedSoundsLikeSearch)).ToList();
            }
            else
            {
                prods = _context.Products.ToList();
            }

           
            
            var providers = _context.ProductProviders.ToList();
            IEnumerable<ProductDTO> list = from a in prods
                                           join s in types on a.ProductTypeId equals s.Id
                                           join m in manus on a.ProductManufacturerId equals m.Id
                                           join c in categories on a.ProductCategoryId equals c.Id
                                           join p in providers on a.Id equals p.ProductId
                                           join r in provs on p.ProviderId equals r.Id
                                           select new ProductDTO
                                           {
                                               Number = a.Id,
                                               Title = a.Title,
                                               Cost = a.Cost,
                                               Category = c.Title,
                                               Count = a.Count,
                                               Description = a.Description,
                                               Manufacturer = m.Title,
                                               Measurement = a.Measurement,
                                               Type = s.Title,
                                               Provider = r.Title
                                           };


            ProductsList = new ObservableCollection<ProductDTO>(list);
        }
        public override void Execute(object parameter)
        {
            _viewModel.CurrentTarget = this;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged([CallerMemberName] string name = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}
