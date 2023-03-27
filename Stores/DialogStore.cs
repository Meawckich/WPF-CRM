using BuildingMaterials.DataDTOs;
using BuildingMaterials.ViewModels;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace BuildingMaterials.Stores
{
    public class DialogStore : INotifyPropertyChanged
    {
        public event Action CurrentViewModelChanged;
        private ViewModelBase _currentViewModel;
        private ObservableCollection<BasketDTO> _basket;
        public delegate void CollectionAdding(ObservableCollection<BasketDTO> basket);
        public event CollectionAdding CollectionAdded;

        public ObservableCollection<BasketDTO> Basket
        {
            get => _basket;
            set
            {
                if (_basket == null)
                {
                    _basket = new ObservableCollection<BasketDTO>(value);
                }
                _basket = value;
                OnPropertyChanged(nameof(Basket));
                CollectionAdded?.Invoke(_basket);
            }
        }
        public ViewModelBase CurrentViewModel
        {
            get => _currentViewModel;
            set
            {
                _currentViewModel = value;
                OnCurrentViewModelChanged();
            }
        }

        private void OnCurrentViewModelChanged()
        {
            CurrentViewModelChanged?.Invoke();
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged([CallerMemberName] string name = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }


}
