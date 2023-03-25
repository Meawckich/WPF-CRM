using BuildingMaterials.DbContext;
using BuildingMaterials.Interfaces;
using BuildingMaterials.Models;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;

namespace BuildingMaterials.Moks
{
    public class MockUserRepository : IUserRepository
    {
        private readonly List<User> _usersList;
        private readonly SqlServerDbContext _context;

        public MockUserRepository()
        {
            _context = new SqlServerDbContext();
            _usersList = _context.Users.ToList();
        }
        public User Add(User user)
        {
            //user.Id = _context.Users.Max(x => x.Id) + 1;
            if(_context.Users.FirstOrDefault(x => x.Id != user.Id) is User)
            {
                _context.Users.Add(user);
            }
            _context.SaveChanges();
            return user;
        }

        public User Delete(int id)
        {
            var search = _context.Users.FirstOrDefault(x => x.Id == id);
            if (search is not null)
            {
                _context.Users.Remove(search);
            }
            _context.SaveChanges();
            return search;
        }

        public IEnumerable<User> GetAllUsers() =>  _usersList;

        public User GetUser(int id) => _context.Users.FirstOrDefault(x=> x.Id == id);


        public User Update(User user)
        {
            var updatingUser = _context.Users.FirstOrDefault(x=> x.Id == user.Id);
            if (updatingUser is not null)
            {
                updatingUser.FullName= user.FullName;
                updatingUser.Adress= user.Adress;
                updatingUser.Login = user.Login;
                updatingUser.Password = user.Password;
                _context.Users.Add(updatingUser);
            }
            _context.SaveChanges();
            return user;
        }
    }
}
