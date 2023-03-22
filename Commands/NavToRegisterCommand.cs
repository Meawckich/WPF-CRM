using BuildingMaterials.Stores;
using BuildingMaterials.ViewModels;
using BuildingMaterials.Views;
using System.Windows;

namespace BuildingMaterials.Commands
{
    class NavToRegisterCommand : CommandBase
    {
        private NavigationStore _store;
        private RegisterViewModel _registerViewModel;

        public NavToRegisterCommand(NavigationStore store,RegisterViewModel registerViewModel)
        {
            _store = store;
            _registerViewModel = registerViewModel;
        }
        public override void Execute(object parameter)
        {
            _store.OnCurrentViewChanged(new RegisterView(_registerViewModel));
        }
    }
}
