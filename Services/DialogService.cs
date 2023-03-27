using BuildingMaterials.DataDTOs;
using BuildingMaterials.DbContext;
using BuildingMaterials.Interfaces;
using BuildingMaterials.Models;
using BuildingMaterials.Stores;
using BuildingMaterials.ViewModels;
using BuildingMaterials.Views;

namespace BuildingMaterials.Services
{
    public class DialogService : IDialogService
    {
        private ProductDTO _product;
        private SqlServerDbContext _context;
        private DialogStore _diagStore;
        public DialogService(DialogStore store,ProductDTO product, SqlServerDbContext context)
        {
            _context = context;
            _diagStore = store;
            _product = product;
        }
        public void ShowDialog()
        {
            var dialogWindow = new ProductDialog()
            {
                DataContext = new ProductDialogViewModel(_diagStore),
            };
            _diagStore.CurrentViewModel = new ProductViewModel(_product, _diagStore, _context);
             dialogWindow.ShowDialog();
        }
    }
}
