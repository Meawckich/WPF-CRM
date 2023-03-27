using BuildingMaterials.DataDTOs;
using BuildingMaterials.DbContext;
using BuildingMaterials.Models;
using Microsoft.Office.Interop.Word;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading;
using System.Windows.Controls;
using System.Windows.Documents;

namespace BuildingMaterials.ViewModels
{
    public class AdminViewModel : ViewModelBase
    {
        private LoginViewModel _loginViewModel;
        private SqlServerDbContext _context;
        private ObservableCollection<ProductDTO> _products;

        public ObservableCollection<ProductDTO> Products
        {
            get => _products;
            set
            {
                _products= value;
                OnPropertyChanged(nameof(Products));
            }
        }

        public AdminViewModel(SqlServerDbContext context, LoginViewModel loginViewModel)
        {
            _context = context;
            _loginViewModel = loginViewModel;
            Search();

        }
        public void Search()
        {
            var prods = _context.Products.ToList();
            var types = _context.ProductTypes.ToList();
            var manus = _context.ProductManufacturers.ToList();
            var categories = _context.ProductCategories.ToList();
            var provs = _context.Providers.ToList();
            var providers = _context.ProductProviders.ToList();



            IEnumerable < ProductDTO > list = from a in prods
                                                  join s in types on a.ProductTypeId equals s.Id
                                                  join m in manus on a.ProductManufacturerId equals m.Id
                                                  join c in categories on a.ProductCategoryId equals c.Id
                                                  join p in providers on a.Id equals p.ProductId
                                                  join r in prods on p.ProviderId equals r.Id
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
            Products = new ObservableCollection<ProductDTO>(list);
        }
    }
}
