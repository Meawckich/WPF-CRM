using BuildingMaterials.Stores;
using BuildingMaterials.Views;
using System.Windows.Controls;

namespace BuildingMaterials.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        private readonly NavigationStore _navigationStore;
        private int _width;
        private int _height;
        public int Width
        {
            get => _navigationStore.Width;
            set
            {
                _navigationStore.Width = value;
            }
        }
        public int Height
        {
            get => _navigationStore.Height;
            set
            {
                _navigationStore.Height = value;
            }
        }

        public ViewModelBase CurrentViewModel => _navigationStore.CurrentViewModel;

        public MainViewModel(NavigationStore navigationStore)
        {
            _navigationStore = navigationStore;

            _navigationStore.CurrentViewModelChanged += OnCurrentViewModelChanged;
        }

        private void OnCurrentViewModelChanged()
        {
            OnPropertyChanged(nameof(CurrentViewModel));
            OnPropertyChanged(nameof(Width));
            OnPropertyChanged(nameof(Height));
        }

    }
}
