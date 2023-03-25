using BuildingMaterials.DbContext;
using BuildingMaterials.Stores;
using BuildingMaterials.ViewModels;
using BuildingMaterials.Views;
using System.Windows;

namespace BuildingMaterials.Commands
{
    class NavToRegisterCommand : CommandBase
    {
        private readonly NavigationStore _navigationStore;
        private SqlServerDbContext _context;

        public NavToRegisterCommand(NavigationStore navigationStore, SqlServerDbContext context)
        {
            _navigationStore = navigationStore;
            _context = context;
        }

        public override void Execute(object parameter)
        {
            _navigationStore.CurrentViewModel = new RegisterViewModel(_navigationStore, _context);
            _navigationStore.Width = 800;
            _navigationStore.Height = 500;
        }


    }
}
