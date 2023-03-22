using BuildingMaterials.ViewModels;
using System.Windows.Controls;

namespace BuildingMaterials.Views
{
    public partial class LoginView : Page
    {
        public LoginView(LoginViewModel loginViewModel)
        {
            InitializeComponent();
            DataContext = loginViewModel;
        }
    }
}
