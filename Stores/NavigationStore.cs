using System;
using System.Windows.Controls;

namespace BuildingMaterials.Stores
{
    public class NavigationStore
    {
        public event Action<Page> CurrentViewChanged;
        public void OnCurrentViewChanged(Page page) => CurrentViewChanged?.Invoke(page);
    }
}
