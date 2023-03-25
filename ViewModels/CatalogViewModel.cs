using BuildingMaterials.Commands;
using BuildingMaterials.DataDTOs;
using BuildingMaterials.DbContext;
using BuildingMaterials.Models;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows.Input;

namespace BuildingMaterials.ViewModels
{
    public class CatalogViewModel : ViewModelBase
    {
        private string _fullName;
        private CommandBase _currentTarget;

        public ICommand ShowCatalog { get; set; }
        public ICommand ShowOrders { get; set; }
        public string FullName
        {
            get => _fullName;
            set
            {
                _fullName = value;
                OnPropertyChanged(nameof(FullName));
            }
        }
        public CommandBase CurrentTarget
        {
            get => _currentTarget;
            set
            {
                _currentTarget = value;
                OnPropertyChanged(nameof(CurrentTarget));
            }
        }

        public CatalogViewModel(User user , SqlServerDbContext context)
        {
            ShowCatalog = new ShowCatalog(this, context);
            ShowOrders = new ShowOrders(this, context , user);
            CurrentTarget = new ShowCatalog(this, context);
            FullName= user.FullName.Split(" ")[0] + " " + user.FullName.Split(" ")[1][0] + "." + user.FullName.Split(" ")[2][0] + ".";


        }
    }
}
