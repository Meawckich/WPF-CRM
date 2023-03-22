using BuildingMaterials.Commands;
using BuildingMaterials.Stores;
using System.Windows.Input;

namespace BuildingMaterials.ViewModels
{
    public class LoginViewModel : ViewModelBase
    {
        public ICommand NavRegisterCommand { get; set; }
        public LoginViewModel(NavigationStore store,RegisterViewModel registerViewModel)
        {
            NavRegisterCommand = new NavToRegisterCommand(store, registerViewModel);
        }
    }
}
