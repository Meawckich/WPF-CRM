using BuildingMaterials.DataDTOs;
using BuildingMaterials.ViewModels;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.ObjectModel;
using System.Windows.Controls;

namespace BuildingMaterials.Stores
{
    public class NavigationStore
    {
        public event Action CurrentViewModelChanged;
        private ViewModelBase _currentViewModel;
        private int _height;
        private int _width;

        public ViewModelBase CurrentViewModel
        {
            get => _currentViewModel;
            set
            {
                _currentViewModel= value;
                OnCurrentViewModelChanged();
            }
        }
        public int Height
        {
            get => _height;
            set
            {
                _height = value;
                OnCurrentViewModelChanged();
            }
        }
        public int Width
        {
            get => _width;
            set
            {
                _width = value;
                OnCurrentViewModelChanged();
            }
        }

        private void OnCurrentViewModelChanged()
        {
            CurrentViewModelChanged?.Invoke();
        }
    }
}
