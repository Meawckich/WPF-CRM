using BuildingMaterials.Commands;
using BuildingMaterials.DbContext;
using BuildingMaterials.Stores;
using MaterialDesignThemes.Wpf;
using System.Windows.Input;

namespace BuildingMaterials.ViewModels
{
    public class RegisterViewModel : ViewModelBase
    {
        private string _login;
        private string _password;
        private string _secondPass;
        private string _fullname;
        private string _address;

        public string Login
        {
            get => _login;
            set
            {
                _login = value;
                OnPropertyChanged(nameof(Login));
            }
        }
        public string Password
        {
            get => _password;
            set
            {
                _password = value;
                OnPropertyChanged(nameof(Password));
            }
        }
        public string PasswordOne
        {
            get => _secondPass;
            set
            {
                _secondPass = value;
                OnPropertyChanged(nameof(PasswordOne));
            }
        }
        public string FullName
        {
            get => _fullname;
            set
            {
                _fullname = value;
                OnPropertyChanged(nameof(FullName));
            }
        }
        public string Address
        {
            get => _address;
            set
            {
                _address = value;
                OnPropertyChanged(nameof(Address));
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

        private SnackbarMessageQueue _errorMessage;
        public ICommand BackCommand { get; set; }
        public ICommand EnterCommand { get; set; }
        public RegisterViewModel(NavigationStore store, SqlServerDbContext context, DialogStore store1)
        {
            _errorMessage = new SnackbarMessageQueue();
            BackCommand = new ReturnCommand(store, context, store1);
            EnterCommand= new EnterCommand(store,this, context);
        }
    }
}
