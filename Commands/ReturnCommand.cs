using BuildingMaterials.DbContext;
using BuildingMaterials.Stores;
using BuildingMaterials.ViewModels;
using BuildingMaterials.Views;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel;

namespace BuildingMaterials.Commands
{
    public class ReturnCommand : CommandBase
    {
        private DialogStore _dialogStore;
        private NavigationStore _store;
        private SqlServerDbContext _context;


        public ReturnCommand(NavigationStore store,SqlServerDbContext context, DialogStore dialogStore)
        {
            _store = store;
            _context = context;
            _dialogStore = dialogStore;
        }

        public override void Execute(object parameter)
        {
            _store.CurrentViewModel = new LoginViewModel(_store, _context, _dialogStore);
            _store.Width = 800;
            _store.Height = 450;
        }
    }
}
