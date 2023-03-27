using BuildingMaterials.DbContext;
using BuildingMaterials.Models;
using BuildingMaterials.Stores;
using BuildingMaterials.ViewModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace BuildingMaterials.Commands
{
    public class NavToDataCommand : CommandBase
    {
        private LoginViewModel _loginViewModel;
        private DialogStore _diagStore;
        private SqlServerDbContext _context;
        private NavigationStore _store;
        public NavToDataCommand(NavigationStore store,LoginViewModel loginViewModel, SqlServerDbContext context, DialogStore diagStore)
        {
            _loginViewModel = loginViewModel;
            _context = context;
            _store = store;
            _diagStore = diagStore;
        }
        public override void Execute(object parameter)
        {
            if (_loginViewModel.Login is null || _loginViewModel.Login == string.Empty)
            {
                _loginViewModel.ErrorMessage?.Enqueue("Заполните логин", null, null, null, false, true,
                TimeSpan.FromSeconds(2));
                return;
            }

            if (_loginViewModel.Password is null || _loginViewModel.Password == string.Empty)
            {
                _loginViewModel.ErrorMessage?.Enqueue("Заполните пароль", null, null, null, false, true,
                TimeSpan.FromSeconds(2));
                return;
            }

            var md5 = MD5.Create();
            var hash = md5.ComputeHash(Encoding.UTF8.GetBytes(_loginViewModel.Password));

            var findUser = _context.Users.FirstOrDefault(x => x.Login == _loginViewModel.Login && x.Password == Convert.ToBase64String(hash));

            if (findUser == null)
            {
                _loginViewModel.ErrorMessage?.Enqueue("Пользователя не существует", null, null, null, false, true,
                    TimeSpan.FromSeconds(2));
                return;
            }
            _store.CurrentViewModel = new CatalogViewModel(_diagStore, findUser, _context);
            _store.Width = 1100;
            _store.Height = 700;
        }
    }
}
