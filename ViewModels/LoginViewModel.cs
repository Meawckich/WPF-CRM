using BuildingMaterials.Commands;
using BuildingMaterials.DbContext;
using BuildingMaterials.Stores;
using MaterialDesignThemes.Wpf;
using System.Windows.Input;

namespace BuildingMaterials.ViewModels
{
    public class LoginViewModel : ViewModelBase
    {
        private readonly SqlServerDbContext _context;
        private string login;
        private string password;
        private SnackbarMessageQueue _errorMessage;
        public string Login
        {
            get => login;
            set
            {
                login = value;
                OnPropertyChanged(nameof(Login));
            }
        }

        public string Password
        {
            get => password;
            set
            {
                password = value;
                OnPropertyChanged(nameof(Password));
            }
        }

        public SnackbarMessageQueue ErrorMessage
        {
            get => _errorMessage;
            set
            {
                _errorMessage = value;
                OnPropertyChanged(nameof(ErrorMessage));
            }
        }
        public ICommand NavRegisterCommand { get; set; }
        public ICommand NavToDataCommand { get; set; }
        public LoginViewModel(NavigationStore store, SqlServerDbContext context, DialogStore store1)
        {
            _errorMessage = new SnackbarMessageQueue();
            NavRegisterCommand = new NavToRegisterCommand(store, context, store1);
            NavToDataCommand = new NavToDataCommand(store, this, context, store1);
        }
    }
}
