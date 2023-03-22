using BuildingMaterials.Stores;
using BuildingMaterials.Views;
using System.Windows.Controls;

namespace BuildingMaterials.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        private NavigationStore _store;
        private Page _currentView;

        public Page CurrentView
        {
            get => _currentView;
            set
            {
                _currentView = value;
                OnPropertyChanged(nameof(CurrentView));
            }
        }
        public MainViewModel(NavigationStore store, LoginViewModel loginViewModel)
        {
            _store= store;
            _store.CurrentViewChanged += (page) => CurrentView = page;
            _store.OnCurrentViewChanged(new LoginView(loginViewModel));
        }

    }
}
