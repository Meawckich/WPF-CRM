using BuildingMaterials.Models;
using System.Collections.Generic;

namespace BuildingMaterials.Interfaces
{
    public interface IUserRepository
    {
        User GetUser(int id);
        IEnumerable<User> GetAllUsers();
        User Add(User user);
        User Update(User user);
        User Delete(int id);
    }
}
