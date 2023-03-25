using BuildingMaterials.DataDTOs;
using BuildingMaterials.DbContext;
using BuildingMaterials.Models;
using BuildingMaterials.ViewModels;
using MaterialDesignColors;
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
        private string _pickedType;
        private string _pickedCategory;
        public ICommand OpenProduct;

        private ProductDTO _openedProduct;

        public  delegate void PickingSelectedItem();

        //public void OnPickingSelectedItem()
        //{
        //    PickingSelectedItem?.Invoke();
        //    MessageBox.Show(OpenProduct.Title);
        //}
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

        public ObservableCollection<ProductDTO> ProductsList
        {
            get => _productsList;
            set
            {
                _productsList = value;
                OnPropertyChanged(nameof(ProductsList));
            }
        }

        public ShowCatalog(CatalogViewModel viewModel, SqlServerDbContext context)
        {
            //OpenProduct = new OpenProductCommand();
            _context = context;
            IEnumerable<string> smthing = from a in context.ProductTypes.ToList()
                                          select a.Title;
            Types = new ObservableCollection<string>(smthing);

            IEnumerable<string> cats = from a in context.ProductCategories.ToList()
                                          select a.Title;
            Categories = new ObservableCollection<string>(cats);


            Search();
            _viewModel = viewModel;
        }

        private void Search()
        {
            IEnumerable<ProductType> types;
            IEnumerable<ProductCategory> categories;
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

            var temp = _context.Products.ToList();
            
            var manufactures = _context.ProductManufacturers.ToList();
            var providers = _context.ProductProviders.ToList();
            var provider = _context.Providers.ToList();
            IEnumerable<ProductDTO> list = from a in temp
                                           join s in types on a.ProductTypeId equals s.Id
                                           join m in manufactures on a.ProductManufacturerId equals m.Id
                                           join c in categories on a.ProductCategoryId equals c.Id
                                           join p in providers on a.Id equals p.ProductId
                                           join r in provider on p.ProviderId equals r.Id
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
