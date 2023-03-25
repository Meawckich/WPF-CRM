using BuildingMaterials.DbContext;
using BuildingMaterials.Interfaces;
using BuildingMaterials.Models;
using BuildingMaterials.Moks;
using BuildingMaterials.Stores;
using BuildingMaterials.ViewModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace BuildingMaterials.Commands
{
    public class EnterCommand : CommandBase
    {
        private NavigationStore _store;
        private RegisterViewModel _registerViewModel;
        private SqlServerDbContext _context;
        private IUserRepository NewUser { get; set; }

        public EnterCommand(NavigationStore store, RegisterViewModel registerViewModel, SqlServerDbContext context)
        {
            _store = store;
            _registerViewModel = registerViewModel;
            _context = context;
        }

        public override void Execute(object parameter)
        {
            if (_registerViewModel.FullName is null || _registerViewModel.FullName == string.Empty)
            {
                _registerViewModel.ErrorMessage?.Enqueue("Заполните ФИО", null, null, null, false, true,
                TimeSpan.FromSeconds(2));
                return;
            }

            if (_registerViewModel.Login is null || _registerViewModel.Login == string.Empty)
            {
                _registerViewModel.ErrorMessage?.Enqueue("Заполните логин", null, null, null, false, true,
                TimeSpan.FromSeconds(2));
                return;
            }

            if (_registerViewModel.Password is null || _registerViewModel.Password == string.Empty)
            {
                _registerViewModel.ErrorMessage?.Enqueue("Заполните пароль", null, null, null, false, true,
                TimeSpan.FromSeconds(2));
                return;
            }

            if (_registerViewModel.Address is null || _registerViewModel.Address == string.Empty)
            {
                _registerViewModel.ErrorMessage?.Enqueue("Заполните адрес", null, null, null, false, true,
                TimeSpan.FromSeconds(2));
                return;
            }

            if (_registerViewModel.Password != _registerViewModel.PasswordOne)
            {
                _registerViewModel.ErrorMessage?.Enqueue("Не совпадение паролей", null, null, null, false, true,
                TimeSpan.FromSeconds(2));
                return;
            }

            var findUser = _context.Users.FirstOrDefault(x => x.Login == _registerViewModel.Login);

            if (findUser != null)
            {
                _registerViewModel.ErrorMessage?.Enqueue("Пользовател с таким логином уже существует", null, null, null, false, true,
                    TimeSpan.FromSeconds(2));
                return;
            }

            NewUser = new MockUserRepository();

            var md5 = MD5.Create();
            var hash = md5.ComputeHash(Encoding.UTF8.GetBytes(_registerViewModel.Password));

            User user = new User(_registerViewModel.FullName, _registerViewModel.Login, Convert.ToBase64String(hash), 1, _registerViewModel.Address);
            NewUser.Add(user);

        }
    }
}
