using BuildingMaterials.DataDTOs;
using BuildingMaterials.DbContext;
using BuildingMaterials.Interfaces;
using BuildingMaterials.Models;
using BuildingMaterials.Services;
using BuildingMaterials.Stores;
using System;
using System.Runtime.Serialization;
using System.Windows;

namespace BuildingMaterials.Commands
{
    public class OpenProductCommand : CommandBase
    {
        private DialogStore _diagStore; 
        private ProductDTO _product;
        private SqlServerDbContext _context;
        private ShowCatalog _catalog;
        public OpenProductCommand(DialogStore store,ShowCatalog cat, SqlServerDbContext context)
        {
            _context = context;
            _diagStore = store;
            _catalog = cat;
            cat.OnSelChanged += ShowMes;
        }

        private void ShowMes(ProductDTO product)
        {
            _product= product;
        }

        public override void Execute(object parameter)
        {
            if(_product!=null)
            {
                IDialogService dialog = new DialogService(_diagStore,_product, _context);
                dialog.ShowDialog();
            }
        }
    }
}
