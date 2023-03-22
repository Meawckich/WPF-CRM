using BuildingMaterials.ViewModels;
using System.Windows.Controls;

namespace BuildingMaterials.Views
{
    public partial class RegisterView : Page
    {
        public RegisterView(RegisterViewModel registerViewModel)
        {
            InitializeComponent();
            DataContext = registerViewModel;
        }
    }
}
