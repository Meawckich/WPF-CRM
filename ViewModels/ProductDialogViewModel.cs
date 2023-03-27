using BuildingMaterials.Stores;
using System.Windows.Media.Media3D;

namespace BuildingMaterials.ViewModels
{
    public class ProductDialogViewModel : ViewModelBase
    {
        private readonly DialogStore _dialogStore;
        public ViewModelBase CurrentViewModel => _dialogStore.CurrentViewModel;
        public ProductDialogViewModel(DialogStore store)
        {
            _dialogStore = store;
            _dialogStore.CurrentViewModelChanged += OnCurrentViewModelChanged;
        }

        private void OnCurrentViewModelChanged()
        {
            OnPropertyChanged(nameof(CurrentViewModel));
        }
    }
}
